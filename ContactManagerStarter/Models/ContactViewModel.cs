using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactManagerServices.Data;

namespace ContactManager.Models
{
    public class ContactViewModel
    {
        private Contact a;

        public ContactViewModel(Contact contact)
        {
            this.Id = contact.Id;
            this.Title = contact.Title;
            this.FirstName = contact.FirstName;
            this.LastName = contact.LastName;
            this.PrimaryEmailAddress = contact.EmailAddresses.Any(a => a.Type == EmailType.Primary) ? contact.EmailAddresses.First(a => a.Type == EmailType.Primary).Email : (contact.EmailAddresses.Any() ? contact.EmailAddresses.First().Email : string.Empty);
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PrimaryEmailAddress { get; set; }
    }
}
