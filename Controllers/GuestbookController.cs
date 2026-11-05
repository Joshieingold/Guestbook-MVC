using Microsoft.AspNetCore.Mvc;
using A3_Guestbook.Models;
using A3_Guestbook.Services;

namespace A3_Guestbook.Controllers
{
    public class GuestbookController : Controller
    {
        private readonly IGuestbookService _service;

        public GuestbookController(IGuestbookService service)
        {
            _service = service;
        }

        public IActionResult List()
        {
            var entries = _service.GetAll();
            return View(entries);
        }

    } // end class
}
