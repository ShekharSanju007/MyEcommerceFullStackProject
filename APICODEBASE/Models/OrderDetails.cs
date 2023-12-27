namespace APICODEBASE.Models
{
    public class OrderDetails
    {
        public int? OrderId { get; set; }

        public decimal? price { get; set; }


        public int? quantity { get; set; }


        public DateTime? date { get; set; }

        public string? orderStatus { get; set; }



        public string? street { get; set; }

        public string? city { get; set; }

        public int? pincode { get; set; }
        public string? state { get; set; }

        public string? country { get; set; }

        public int? ProductId { get; set; }

        public int? CustomerId { get; set; }


        public int? DeliveryId { get; set; }


        public int? VariantId { get; set; }


        public int? PaymentId { get; set; }










    }
}
