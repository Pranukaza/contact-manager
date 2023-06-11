using ContactManagerServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.Models
{
    public class SaveContactViewModel
    {
        public Guid ContactId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public List<EmailViewModel> Emails { get; set; } = new List<EmailViewModel>();
        public List<AddressViewModel> Addresses { get; set; } = new List<AddressViewModel>();

        public Contact ToDataModel(Contact contact)
        {
            contact.Id = this.ContactId;
            contact.Title = this.Title;
            contact.FirstName = this.FirstName;
            contact.LastName = this.LastName;
            contact.DOB = this.DOB;
            contact.EmailAddresses = this.Emails.Select(e => e.ToDataModel(contact)).ToList();
            contact.Addresses = this.Addresses.Select(e => e.ToDataModel()).ToList();
            return contact;
        }

    }
}
