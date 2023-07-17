namespace PropertyFilterApi.Controllers
{
    public class PropertyRequest
    //string? keyword, decimal? minPrice, decimal? maxPrice
    {
        public string? Keyword { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
