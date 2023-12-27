using System.Net;

namespace APICODEBASE.Models
{
    public class Delivery
    {

        public int? DeliveryId { get; set; }

        public int? DeliveryPersonId { get; set; }


        public int? CustomerId { get; set; }

        public string? type { get; set; }

        public DateTime? expectedDeliverydate { get; set; }

        public string? status { get; set; }


        public string? street { get; set; }

        public string? city { get; set; }

        public int? pincode { get; set; }
        public string? state { get; set; }

        public string? country { get; set; }


    }
}
