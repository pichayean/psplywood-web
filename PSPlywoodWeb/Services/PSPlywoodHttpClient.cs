using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using PSPlywoodWeb.Services.ResultModel;
using System.Net.Http.Headers;
using System.Text;

namespace PSPlywoodWeb.Services
{
    public class PSPlywoodHttpClient : IPSPlywoodService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private SettingsResultModel _settingsResultModel;
        private int ctime = 1200; //1200= 20 minute

        public PSPlywoodHttpClient(HttpClient httpClient, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _cache = cache;
        }

        public async Task<ArticleResultModel> GetArticleAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/article/getarticle?id={id}");
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ArticleResultModel>(responseBody);
                Console.WriteLine($"Received data: {result}");
                return result == null ? new ArticleResultModel() : result;
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                return new ArticleResultModel();
            }
        }

        public async Task<List<ArticleResultModel>> GetArticlesAsync()
        {
            string jsonRequest = JsonConvert.SerializeObject(new
            {
                visibility = "Public",
                title = ""
            });
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/article/getarticle", content);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<ArticleResultModel>>(responseBody);
                Console.WriteLine($"Received data: {result}");
                return result == null ? new List<ArticleResultModel>() : result;
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                return new List<ArticleResultModel>();
            }
        }

        public async Task<List<CategoryResultModel>> GetCategoriesAsync()
        {
            List<CategoryResultModel> cacheData;
            var cacheKey = "psply_category";
            if (!_cache.TryGetValue(cacheKey, out cacheData))
            {
                var response = await _httpClient.GetAsync("api/categories/getall");
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<CategoryResultModel>>(responseBody);
                    Console.WriteLine($"Received data: {result}");
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(ctime));
                    // Save data in cache.
                    _cache.Set(cacheKey, result, cacheEntryOptions);
                    return result == null ? new List<CategoryResultModel>() : result;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return new List<CategoryResultModel>();
                }
            }
            else
            {
                return cacheData;
            }
        }

        public async Task<ContactUsResultModel> GetContactUsAsync()
        {
            ContactUsResultModel cacheData;
            var cacheKey = "psply_contact";
            if (!_cache.TryGetValue(cacheKey, out cacheData))
            {
                var response = await _httpClient.GetAsync("api/DisplayShop/GetContactMeDisplayShop");
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ContactUsResultModel>(responseBody);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(ctime));
                    // Save data in cache.
                    _cache.Set(cacheKey, result, cacheEntryOptions);
                    Console.WriteLine($"Received data: {result}");
                    return result == null ? new ContactUsResultModel() : result;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return new ContactUsResultModel();
                }
            }
            else
            {
                return cacheData;
            }
        }

        public async Task<ProductResultModel> GetProductDetailAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/DisplayShop/GetProductDisplayLanddingShop?id={id}");
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ProductResultModel>(responseBody);
                Console.WriteLine($"Received data: {result}");
                return result == null ? new ProductResultModel() : result;
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                return new ProductResultModel();
            }
        }

        public async Task<List<ProductResultModel>> GetProductsAsync(int categoryId)
        {
            List<ProductResultModel> cacheData;
            var cacheKey = "psply_product_list";
            if (!_cache.TryGetValue(cacheKey, out cacheData))
            {
                string jsonRequest = JsonConvert.SerializeObject(new
                {
                    categoryId = -1,
                });
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/DisplayShop/GetProductsDisplayLanddingShop", content);
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<ProductResultModel>>(responseBody);
                    Console.WriteLine($"Received data: {result}");
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(ctime));
                    // Save data in cache.
                    _cache.Set(cacheKey, result, cacheEntryOptions);
                    return result == null ? new List<ProductResultModel>() : result;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return new List<ProductResultModel>();
                }
            }
            else
            {
                return cacheData;
            }
        }

        public async Task<List<ProductResultModel>> GetAllProductsAsync(int categoryId)
        {
            List<ProductResultModel> cacheData;
            var cacheKey = "psply_all_product_list";
            if (!_cache.TryGetValue(cacheKey, out cacheData))
            {
                string jsonRequest = JsonConvert.SerializeObject(new
                {
                    productName = "",
                    categoryId = categoryId,
                    visibility = "Public",
                });
        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/Product/GetProduct", content);
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<ProductResultModel>>(responseBody);
                    Console.WriteLine($"Received data: {result}");
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(ctime));
                    // Save data in cache.
                    _cache.Set(cacheKey, result, cacheEntryOptions);
                    return result == null ? new List<ProductResultModel>() : result;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return new List<ProductResultModel>();
                }
            }
            else
            {
                return cacheData;
            }
        }


        public async Task<SettingsResultModel> GetSettingsAsync()
        {
            SettingsResultModel cacheData;
            var cacheKey = "psply_setting";
            if (!_cache.TryGetValue(cacheKey, out cacheData))
            {
                var response = await _httpClient.GetAsync("api/DisplayShop/GetSettingLanddingShop");
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<SettingsResultModel>(responseBody);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(ctime));
                    // Save data in cache.
                    _cache.Set(cacheKey, result, cacheEntryOptions);
                    return result == null ? new SettingsResultModel() : result;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return new SettingsResultModel();
                }
            } 
            else
            {
                return cacheData;
            }
        }

        public async Task SendMessage(string mobileNumber, string productId)
        {
            string jsonRequest = JsonConvert.SerializeObject(new
            {
                mobileNo = mobileNumber,
                productId = productId
            });
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/orders/UserWillOrder", content);
        }

        

        public async Task<int> GetSiteVisitCounterAsync()
        {
            var response = await _httpClient.GetAsync("api/Settings/GetSiteVisitCounter");
            if (!response.IsSuccessStatusCode)
                return 0;

            string responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SettingsResultModel>(responseBody);
            var cnt = result?.SiteVisitCounter??1002;
            return cnt;
        }
        

        public void SetSiteVisitCounter()
        {
            _httpClient.GetAsync("api/Settings/SetSiteVisitCounter");
        }

        public async Task<List<string>> GetAllTagsAsync()
        {
            var response = await _httpClient.GetAsync("api/tags/gettags");
            if (!response.IsSuccessStatusCode)
                return new List<string>();

            string responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<string>>(responseBody);
            return result?? new List<string>();
        }
    }
}
