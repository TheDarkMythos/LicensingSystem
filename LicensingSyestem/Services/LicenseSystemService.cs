using LicensingSyestem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicensingSyestem.Services
{
    public class LicenseSystemService : ILicenseSystemService
    {
        private SMDBContext dbContext;
        public LicenseSystemService(SMDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<int> AddClientAsync(string lisenseCode, Client client)
        {

            var license = await dbContext.Licenses.AsNoTracking().FirstOrDefaultAsync(q => q.Code == lisenseCode);
            if (license == null || !license.Active || (license.DeactiveateAfterExpiration && license.ExpirationDate.HasValue && license.ExpirationDate.Value > DateTime.Now))
                return 0;

            if (await IsUserLimitReached(license.Id, license.AllowedClientCount))
                return -1;

            client.LicenseId = license.Id;
            dbContext.Clients.Add(client);
            return await dbContext.SaveChangesAsync();
        }
        private async Task<bool> IsUserLimitReached(Guid licenseid, int MaxCount)
        {
            var IsUserLimited = await dbContext.Clients.AsNoTracking().CountAsync(q => q.LicenseId.Equals(licenseid)) >= MaxCount;
            return IsUserLimited;
        }
        public async Task<bool> IsUserLimitReachedAsync(string lisenseCode)
        {
            var license = await dbContext.Licenses.AsNoTracking().FirstOrDefaultAsync(q => q.Code == lisenseCode);
            var registedCount = await dbContext.Clients.CountAsync(q => q.LicenseId.Equals(license.Id));
            return registedCount >= license.AllowedClientCount;
        }
        public async Task<int> AddClientAsync(Guid Id, Client client)
        {
            var license = await dbContext.Licenses.AsNoTracking().FirstOrDefaultAsync(q => q.Id == Id);
            if (license == null || !license.Active || (license.DeactiveateAfterExpiration && license.ExpirationDate.HasValue && license.ExpirationDate.Value > DateTime.Now))
                return 0;

            if (await IsUserLimitReached(license.Id, license.AllowedClientCount))
                return -1;

            client.LicenseId = license.Id;
            dbContext.Clients.Add(client);
            return await dbContext.SaveChangesAsync();
        }
        public async Task<int> AddNewApplicationAsync(Application application)
        {
            dbContext.Applications.Add(application);
            return await dbContext.SaveChangesAsync();
        }
        public async Task<int> AddNewLisenseAsync(License license)
        {
            dbContext.Licenses.Add(license);
            return await dbContext.SaveChangesAsync();
        }
        public async Task<bool> CanLoginAsync(string licenseCode, string UniqueId, string DeviceId)
        {
            var lic = await dbContext.Licenses.AsNoTracking().FirstOrDefaultAsync(w => w.Code == licenseCode);
            if (lic == null || !lic.Active) return false;

            var clinet = await dbContext.Clients.AsNoTracking().FirstOrDefaultAsync(q => q.LicenseId == lic.Id && q.UniqueID == UniqueId && q.DeviceID == DeviceId);

            if (clinet == null || !clinet.Active) return false;

            return true;
        }
        public async Task<License> GetLicenseAsync(string code) => await dbContext.Licenses.FirstOrDefaultAsync(w => w.Code == code);
        public async Task<List<Application>> GetApplicationListAsync() => await dbContext.Applications.AsNoTracking().ToListAsync();
        public async Task<List<License>> GetAllLicenseListAsync() => await dbContext.Licenses.AsNoTracking().ToListAsync();
        public async Task<List<License>> GetLicenseListAsync(int application) => await dbContext.Licenses.AsNoTracking().Where(v => v.AppId == application).ToListAsync();
    }
}
