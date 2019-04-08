using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreMongoDb.Models;
using NetCoreMongoDb.Services;

namespace NetCoreMongoDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;
        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Book>>> Get()
        {
            return await _bookService.Get();
        }
        [HttpGet("{id:length(24)}",Name ="GetBook")]
        public async Task<ActionResult<Book>> Get(string id)
        {
            if (id == null)
                return NotFound();
            var book = await _bookService.Get(id);
            if (book == null)
                return NotFound();
            return book;
        }
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult> Update(string id,Book bookIn)
        {
            var book =await _bookService.Get(id);
            if (book == null)
                return NotFound();
             _bookService.Update(id, bookIn);
            return NoContent();
        }
        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult> Delete(string id)
        {
            var book = await _bookService.Get(id);
            if (book == null)
                return NotFound();
            _bookService.Remove(id);
            return NoContent();
        }
    }
}