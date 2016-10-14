using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebCore.API.Models;

namespace WebCore.API.Controllers
{
    [Route("api/[controller]")]
    public class NoteController : Controller
    {
        private static List<Note> _notes;
        static NoteController()
        {
            _notes = new List<Note>();
        }


        /// <summary>
        /// Returns all the available notes.
        /// </summary>
        [HttpGet]
        public IEnumerable<Note> GetAll()
        {
            return _notes.AsReadOnly();
        }

        /// <summary>
        /// Returns a particular note.
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}", Name = "GetNote")]
        public IActionResult GetById(string id)
        {
            var item = _notes.Find(n => n.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Note item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            item.Id = (_notes.Count + 1).ToString();
            _notes.Add(item);
            return CreatedAtRoute("GetNote", new { controller = "Note", id = item.Id }, item);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _notes.RemoveAll(n => n.Id == id);
        }
    }
}