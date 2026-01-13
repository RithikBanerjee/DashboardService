using DAL;
using DAL.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace LicenseService.CQRS.Queries
{
    public class GetLicenseByIdQuery : IRequest<License>
    {
        public string LicenseId { get; set; }
        public string TenantId { get; set; }
    }

    public class GetLicenseByIdQueryHandler : IRequestHandler<GetLicenseByIdQuery, License>
    {
        private readonly DashboardDbContext _db;
        public GetLicenseByIdQueryHandler(DashboardDbContext db) { _db = db; }

        public async Task<License> Handle(GetLicenseByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _db.Licenses.FirstOrDefaultAsync(l => l.Id == request.LicenseId && l.TenantId == request.TenantId, cancellationToken);
            if (entity == null) return null;
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
