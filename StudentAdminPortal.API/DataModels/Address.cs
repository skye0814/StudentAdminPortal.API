namespace StudentAdminPortal.API.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Address
    {
        public Guid AddressId { get; set; }
        public string PhysicalAddress { get; set; }
        public string PostalAddress { get; set; }

        // Nav Prop
        public Guid StudentId { get; set; }
    }
}
