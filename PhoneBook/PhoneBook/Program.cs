using System;
using PhonebookApp.Controllers;
using PhonebookApp.Models;
using System.Text.RegularExpressions;

namespace PhonebookApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ContactController contactController = new ContactController();
            bool running = true;

            while (running)
            {
                Console.WriteLine("Phonebook:");
                Console.WriteLine("1. Display Contacts");
                Console.WriteLine("2. Add Contact");
                Console.WriteLine("3. Update Contact");
                Console.WriteLine("4. Delete Contact");
                Console.WriteLine("5. Exit");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        contactController.DisplayContacts();
                        break;

                    case 2:
                        Console.WriteLine("Enter Name:");
                        string name = Console.ReadLine();

                        string phoneNumber = GetValidAzerbaijaniPhoneNumber();
                        string email = GetValidEmail();

                        Contact newContact = new Contact(name, phoneNumber, email);
                        contactController.AddContact(newContact);
                        break;

                    case 3:
                        contactController.DisplayContacts();
                        Console.WriteLine("Enter index of contact to update:");
                        int updateIndex = int.Parse(Console.ReadLine());

                        Console.WriteLine("Enter new Name:");
                        string newName = Console.ReadLine();

                        string newPhoneNumber = GetValidAzerbaijaniPhoneNumber();
                        string newEmail = GetValidEmail();

                        Contact updatedContact = new Contact(newName, newPhoneNumber, newEmail);
                        contactController.UpdateContact(updateIndex, updatedContact);
                        break;

                    case 4:
                        contactController.DisplayContacts();
                        Console.WriteLine("Enter index of contact to delete:");
                        int deleteIndex = int.Parse(Console.ReadLine());
                        contactController.DeleteContact(deleteIndex);
                        break;

                    case 5:
                        contactController.SaveContactsToFile();
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice, try again.");
                        break;
                }
            }
        }

        static string GetValidAzerbaijaniPhoneNumber()
        {
            while (true)
            {
                Console.WriteLine("Enter Phone Number (e.g., +994XXXXXXXXX):");
                string phoneNumber = Console.ReadLine();

                if (IsAzerbaijaniPhoneNumber(phoneNumber))
                {
                    return phoneNumber;
                }
                else
                {
                    Console.WriteLine("Invalid Azerbaijani phone number. Please try again.");
                }
            }
        }

        static bool IsAzerbaijaniPhoneNumber(string phoneNumber)
        {
            return phoneNumber.StartsWith("+994") && phoneNumber.Length == 13 && Regex.IsMatch(phoneNumber, @"^\+994\d{9}$");
        }

        static string GetValidEmail()
        {
            while (true)
            {
                Console.WriteLine("Enter Email (must be in the form example@gmail.com):");
                string email = Console.ReadLine();

                if (IsValidEmail(email))
                {
                    return email;
                }
                else
                {
                    Console.WriteLine("Invalid email format. Please enter a valid email like example@gmail.com.");
                }
            }
        }

        static bool IsValidEmail(string email)
        {
            return email.EndsWith("@gmail.com") && Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@gmail\.com$");
        }
    }
}
