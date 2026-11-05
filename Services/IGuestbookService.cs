using System.Collections.Generic;
using A3_Guestbook.Models;

namespace A3_Guestbook.Services
{
    public interface IGuestbookService
    {
        List<GuestbookEntry> GetAll();
        GuestbookEntry? GetById(int id);
        void Add(GuestbookEntry entry);
        void Update(GuestbookEntry entry);
        void Delete(int id);
    }
}
