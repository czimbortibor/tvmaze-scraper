using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TvMazeScraper.Application.Common.Interfaces;
using TvMazeScraper.Application.Common.Models;

namespace Infrastructure.Scraper
{
    public class Scraper : IScraper
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<Scraper> _logger;
        private readonly IApplicationDbContext _dbContext;

        private const string BaseScrapingUrl = "https://api.tvmaze.com/shows";
        private const string ScrapingUrlQueryParams = "?embed=cast";
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public Scraper(HttpClient httpClient, ILogger<Scraper> logger, IApplicationDbContext dbContext)
        {
            _httpClient = httpClient;
            _logger = logger;
            _dbContext = dbContext;

            _jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }
        
        // scrape max 20 items from a random range of Show IDs
        public async IAsyncEnumerable<TvMazeShowModel> ScrapeSome([EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var maxNumberOfItemsToGet = 20;
            var guessedNumberOfTotalItems = 1000;
            
            int rangeStartIndex = new Random().Next(1, guessedNumberOfTotalItems);

            var indexListToScrape = Enumerable.Range(rangeStartIndex, maxNumberOfItemsToGet);
            foreach (var index in indexListToScrape)
            {
                TvMazeShowModel tvShowModel = await Scrape(index, cancellationToken);

                yield return tvShowModel;
            }
        }

        private async Task<TvMazeShowModel> Scrape(int index, CancellationToken cancellationToken)
        {
            string scrapingUrl = BuildNextResourceUrl(index);

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(scrapingUrl, cancellationToken);

                response.EnsureSuccessStatusCode();

                string responseContent = await response.Content.ReadAsStringAsync();
                TvMazeShowModel tvShowModel = GetModel(responseContent);

                return tvShowModel;
            }
            catch (Exception ex)
            {
                // ignore for now
                
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        private TvMazeShowModel GetModel(string rawData)
        {
            if (string.IsNullOrWhiteSpace(rawData))
            {
                return null;
            }
            
           return JsonConvert.DeserializeObject<TvMazeShowModel>(rawData, _jsonSerializerSettings);
        } 
        
        private string BuildNextResourceUrl(int index) => $"{BaseScrapingUrl}/{index}{ScrapingUrlQueryParams}";
    }
}