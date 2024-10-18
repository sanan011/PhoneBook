using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using PhonebookApp.Models;

namespace PhonebookApp.Controllers
{
    public class ContactController
    {
        private readonly string _filePath;

        public List<Contact> Contacts { get; private set; }

        public ContactController()
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Contact");
            _filePath = Path.Combine(folderPath, "contacts.txt");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                Console.WriteLine($"Directory created: {folderPath}");
            }
            else
            {
                Console.WriteLine($"Directory exists: {folderPath}");
            }

            Contacts = LoadContactsFromFile();
        }

        public void AddContact(Contact contact)
        {
            Contacts.Add(contact);
            Console.WriteLine("Contact added successfully.");
        }

        public void UpdateContact(int index, Contact updatedContact)
        {
            if (index >= 0 && index < Contacts.Count)
            {
                Contacts[index] = updatedContact;
                Console.WriteLine("Contact updated successfully.");
            }
            else
            {
                Console.WriteLine("Invalid index. No contact found at the given index.");
            }
        }

        public void DeleteContact(int index)
        {
            if (index >= 0 && index < Contacts.Count)
            {
                Contacts.RemoveAt(index);
                Console.WriteLine("Contact deleted successfully.");
            }
            else
            {
                Console.WriteLine("Invalid index. No contact found at the given index.");
            }
        }

        public void DisplayContacts()
        {
            if (Contacts.Count == 0)
            {
                Console.WriteLine("No contacts available.");
            }
            else
            {
                for (int i = 0; i < Contacts.Count; i++)
                {
                    Console.WriteLine($"{i}. {Contacts[i]}");
                }
            }
        }

        public void SaveContactsToFile()
        {
            if (Contacts.Count > 0)
            {
                string json = JsonConvert.SerializeObject(Contacts, Formatting.Indented);
                File.WriteAllText(_filePath, json);
                Console.WriteLine($"Contacts saved to file: {_filePath}");
            }
            else
            {
                Console.WriteLine("No contacts to save.");
            }
        }

        private List<Contact> LoadContactsFromFile()
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);

                if (!string.IsNullOrEmpty(json))
                {
                    Console.WriteLine("Contacts loaded from file.");
                    return JsonConvert.DeserializeObject<List<Contact>>(json) ?? new List<Contact>();
                }
                else
                {
                    Console.WriteLine("File is empty. No contacts loaded.");
                    return new List<Contact>();
                }
            }
            else
            {
                Console.WriteLine("File does not exist. No contacts loaded.");
                return new List<Contact>();
            }
        }
    }
}
