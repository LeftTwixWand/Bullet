﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class SampleCompany
    {
        public string CompanyID { get; set; }

        public string CompanyName { get; set; }

        public string ContactName { get; set; }

        public string ContactTitle { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public ICollection<User> Orders { get; set; }
    }
}
