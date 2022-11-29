using My_books.Data.Models;
using My_books.Data.ViewModels;
using My_books.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace My_books.Data.Services
{
    public class PublisherService
    {
        private AppDbContext _context;
        public PublisherService(AppDbContext context)
        {
            _context = context;
        }

        public Publisher AddPublisher(PublisherVM publisher)
        {
            if (StringStartWithNumber(publisher.Name)) throw new PublisherNameException("Name starts with number", publisher.Name); //Check if start with digit throw exception

            var _publisher = new Publisher()
            {
                Name = publisher.Name
                
            };
            _context.Publishers.Add(_publisher);
            _context.SaveChanges();
            return _publisher;
        }

        public Publisher GetPublisherById(int id) => _context.Publishers.FirstOrDefault(n => n.Id == id);

        public PublisherWithBooksAndAuthorsVM GetPublisherData(int publisherId)
        {
            var _publisher = _context.Publishers.Where(n => n.Id == publisherId).Select(n => new PublisherWithBooksAndAuthorsVM()
            {
                Name = n.Name,
                BookAuthors = n.Books.Select(n => new BookAuthorVM()
                {
                    BookName = n.Title,
                    BookAuthors = n.Book_Authors.Select(n => n.Author.FullName).ToList()
                }).ToList()
            }).FirstOrDefault();

            return _publisher;
        }

        public void DeletePublisherById(int id)
        {
            var publisher = _context.Publishers.FirstOrDefault(n => n.Id == id);
            if (publisher != null)
            {
                _context.Publishers.Remove(publisher);
                _context.SaveChanges();
            }
        }

        private bool StringStartWithNumber(string name)
        {
            return Regex.IsMatch(name, @"^\d"); //start with digit (return true if start with digit, and false if it doesn't)
        }
    }
}
