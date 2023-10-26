using System.Text.Json.Serialization;

namespace JayrideCodeChallengeAPI.Models
{
    public class ListingsResponse
    {
        public string from { get; set; }
        public string to { get; set; }
        public List<Listing> listings { get; set; }
    }

    public class Listing
    {
        public string name { get; set; }
        public double pricePerPassenger { get; set; }
        public VehicleType vehicleType { get; set; }
    }

    public class VehicleType
    {
        public string name { get; set; }
        public int maxPassengers { get; set; }
    }
}
