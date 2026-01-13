using DAL;
using System;
using System.Linq;

namespace LicenseService.Jobs
{
    public class LicenseRenewalJob
    {
        private readonly DashboardDbContext _db;
        public LicenseRenewalJob(DashboardDbContext dbContext)
        {
            _db = dbContext;
        }

        public void ProcessRenewals()
        {
            // Logic to check for licenses nearing expiration and renew them
            var expiringLicenses = _db.Licenses
                .Where(license => license.ExpiryDate <= DateTime.UtcNow.AddDays(30) && license.Status == "Active")
                .ToList();
            foreach (var license in expiringLicenses)
            {
                // Example renewal logic: extend expiry date by one year
                license.ExpiryDate = license.ExpiryDate.AddYears(1);
                // Optionally, update status or send notification
            }
            _db.SaveChanges();
        }
    }
}
