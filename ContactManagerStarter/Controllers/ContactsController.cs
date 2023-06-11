using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactManagerServices.Data;
using ContactManager.Hubs;
using ContactManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MailKit;
using MimeKit;
using MailKit.Net.Smtp;
using ContactManagerServices.Services;

namespace ContactManager.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ContactService contactService;
        private readonly IHubContext<ContactHub> _hubContext;

        public ContactsController(ContactService contactService, IHubContext<ContactHub> hubContext)
        {
            this.contactService = contactService;
            _hubContext = hubContext;
        }

        public async Task<IActionResult> DeleteContact(Guid id)
        {
            var contactToDelete = await contactService.GetContactAsync(id);

            if (contactToDelete == null)
            {
                return BadRequest();
            }
            await contactService.DeleteContactAsync(contactToDelete);

            await _hubContext.Clients.All.SendAsync("Update");

            return Ok();
        }

        public async Task<IActionResult> EditContact(Guid id)
        {
            var contact = await contactService.GetContactAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            var viewModel = new EditContactViewModel
            {
                Id = contact.Id,
                Title = contact.Title,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                DOB = contact.DOB,
                EmailAddresses = contact.EmailAddresses,
                Addresses = contact.Addresses
            };

            return PartialView("_EditContact", viewModel);
        }

        public async Task<IActionResult> GetContacts()
        {
            try
            {
                var contactList = await contactService.GetContactsAsync();
                var contactsVM = contactList.Select(a => new ContactViewModel(a)).ToList();

                return PartialView("_ContactTable", contactsVM);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NewContact()
        {
            return PartialView("_EditContact", new EditContactViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> SaveContact([FromBody] SaveContactViewModel model)
        {
            var contact = model.ContactId == Guid.Empty
                ? new Contact { Title = model.Title, FirstName = model.FirstName, LastName = model.LastName, DOB = model.DOB }
                : await contactService.GetContactAsync(model.ContactId);

            if (contact == null)
            {
                return NotFound();
            }

            await contactService.AddUpdateContactAsync(model.ToDataModel(contact));

            await _hubContext.Clients.All.SendAsync("Update");

            SendEmailNotification(contact.Id);

            return Ok();
        }

        private void SendEmailNotification(Guid contactId)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("noreply", "noreply@contactmanager.com"));
            message.To.Add(new MailboxAddress("SysAdmin", "Admin@contactmanager.com"));
            message.Subject = "ContactManager System Alert";

            message.Body = new TextPart("plain")
            {
                Text = "Contact with id:" + contactId.ToString() + " was updated"
            };

            using (var client = new SmtpClient())
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("127.0.0.1", 25, false);

                client.Send(message);
                client.Disconnect(true);
            }

        }

    }

}