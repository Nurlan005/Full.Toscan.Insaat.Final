using Microsoft.AspNetCore.Mvc;
using Toscan_Insaat_Final.Data;
using Toscan_Insaat_Final.Models;

namespace Toscan_Insaat_Final.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly ApplicationContext _context;

        public ContactController(ApplicationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Contact> result = _context.Contacts.ToList();
            return View(result);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Contact contact = _context.Contacts.FirstOrDefault(x => x.Id == id);
            _context.Contacts.Remove(contact);
            _context.SaveChanges();
            return RedirectToAction("Index", "Contact");

        }
    }
}
