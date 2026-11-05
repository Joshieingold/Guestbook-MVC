using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        // private helper method
        // call this method after you make a change to the guestbook in memory to save the new version to the file
        private void Save(List<GuestbookEntry> entries)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(entries, options);
            File.WriteAllText(_filePath, json);
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
            // Add the entry to the list.
            // Call Save to refresh the JSON file.
        }

        public void Update(GuestbookEntry entry)
        {
            // Update the entry in the list.
            // Call Save to refresh the JSON file.
        }

        public void Delete(int id)
        {
            // Delete the entry from the list.
            // Call Save to refresh the JSON file.
        }
    }
}
