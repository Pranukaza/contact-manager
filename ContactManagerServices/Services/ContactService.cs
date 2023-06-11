using ContactManagerServices.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagerServices.Services
{
    public class ContactService
    {
        public ApplicationContext _context { get; }

        public ContactService(ApplicationContext applicationContext)
        {
            _context = applicationContext;
        }

        public async Task<Contact> GetContactAsync(Guid id)
        {
            var contact = await _context.Contacts
                .Include(x => x.EmailAddresses)
                .Include(x => x.Addresses)
                .FirstOrDefaultAsync(x => x.Id == id);
            return contact;
        }
        public async Task DeleteContactAsync(Contact contactToDelete)
        {
            _context.EmailAddresses.RemoveRange(contactToDelete.EmailAddresses);
            _context.Contacts.Remove(contactToDelete);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Contact>> GetContactsAsync()
        {
            return await _context.Contacts
            .OrderBy(x => x.FirstName)
            .Include(x => x.EmailAddresses)
            .ToListAsync();
        }

        public async Task AddUpdateContactAsync(Contact contact)
        {

            if (contact.Id == Guid.Empty)
            {
                await _context.Contacts.AddAsync(contact);
            }
            else
            {
                DeleteEmailsAndAddress(contact.Id);
                _context.Contacts.Update(contact);
            }

            await _context.SaveChangesAsync();

        }

        private void DeleteEmailsAndAddress(Guid id)
        {
            var emails = _context.EmailAddresses.Where(a => a.Contact.Id == id);
            _context.EmailAddresses.RemoveRange(emails);
            var addresses = _context.Addresses.Where(a => a.Contact.Id == id);
            _context.Addresses.RemoveRange(addresses);

        }
    }
}
