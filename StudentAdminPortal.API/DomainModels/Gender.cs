﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.DomainModels
{
    
    public class Gender
    {
        public Guid GenderId { get; set; }
        public string Description { get; set; }
    }
}
