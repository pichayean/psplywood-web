using Newtonsoft.Json;
using PSPlywoodWeb.Services.ResultModel;
using System.Text;

namespace PSPlywoodWeb.Services
{
    public class PSPlywoodHttpClient : IPSPlywoodService
    {
        private readonly HttpClient _httpClient;

        public PSPlywoodHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CategoryResultModel>> GetCategoriesAsync()
        {
            var response = await _httpClient.GetAsync("api/categories/getall");
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<CategoryResultModel>>(responseBody);
                Console.WriteLine($"Received data: {result}");
                return result == null ? new List<CategoryResultModel>() : result;
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                return new List<CategoryResultModel>();
            }
        }

        public async Task<ContactUsResultModel> GetContactUsAsync()
        {
            var response = await _httpClient.GetAsync("api/DisplayShop/GetContactMeDisplayShop");
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ContactUsResultModel>(responseBody);
                Console.WriteLine($"Received data: {result}");
                return result == null ? new ContactUsResultModel() : result;
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                return new ContactUsResultModel();
            }
        }

        public async Task<ProductResultModel> GetProductDetailAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/DisplayShop/GetProductDisplayLanddingShop/{id}");
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
            string jsonRequest = JsonConvert.SerializeObject(new
            {
                categoryId = categoryId,
            });
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/DisplayShop/GetProductsDisplayLanddingShop", content);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<ProductResultModel>>(responseBody);
                Console.WriteLine($"Received data: {result}");
                return result == null ? new List<ProductResultModel>() : result;
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                return new List<ProductResultModel>();
            }
        }

        public async Task<SettingsResultModel> GetSettingsAsync()
        {
            var response = await _httpClient.GetAsync("api/DisplayShop/GetSettingLanddingShop");
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<SettingsResultModel>(responseBody);
                Console.WriteLine($"Received data: {result}");
                return result == null ? new SettingsResultModel() : result;
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                return new SettingsResultModel();
            }
        }

        public async Task SendMessage(string mobileNumber, string productId)
        {
            string jsonRequest = JsonConvert.SerializeObject(new
            {
                mobileNumber = mobileNumber,
                productId = productId
            });
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/orders/UserWillOrder", content);
        }
    }
}
