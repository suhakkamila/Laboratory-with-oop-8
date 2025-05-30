using System;
using System.Collections.Generic;
using StudentLibrary;

class Program
{
    static Library library = new Library();

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        while (true)
        {
            Console.WriteLine("\n=== Студентська бібліотека ===");
            Console.WriteLine("1.1 Додати користувача");
            Console.WriteLine("1.2 Видалити користувача");
            Console.WriteLine("1.3 Змінити дані користувача");
            Console.WriteLine("1.4 Переглянути користувача");
            Console.WriteLine("1.5 Переглянути список усіх користувачів");
            Console.WriteLine("1.5.1 Сортувати по імені");
            Console.WriteLine("1.5.2 Сортувати по прізвищу");
            Console.WriteLine("1.5.3 Сортувати по групі");
            Console.WriteLine("2.1 Додати документ");
            Console.WriteLine("2.2 Видалити документ");
            Console.WriteLine("2.3 Змінити документ");
            Console.WriteLine("2.4 Переглянути документ");
            Console.WriteLine("2.5 Переглянути список усіх документів");
            Console.WriteLine("2.5.1 Сортувати по назві");
            Console.WriteLine("2.5.2 Сортувати по автору");
            Console.WriteLine("3.1 Видати документ користувачу");
            Console.WriteLine("3.2 Переглянути документи користувача");
            Console.WriteLine("3.3 Хто має документ?");
            Console.WriteLine("3.4 Повернути документ");
            Console.WriteLine("4.1 Пошук серед документів");
            Console.WriteLine("4.2 Пошук серед користувачів");
            Console.WriteLine("0. Вийти\n");

            Console.Write("Виберіть пункт: ");
            string input = Console.ReadLine();
            Console.WriteLine();

            switch (input)
            {
                case "1.1":
                    AddUser();
                    break;
                case "1.2":
                    RemoveUser();
                    break;
                case "1.3":
                    UpdateUser();
                    break;
                case "1.4":
                    ViewUser();
                    break;
                case "1.5":
                    ListUsers();
                    break;
                case "1.5.1":
                    SortUsers("first");
                    break;
                case "1.5.2":
                    SortUsers("last");
                    break;
                case "1.5.3":
                    SortUsers("group");
                    break;
                case "2.1":
                    AddDocument();
                    break;
                case "2.2":
                    RemoveDocument();
                    break;
                case "2.3":
                    UpdateDocument();
                    break;
                case "2.4":
                    ViewDocument();
                    break;
                case "2.5":
                    ListDocuments();
                    break;
                case "2.5.1":
                    SortDocuments("title");
                    break;
                case "2.5.2":
                    SortDocuments("author");
                    break;
                case "3.1":
                    IssueDocument();
                    break;
                case "3.2":
                    ListUserDocuments();
                    break;
                case "3.3":
                    WhoHasDocument();
                    break;
                case "3.4":
                    ReturnDocument();
                    break;
                case "4.1":
                    SearchDocuments();
                    break;
                case "4.2":
                    SearchUsers();
                    break;
                case "0": return;
                default:
                    Console.WriteLine("Невірна команда");
                    break;
            }
        }
    }

    static void AddUser()
    {
        Console.Write("Ім’я: ");
        string fn = Console.ReadLine();
        Console.Write("Прізвище: ");
        string ln = Console.ReadLine();
        Console.Write("Група: ");
        string gr = Console.ReadLine();
        library.AddUser(new User(fn, ln, gr));
        Console.WriteLine("Користувача додано.");
    }

    static void RemoveUser()
    {
        Console.Write("Прізвище для видалення: ");
        var user = library.GetUser(Console.ReadLine());
        if (user != null)
        {
            library.RemoveUser(user);
            Console.WriteLine("Видалено.");
        }
        else Console.WriteLine("Не знайдено.");
    }

    static void UpdateUser()
    {
        Console.Write("Прізвище для оновлення: ");
        var user = library.GetUser(Console.ReadLine());
        if (user != null)
        {
            Console.Write("Нове ім’я: ");
            string fn = Console.ReadLine();
            Console.Write("Нове прізвище: ");
            string ln = Console.ReadLine();
            Console.Write("Нова група: ");
            string gr = Console.ReadLine();
            library.UpdateUser(user, fn, ln, gr);
            Console.WriteLine("Оновлено.");
        }
        else Console.WriteLine("Не знайдено.");
    }

    static void ViewUser()
    {
        Console.Write("Прізвище: ");
        var user = library.GetUser(Console.ReadLine());
        Console.WriteLine(user?.GetInfo() ?? "Не знайдено.");
    }

    // Метод для виводу всіх користувачів
    static void ListUsers()
    {
        var users = library.GetAllUsers();
        if (users.Count == 0)
        {
            Console.WriteLine("Немає жодного користувача.");
            return;
        }

        foreach (var u in users) Console.WriteLine(u.GetInfo());
    }

// Метод сортування користувачів
    static void SortUsers(string by)
    {
        var all = library.GetAllUsers();
        if (all.Count == 0)
        {
            Console.WriteLine("Немає жодного користувача для сортування.");
            return;
        }

        List<User> sorted = by switch
        {
            "first" => library.SortUsersByFirstName(),
            "last" => library.SortUsersByLastName(),
            _ => library.SortUsersByGroup()
        };
        foreach (var u in sorted) Console.WriteLine(u.GetInfo());
    }

    static void AddDocument()
    {
        Console.Write("Тип (book/magazine/thesis): ");
        string type = Console.ReadLine();
        Console.Write("Назва: ");
        string title = Console.ReadLine();
        Console.Write("Автор: ");
        string author = Console.ReadLine();

        Document doc = type switch
        {
            "book" => new Book(title, author),
            "magazine" => new Magazine(title, author),
            "thesis" => new Thesis(title, author),
            _ => null
        };

        if (doc != null)
        {
            library.AddDocument(doc);
            Console.WriteLine("Документ додано.");
        }
        else Console.WriteLine("Невірний тип.");
    }

    static void RemoveDocument()
    {
        Console.Write("Назва для видалення: ");
        var doc = library.GetDocument(Console.ReadLine());
        if (doc != null)
        {
            library.RemoveDocument(doc);
            Console.WriteLine("Видалено.");
        }
        else Console.WriteLine("Не знайдено.");
    }

    static void UpdateDocument()
    {
        Console.Write("Назва документа: ");
        var doc = library.GetDocument(Console.ReadLine());
        if (doc != null)
        {
            Console.Write("Нова назва: ");
            string title = Console.ReadLine();
            Console.Write("Новий автор: ");
            string author = Console.ReadLine();
            library.UpdateDocument(doc, title, author);
            Console.WriteLine("Оновлено.");
        }
        else Console.WriteLine("Не знайдено.");
    }

    static void ViewDocument()
    {
        Console.Write("Назва документа: ");
        var doc = library.GetDocument(Console.ReadLine());
        Console.WriteLine(doc?.GetInfo() ?? "Не знайдено.");
    }

    // Метод для перегляду всіх документів
    static void ListDocuments()
    {
        var docs = library.GetAllDocuments();
        if (docs.Count == 0)
        {
            Console.WriteLine("Документів ще не існує.");
            return;
        }

        foreach (var d in docs) Console.WriteLine(d.GetInfo());
    }

// Метод сортування документів
    static void SortDocuments(string by)
    {
        var docs = library.GetAllDocuments();
        if (docs.Count == 0)
        {
            Console.WriteLine("Документів ще не існує для сортування.");
            return;
        }

        List<Document> sorted = by switch
        {
            "title" => library.SortDocumentsByTitle(),
            _ => library.SortDocumentsByAuthor()
        };
        foreach (var d in sorted) Console.WriteLine(d.GetInfo());
    }

    static void IssueDocument()
    {
        Console.Write("Прізвище користувача: ");
        var user = library.GetUser(Console.ReadLine());
        Console.Write("Назва документа: ");
        var doc = library.GetDocument(Console.ReadLine());

        if (user != null && doc != null)
        {
            try
            {
                user.Card.Borrow(doc);
                Console.WriteLine("Документ видано.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        else Console.WriteLine("Користувача або документ не знайдено.");
    }

    // Метод для перегляду документів користувача
    static void ListUserDocuments()
    {
        Console.Write("Прізвище користувача: ");
        var user = library.GetUser(Console.ReadLine());
        if (user != null)
        {
            if (user.Card.BorrowedDocuments.Count == 0)
            {
                Console.WriteLine("Користувач не має жодного документа.");
                return;
            }

            foreach (var doc in user.Card.BorrowedDocuments)
                Console.WriteLine(doc.GetInfo());
        }
        else Console.WriteLine("Користувача не знайдено.");
    }

// Метод для перевірки, хто має документ
    static void WhoHasDocument()
    {
        Console.Write("Назва документа: ");
        var doc = library.GetDocument(Console.ReadLine());
        if (doc == null)
        {
            Console.WriteLine("Документ не знайдено.");
            return;
        }

        var owner = library.GetOwnerOfDocument(doc);
        Console.WriteLine(owner != null ? owner.GetInfo() : "Ніхто не має цього документа.");
    }

    static void ReturnDocument()
    {
        Console.Write("Прізвище користувача: ");
        var user = library.GetUser(Console.ReadLine());
        Console.Write("Назва документа: ");
        var doc = library.GetDocument(Console.ReadLine());
        if (user != null && doc != null && user.Card.HasDocument(doc))
        {
            user.Card.Return(doc);
            Console.WriteLine("Повернено.");
        }
        else Console.WriteLine("Не знайдено або документ не належить користувачу");
    }

    // Пошук документів
    static void SearchDocuments()
    {
        var all = library.GetAllDocuments();
        if (all.Count == 0)
        {
            Console.WriteLine("Немає документів для пошуку.");
            return;
        }

        Console.Write("Ключове слово: ");
        var results = library.SearchDocuments(Console.ReadLine());
        if (results.Count == 0)
        {
            Console.WriteLine("Документів за цим ключовим словом не знайдено.");
            return;
        }

        foreach (var doc in results) Console.WriteLine(doc.GetInfo());
    }

    // Пошук користувачів
    static void SearchUsers()
    {
        var all = library.GetAllUsers();
        if (all.Count == 0)
        {
            Console.WriteLine("Немає користувачів для пошуку.");
            return;
        }

        Console.Write("Ключове слово: ");
        var results = library.SearchUsers(Console.ReadLine());
        if (results.Count == 0)
        {
            Console.WriteLine("Користувачів за цим ключовим словом не знайдено.");
            return;
        }

        foreach (var user in results) Console.WriteLine(user.GetInfo());
    }
}

