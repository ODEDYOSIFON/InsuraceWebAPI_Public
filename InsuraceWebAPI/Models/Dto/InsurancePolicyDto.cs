using InsuranceWebAPI.Models;

namespace InsuranceWebAPI.Models.Dto
{
    public class InsurancePolicyDto
    {
        public int ID { get; set; }
        public string PolicyNumber { get; set; } = "";
        public double InsuranceAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserID { get; set; }

    }
}
