using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentLibrary
{
    /// <summary>
    /// Абстрактний клас документа (успадкування)
    /// </summary>
    public abstract class Document
    {
        public string Title { get; set; }
        public string Author { get; set; }

        public Document(string title, string author)
        {
            Title = title;
            Author = author;
        }

        public abstract string GetInfo();
    }

    public class Book : Document
    {
        public Book(string title, string author) : base(title, author) { }
        public override string GetInfo() => $"Book: {Title} by {Author}";
    }

    public class Magazine : Document
    {
        public Magazine(string title, string author) : base(title, author) { }
        public override string GetInfo() => $"Magazine: {Title} by {Author}";
    }

    public class Thesis : Document
    {
        public Thesis(string title, string author) : base(title, author) { }
        public override string GetInfo() => $"Thesis: {Title} by {Author}";
    }

    /// <summary>
    /// Клас читацького формуляру (композиція)
    /// </summary>
    public class LibraryCard
    {
        public List<Document> BorrowedDocuments { get; private set; } = new List<Document>();

        public void Borrow(Document doc)
        {
            if (BorrowedDocuments.Count >= 5)
                throw new InvalidOperationException("Не можна брати більше 5 документів.");
            BorrowedDocuments.Add(doc);
        }

        public void Return(Document doc)
        {
            BorrowedDocuments.Remove(doc);
        }

        public bool HasDocument(Document doc) => BorrowedDocuments.Contains(doc);
    }

    /// <summary>
    /// Клас користувача
    /// </summary>
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Group { get; set; }
        public LibraryCard Card { get; private set; } = new LibraryCard();

        public User(string firstName, string lastName, string group)
        {
            FirstName = firstName;
            LastName = lastName;
            Group = group;
        }

        public string GetInfo() => $"{FirstName} {LastName}, Group: {Group}";
    }

    /// <summary>
    /// Основна логіка бібліотеки (агрегація)
    /// </summary>
    public class Library
    {
        private readonly List<User> users = new List<User>();
        private readonly List<Document> documents = new List<Document>();

        // 1.1
        public void AddUser(User user) => users.Add(user);

        // 1.2
        public void RemoveUser(User user) => users.Remove(user);

        // 1.3
        public void UpdateUser(User user, string newFirstName, string newLastName, string newGroup)
        {
            user.FirstName = newFirstName;
            user.LastName = newLastName;
            user.Group = newGroup;
        }

        // 1.4
        public User GetUser(string lastName) => users.FirstOrDefault(u => u.LastName == lastName);

        // 1.5
        public List<User> GetAllUsers() => users;

        // 1.5.1
        public List<User> SortUsersByFirstName() => users.OrderBy(u => u.FirstName).ToList();

        // 1.5.2
        public List<User> SortUsersByLastName() => users.OrderBy(u => u.LastName).ToList();

        // 1.5.3
        public List<User> SortUsersByGroup() => users.OrderBy(u => u.Group).ToList();

        // 2.1
        public void AddDocument(Document doc) => documents.Add(doc);

        // 2.2
        public void RemoveDocument(Document doc) => documents.Remove(doc);

        // 2.3
        public void UpdateDocument(Document doc, string newTitle, string newAuthor)
        {
            doc.Title = newTitle;
            doc.Author = newAuthor;
        }

        // 2.4
        public Document GetDocument(string title) => documents.FirstOrDefault(d => d.Title == title);

        // 2.5
        public List<Document> GetAllDocuments() => documents;

        // 2.5.1
        public List<Document> SortDocumentsByTitle() => documents.OrderBy(d => d.Title).ToList();

        // 2.5.2
        public List<Document> SortDocumentsByAuthor() => documents.OrderBy(d => d.Author).ToList();

        // 3.3
        public User GetOwnerOfDocument(Document doc)
        {
            return users.FirstOrDefault(u => u.Card.HasDocument(doc));
        }

        // 4.1
        public List<Document> SearchDocuments(string keyword)
        {
            return documents.Where(d => d.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                         d.Author.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // 4.2
        public List<User> SearchUsers(string keyword)
        {
            return users.Where(u => u.FirstName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                     u.LastName.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}
