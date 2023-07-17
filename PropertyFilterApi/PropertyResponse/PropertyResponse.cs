namespace PropertyFilterApi.Controllers
{
    public class PropertyResponse
    {
        public PropertyResponse(string propertyName, decimal askingPrice, Address address)
        {
            PropertyName = propertyName;
            AskingPrice = askingPrice;
            Address = address;
        }

        public string PropertyName { get; set; }
        public decimal AskingPrice { get; set; }
        public Address Address { get; set; }
    }
}
