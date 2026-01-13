using DAL;
using DAL.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LicenseService.CQRS.Commands
{
    public class ApplyForLicenseCommand : IRequest<License>
    {
        public string UserId { get; set; }
        public string TenantId { get; set; }
        public string LicenseType { get; set; }
    }

    public class ApplyForLicenseCommandHandler : IRequestHandler<ApplyForLicenseCommand, License>
    {
        private readonly DashboardDbContext _db;
        public ApplyForLicenseCommandHandler(DashboardDbContext db) { _db = db; }

        public async Task<License> Handle(Commands.ApplyForLicenseCommand request, CancellationToken cancellationToken)
        {
            var entity = new License
            {
                Id = Guid.NewGuid().ToString(),
                TenantId = request.TenantId,
                UserId = request.UserId,
                Status = "Pending",
                ExpiryDate = DateTime.UtcNow.AddYears(1)
            };
            _db.Licenses.Add(entity);
            await _db.SaveChangesAsync(cancellationToken);
            return new License
            {
                Id = entity.Id,
                TenantId = entity.TenantId,
                UserId = entity.UserId,
                Status = entity.Status,
                ExpiryDate = entity.ExpiryDate
            };
        }
    }
}
