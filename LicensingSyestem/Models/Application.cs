using System;
using System.Collections.Generic;
using System.Text;

namespace LicensingSyestem.Models
{
    public abstract class Application
    {
        public Application()
        {
            CreatedDate = DateTime.Now;
        }
        public int Id { get; set; }
        public string AppName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
