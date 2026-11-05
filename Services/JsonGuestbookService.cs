using System.Text.Json;
using A3_Guestbook.Models;

namespace A3_Guestbook.Services
{
    public class JsonGuestbookService : IGuestbookService
    {
        private readonly string _filePath;

        private static string GetBasePath()
        {
            var p = Directory.GetCurrentDirectory();
            if (!Directory.Exists(p))
            {
                p = AppContext.BaseDirectory;
            }
            return p;
        }

        public JsonGuestbookService()
        {
            _filePath = Path.Combine(GetBasePath(), "AppData/guestbook.json");
            if (!File.Exists(_filePath))
            {
                throw new Exception("Could not locate guestbook!");
            }
        }

        public List<GuestbookEntry> GetAll()
        {
            string jsonString = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<GuestbookEntry>>(jsonString) ?? new();
        }

        public GuestbookEntry? GetById(int id)
        {
            List<GuestbookEntry> allEntries = GetAll();
            for (int i = 0; i < allEntries.Count; i++)
            {
                GuestbookEntry current = allEntries[i];
                if (current.Id == id)
                {
                    return current;
                }
            }
            //NOTE: Could throw here.
            return null;
        }

        public void Add(GuestbookEntry entry)
        {
            entry.Id = GetUniqueId();
            List<GuestbookEntry> allEntries = GetAll();
            allEntries.Add(entry);
            Save(allEntries);
        }

        public void Update(GuestbookEntry entry)
        {
            List<GuestbookEntry> currentEntries = GetAll();
            for (int i = 0; i < currentEntries.Count; i++)
            {
                GuestbookEntry current = currentEntries[i];
                if (current.Id == entry.Id)
                {
                    currentEntries[i] = entry;
                }
            }
            Save(currentEntries);
        }

        public void Delete(int id)
        {
            List<GuestbookEntry> newList = new();
            List<GuestbookEntry> currentEntries = GetAll();
            for (int i = 0; i < currentEntries.Count; i++)
            {
                GuestbookEntry current = currentEntries[i];
                if (current.Id != id)
                {
                    newList.Add(current);
                }
            }
            Save(newList);
        }

        /////////////
        // HELPERS //
        /////////////

        // Will look through all entries and find the max id + 1
        private int GetUniqueId()
        {
            int max = 1;
            List<GuestbookEntry> allEntries = GetAll();
            for (int i = 0; i < allEntries.Count; i++)
            {
                GuestbookEntry current = allEntries[i];
                if (current.Id > max)
                {
                    max = current.Id;
                }
            }
            return max + 1;
        }

        // call this method after you make a change to the guestbook in memory to save the new version to the file
        private void Save(List<GuestbookEntry> entries)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(entries, options);
            File.WriteAllText(_filePath, json);
        }
    }
}
