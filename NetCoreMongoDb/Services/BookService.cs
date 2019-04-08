using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using NetCoreMongoDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreMongoDb.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;
        public BookService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("BookStoreDb"));
            var database = client.GetDatabase("BookStoreDb");
            _books = database.GetCollection<Book>("Books");
        }
        public async Task<List<Book>> Get()
        {
            return await _books.Find(book => true).ToListAsync();
        }
        public async Task<Book> Get(string id)
        {
            return await _books.Find<Book>(book => book.Id == id).SingleOrDefaultAsync();
        }
        public async Task<Book> Create(Book book)
        {
            await _books.InsertOneAsync(book);
            return book;
        }
        public async void Update(string id,Book bookIn)
        {
            await _books.ReplaceOneAsync(book => book.Id == id, bookIn);
        } 
        public async void Remove(Book bookIn)
        {
            await _books.DeleteOneAsync(book=>book.Id==bookIn.Id);
        }
        public async void Remove(string id)
        {
            await _books.DeleteOneAsync(book => book.Id == id);
        }
    }
}
