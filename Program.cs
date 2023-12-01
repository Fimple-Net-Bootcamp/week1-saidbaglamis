using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Phonebook phonebook = new Phonebook();

        while (true)
        {
            Console.WriteLine("Phonebook Menu:");
            Console.WriteLine("1. Save a phone number");
            Console.WriteLine("2. Delete a phone number");
            Console.WriteLine("3. Update a phone number");
            Console.WriteLine("4. List phone numbers");
            Console.WriteLine("5. Search in list");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice (1-6): ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    phonebook.SavePhoneNumber();
                    break;

                case "2":
                    phonebook.DeletePhoneNumber();
                    break;

                case "3":
                    phonebook.UpdatePhoneNumber();
                    break;

                case "4":
                    phonebook.ListPhoneNumbers();
                    break;

                case "5":
                    phonebook.SearchInList();
                    break;

                case "6":
                    Console.WriteLine("Exiting the program. Goodbye!");
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                    break;
            }

            Console.WriteLine(); // Add a line break for better readability
        }
    }
}

class Phonebook
{
    private Dictionary<string, string> phoneNumbers = new Dictionary<string, string>();

    public void SavePhoneNumber()
    {
        Console.Write("Enter name: ");
        string name = Console.ReadLine();

        Console.Write("Enter surname: ");
        string surname = Console.ReadLine();

        Console.Write("Enter phone number: ");
        string phoneNumber = Console.ReadLine();

        string fullName = $"{name} {surname}";
        phoneNumbers[fullName] = phoneNumber;

        Console.WriteLine($"Phone number for {fullName} saved successfully.");
    }

    public void DeletePhoneNumber()
    {
        Console.Write("Enter name or surname to delete: ");
        string searchQuery = Console.ReadLine();

        var results = phoneNumbers.Where(entry =>
            entry.Key.ToLower().Contains(searchQuery.ToLower()) ||
            entry.Value.Contains(searchQuery)
        ).ToList();

        if (results.Count == 0)
        {
            Console.WriteLine($"No matching entries found for {searchQuery}.");

            Console.WriteLine("Options:");
            Console.WriteLine("1. Terminate the delete operation");
            Console.WriteLine("2. Try again");

            Console.Write("Enter your choice (1 or 2): ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.WriteLine("Delete operation terminated.");
                    return;

                case "2":
                    DeletePhoneNumber();
                    return;

                default:
                    Console.WriteLine("Invalid option. Delete operation terminated.");
                    return;
            }
        }

        foreach (var entry in results)
        {
            Console.WriteLine($"Found: {entry.Key}: {entry.Value}");
            Console.Write($"Do you want to continue with the deletion? (y/n): ");
            string confirm = Console.ReadLine().ToLower();

            if (confirm == "y")
            {
                phoneNumbers.Remove(entry.Key);
                Console.WriteLine($"Phone number for {entry.Key} deleted successfully.");
            }
            else
            {
                Console.WriteLine("Delete operation canceled.");
            }
        }
    }

    public void UpdatePhoneNumber()
    {
        Console.Write("Enter name or surname to update: ");
        string searchQuery = Console.ReadLine();

        var results = phoneNumbers.Where(entry =>
            entry.Key.ToLower().Contains(searchQuery.ToLower()) ||
            entry.Value.Contains(searchQuery)
        ).ToList();

        if (results.Count == 0)
        {
            Console.WriteLine($"No matching entries found for {searchQuery}.");

            Console.WriteLine("Options:");
            Console.WriteLine("1. Terminate the update operation");
            Console.WriteLine("2. Try again");

            Console.Write("Enter your choice (1 or 2): ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.WriteLine("Update operation terminated.");
                    return;

                case "2":
                    UpdatePhoneNumber();
                    return;

                default:
                    Console.WriteLine("Invalid option. Update operation terminated.");
                    return;
            }
        }

        Console.WriteLine($"Found: {results.First().Key}: {results.First().Value}");

        Console.Write($"Enter new phone number for {results.First().Key}: ");
        string newPhoneNumber = Console.ReadLine();

        phoneNumbers[results.First().Key] = newPhoneNumber;

        Console.WriteLine($"Phone number for {results.First().Key} updated successfully.");
    }

    public void ListPhoneNumbers()
    {
        Console.WriteLine("Phone Numbers:");

        foreach (var entry in phoneNumbers.OrderBy(entry => entry.Key))
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}");
        }
    }

    public void SearchInList()
    {
        Console.WriteLine("Search options:");
        Console.WriteLine("1. Search with name or surname");
        Console.WriteLine("2. Search with phone number");
        Console.Write("Enter your choice (1 or 2): ");
        string searchOption = Console.ReadLine();

        switch (searchOption)
        {
            case "1":
                Console.Write("Enter name or surname to search: ");
                string searchQuery = Console.ReadLine();

                var results = phoneNumbers.Where(entry =>
                    entry.Key.ToLower().Contains(searchQuery.ToLower()) ||
                    entry.Value.Contains(searchQuery)
                );

                DisplaySearchResults(results);
                break;

            case "2":
                Console.Write("Enter phone number to search: ");
                string phoneNumberQuery = Console.ReadLine();

                var phoneNumberResults = phoneNumbers.Where(entry => entry.Value.Contains(phoneNumberQuery));

                DisplaySearchResults(phoneNumberResults);
                break;

            default:
                Console.WriteLine("Invalid search option.");
                break;
        }
    }

    private void DisplaySearchResults(IEnumerable<KeyValuePair<string, string>> results)
    {
        if (results.Any())
        {
            Console.WriteLine("Search results:");

            foreach (var entry in results)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value}");
            }
        }
        else
        {
            Console.WriteLine("No matching results found.");
        }
    }
}

