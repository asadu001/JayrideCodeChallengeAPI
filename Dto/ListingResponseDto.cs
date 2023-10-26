namespace JayrideCodeChallengeAPI.Dto
{
    //public class ListingResponseDto
    //{
    //    public List<Listing> listings { get; set; }
    //}

    public class ListingDto
    {
        public string name { get; set; }
        public double pricePerPassenger { get; set; }
        public VehicleType vehicleType { get; set; }
        public double TotalPrice { get; set; }
    }

    public class VehicleType
    {
        public string name { get; set; }
        public int maxPassengers { get; set; }
    }
}
