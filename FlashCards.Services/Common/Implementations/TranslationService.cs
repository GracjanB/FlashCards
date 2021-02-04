using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace FlashCards.Services.Common.Implementations
{
    public class TranslationService
    {
        private const string BaseURI = "https://translated-mymemory---translation-memory.p.rapidapi.com/";
        private const string RapidApiKey = "eb887f73e0msh58e55f45fe8df73p102a6cjsn47f482df0db4";
        private const string RapidApiHost = "translated-mymemory---translation-memory.p.rapidapi.com";
        private readonly HttpClient _httpClient;

        public TranslationService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(BaseURI);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async void GetTranslations(string langFrom, string langTo, string query)
        {
            var uriBuilder = new UriBuilder(BaseURI);
            uriBuilder.Path = "api/get";
            var queryPairs = HttpUtility.ParseQueryString(uriBuilder.Query);
            queryPairs["langpair"] = langFrom + "|" + langTo;
            queryPairs["q"] = query;
            queryPairs["mt"] = "1";
            queryPairs["onlyprivate"] = "0";
            queryPairs["de"] = "a@b.c";
            uriBuilder.Query = queryPairs.ToString();

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", RapidApiKey);
            _httpClient.DefaultRequestHeaders.Add("x-rapidapi-host", RapidApiHost);
            _httpClient.DefaultRequestHeaders.Add("useQueryString", true.ToString());

            using (var response = await _httpClient.GetAsync(uriBuilder.ToString()))
            {
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine();
                }
            }
        }
    }
}
