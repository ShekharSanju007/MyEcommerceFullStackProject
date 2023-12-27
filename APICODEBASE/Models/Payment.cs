namespace APICODEBASE.Models
{
    public class Payment
    {
        public string? PaymentId { get; set; }

      
        public int? OrderId { get; set; }

       
        public DateTime? date { get; set; }

       
        public decimal? amount { get; set; }

        public string? paymentMethod { get; set; }

        
        public string? cardNumber { get; set; }

       
        public string? securityCode { get; set; }

        
        public string? cardName { get; set; }

        
        public string? expiryCode { get; set; }

        
        public string? status { get; set; }










    }
}
