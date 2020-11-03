using LicensingSyestem.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LicensingSyestem.Services
{
    public interface ILicenseSystemService
    {
        Task<int> AddNewApplicationAsync(Application application);
        Task<int> AddNewLisenseAsync(License license);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lisenseCode"></param>
        /// <param name="client"></param>
        /// <returns>0 license not valid, -1 client count is over, >0 success</returns>
        Task<int> AddClientAsync(string lisenseCode, Client client);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="client"></param>
        /// <returns>0 license not valid, -1 client count is over, >0 success</returns>
        Task<int> AddClientAsync(Guid Id, Client client);
        Task<License> GetLicenseAsync(string code);
        Task<bool> IsUserLimitReached(string lisenseCode);
        Task<bool> CanLoginAsync(string licenseCode, string UniqueId, string DeviceId);
    }
}
