using ContactManagerServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.Models
{
    public class AddressViewModel
    {
        public AddressType Type { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public Address ToDataModel()
        {
            return new Address
            {
                Street1 = this.Street1,
                Street2 = this.Street2,
                City = this.City,
                State = this.State,
                Zip = this.Zip,
                Type = this.Type
            };
        }

    }
}
