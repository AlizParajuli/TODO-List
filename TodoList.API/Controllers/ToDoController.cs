using Microsoft.AspNetCore.Mvc;
using TodoList.API.Data;
using TodoList.Models;

namespace TodoList.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly DataContext _context;

        public ToDoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ToDoItem>> GetIncompleteItems()
        {
            return _context.ToDoItems.Where(t => t.CompletedDate == null).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<ToDoItem> GetItemById(int id)
        {
            var item = _context.ToDoItems.Find(id);
            if (item == null) return NotFound();
            return item;
        }

        [HttpPost]
        public ActionResult<ToDoItem> AddItem(ToDoItem item)
        {
            _context.ToDoItems.Add(item);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetItemById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult MarkAsCompleted(int id)
        {
            var item = _context.ToDoItems.Find(id);
            if (item == null) return NotFound();
            item.CompletedDate = DateTime.UtcNow;
            _context.SaveChanges();
            return NoContent();
        }
    }
}