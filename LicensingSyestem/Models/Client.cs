using System;
using System.Collections.Generic;
using System.Text;

namespace LicensingSyestem.Models
{
    public class Client
    {
        public Client()
        {
            CreatedDate = DateTime.Now;
        }
        public Guid Id { get; set; }
        public string UniqueID { get; set; }
        public string DeviceID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastLogin { get; set; }
        public bool Active { get; set; }
        public string Extra { get; set; }
        public Guid LicenseId { get; set; }
    }
}
