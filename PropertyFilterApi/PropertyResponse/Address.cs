namespace PropertyFilterApi.Controllers
{
    public class Address
    {
        public string City { get; init; }
        public string District { get; init; }
        public string Road { get; init; }
        public string Number { get; init; }

        public override string ToString()
        {
            return $"{City} {District} {Road} {Number}";
        }
    }
}
