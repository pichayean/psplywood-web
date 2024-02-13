﻿using PSPlywoodWeb.Services.ResultModel;

namespace PSPlywoodWeb.Services
{
    public interface IPSPlywoodService
    {
        //api/DisplayShop/GetProductsDisplayLanddingShop
        Task<List<ProductResultModel>> GetProductsAsync(int categoryId);
        //api/DisplayShop/GetProductDisplayLanddingShop
        Task<ProductResultModel> GetProductDetailAsync(int id);
        //api/DisplayShop/GetContactMeDisplayShop
        Task<ContactUsResultModel> GetContactUsAsync();
        //api/DisplayShop/GetSettingLanddingShop
        Task<SettingsResultModel> GetSettingsAsync();
        //api/categories/getall
        Task<List<CategoryResultModel>> GetCategoriesAsync();
        Task SendMessage(string mobileNumber, string productId);
    }
}