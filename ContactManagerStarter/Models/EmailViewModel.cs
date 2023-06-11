using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactManagerServices.Data;

namespace ContactManager.Models
{
    public class EmailViewModel
    {
        public EmailType Type { get; set; }
        public string Email { get; set; }

        public EmailAddress ToDataModel(Contact contact)
        {
            return new EmailAddress() { Email = this.Email, Type = this.Type, Contact = contact };
        }

    }
}
