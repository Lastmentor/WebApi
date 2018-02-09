using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GithubWebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GithubWebApi.Controllers
{
    [Route("api/[controller]")]
    public class StudentsController : Controller
    {
        private readonly DatabaseContext _context;
        public StudentsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/students
        [HttpGet]
        public IEnumerable<Students> Get()
        {
            return _context.Students.ToList();
        }

        // GET api/students/1
        [HttpGet("{id}", Name = "GetStudent")]
        public IActionResult GetById(int id)
        {
            var item = _context.Students.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST api/students
        [HttpPost]
        public IActionResult Post([FromBody] Students item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.Students.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetStudent", new { id = item.Id }, item);
        }

        // PUT api/students/
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Students item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }
            var foundStudent = _context.Students.FirstOrDefault(t => t.Id == id);
            if (foundStudent == null)
            {
                return NotFound();
            }

            foundStudent.FirstName = item.FirstName;
            foundStudent.LastName = item.LastName;
            foundStudent.Department = item.Department;

            _context.Students.Update(foundStudent);
            _context.SaveChanges();
            return new NoContentResult();
        }

        // DELETE api/students/1
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Students student = _context.Students.Where(x => x.Id == id).FirstOrDefault();
            if (student != null)
            {
                _context.Students.Remove(student);
            }
            _context.SaveChanges();
        }
    }
}
