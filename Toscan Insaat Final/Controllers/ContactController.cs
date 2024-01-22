using Microsoft.AspNetCore.Mvc;
using Toscan_Insaat_Final.Data;
using Toscan_Insaat_Final.Dtos.ContactDtos;
using Toscan_Insaat_Final.Models;

namespace Toscan_Insaat_Final.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationContext _context;

        public ContactController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public  IActionResult Index(Contact contact)
        {
            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
