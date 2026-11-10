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

        public IActionResult Index()
        {
            return RedirectToAction("List", "Guestbook");
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

        [HttpGet]
        public IActionResult Create()
        {
            GuestbookEntry blankEntry = new();
            return View(blankEntry);
        }

        [HttpPost]
        public IActionResult Create(GuestbookEntry gbe)
        {
            if (!ModelState.IsValid)
            {
                return View(gbe);
            }
            _service.Add(gbe);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            // This would mean the model in the JSON is wrong. I think just
            // Go back to the index is best
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            var entry = _service.GetById(id);
            return View(entry);
        }

        [HttpPost]
        public IActionResult Edit(GuestbookEntry gbe)
        {
            if (!ModelState.IsValid)
            {
                // In this case they should be brought back to the edit screen
                return Edit(gbe.Id);
            }
            _service.Update(gbe);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var entry = _service.GetById(id);
            return View(entry);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    } // end class
}
