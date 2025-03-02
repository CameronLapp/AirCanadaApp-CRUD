namespace AirCanadaApp.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TicketOrder
    {
        [Key]
        public int TicketOrderId { get; set; }

        [Required]
        [DisplayName("Seat Number")]
        public string SeatNumber { get; set; }

        [Required]
        public string Class { get; set; }

        [Required]
        [DisplayName("Accessible")]
        public bool IsAccessible { get; set; }

        [DisplayName("Special Info")]
        public string? SpecialInfo { get; set; }

        [Required]
        [DisplayName("Available")]
        public bool IsAvailable { get; set; }

        [Required]
        [ForeignKey("FlightData")]
        public int FlightDataId { get; set; }


        public FlightData? FlightData { get; set; }
    }
}
