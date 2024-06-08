using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using WebApplication1.Data;
using WebApplication1.Entity;

namespace WebApplication1.Controllers
{
    public class ContactsController : Controller
    {
        private AppDBContext _context;

        public ContactsController(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var contacts = await _context.Contacts.ToListAsync();
            return View(contacts);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Contacts.AddAsync(contact);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Что-то пошло не так {ex.Message}");
                }
            }
            ModelState.AddModelError(string.Empty, $"Что-то пошло не так, недопустимая модель");

            return View(contact);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == id);

            return View(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var contactToEdit = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == contact.Id);

                    if (contactToEdit != null)
                    {
                        contactToEdit.FirstName = contact.FirstName;
                        contactToEdit.LastName = contact.LastName;
                        contactToEdit.Mobile = contact.Mobile;
                        contactToEdit.Email = contact.Email;

                        await _context.SaveChangesAsync();

                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Что-то пошло не так {ex.Message}");
                }
            }

            ModelState.AddModelError(string.Empty, $"Что-то пошло не так, недопустимая модель");

            return View(contact);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == id);

            return View(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Contact contact)
        {
         
                try
                {
                    var contactToDelete = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == contact.Id);

                    if (contactToDelete != null)
                    {
                        _context.Contacts.Remove(contactToDelete);
                        await _context.SaveChangesAsync();

                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Что-то пошло не так {ex.Message}");
                }
            
            return View();
        }

    }
}



