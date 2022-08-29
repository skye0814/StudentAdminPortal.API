using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.DomainModels
{
    public class StudentRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
        public Guid GenderId { get; set; }

        // For Address props
        public string PhysicalAddress { get; set; }
        public string PostalAddress { get; set; }

    }
}
