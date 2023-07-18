namespace PropertyFilterApi.Controllers
{
    public class Address
    {
        public string City { get; internal set; }
        public string District { get; internal set; }
        public string Road { get; internal set; }
        public string Number { get; internal set; }

        public override string ToString()
        {
            return $"{City} {District} {Road} {Number}";
        }
    }
}
