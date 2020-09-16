using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataValidation
{
    public class Contact
    {
        public string fullName { get; set; }

        public string cityName { get; set; }

        public string phoneNumber { get; set; }

        public string emailAddress { get; set; }

        public int ErrorCount { get; set; }
    }
}