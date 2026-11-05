using A3_Guestbook.Models;
using A3_Guestbook.Services;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Details(int id)
        {
            var entry = _service.GetById(id);
            return View(entry);
        }

        public IActionResult Create()
        {
            GuestbookEntry blankEntry = new();
            return View(blankEntry);
        }

        public IActionResult Edit(int id)
        {
            var entry = _service.GetById(id);
            return View(entry);
        }

        public IActionResult Delete(int id)
        {
            var entry = _service.GetById(id);
            return View(entry);
        }

        [HttpPost]
        public IActionResult OnPostAdd(GuestbookEntry inEntry)
        {
            if (!ModelState.IsValid)
            {
                return View(inEntry);
            }
            _service.Add(inEntry);
            // bool success = _service.Add(inEntry);
            // if (!success)
            // {
            //     ModelState.AddModelError("IDK", "idk");
            // }
            TempData["Message"] = "Congrats you added a new entry!";

            return List();
        }

        [HttpPost]
        public IActionResult OnPostDelete(int deleteId)
        {
            if (!ModelState.IsValid)
            {
                return Delete(deleteId);
            }
            _service.Delete(deleteId);
            // bool success = _service.Delete(inEntry);
            // if (!success)
            // {
            //     ModelState.AddModelError("IDK", "idk");
            // }
            TempData["Message"] = "Congrats you deleted the entry!";

            return List();
        }
    } // end class
}
