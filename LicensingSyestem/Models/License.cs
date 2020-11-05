using System;
using System.Collections.Generic;
using System.Text;

namespace LicensingSyestem.Models
{
    public class License
    {
        public License()
        {
            CreatedDate = DateTime.Now;
        }
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string OwnerName { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool DeactiveateAfterExpiration { get; set; }
        public bool Active { get; set; }
        public int AllowedClientCount { get; set; }
        public int AppId { get; set; }
    }
}
