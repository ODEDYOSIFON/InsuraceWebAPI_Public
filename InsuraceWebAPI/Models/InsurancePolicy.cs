using System.Text.Json.Serialization;

namespace InsuranceWebAPI.Models
{
    public class InsurancePolicy
    {
        public int ID { get; set; }
        public string PolicyNumber { get; set; } = "";
        public double InsuranceAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserID { get; set; }
        [JsonIgnore]
        public User User { get; set; } 
    }
}
