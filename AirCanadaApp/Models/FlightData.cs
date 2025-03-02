namespace AirCanadaApp.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    public class FlightData
    {
        [Key]
        public int FlightDataId { get; set; }

        [Required]
        [DisplayName("Flight Number")]
        public string FlightNumber { get; set; }

        [Required]
        [DisplayName("Departure City")]
        public string DepartureCity { get; set; }

        [Required]
        [DisplayName("Arrival City")]
        public string ArrivalCity { get; set; }

        [Required]
        [DisplayName("Departure Time")]
        public DateTime DepartureTime { get; set; }

        [Required]
        [DisplayName("Arrival Time")]
        public DateTime ArrivalTime { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public float Price { get; set; }
        public List<TicketOrder>? TicketOrders { get; set; }
    }
}
