using System;

namespace DAL.Models
{
    public class License
    {
        public string Id { get; set; }
        public string TenantId { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
