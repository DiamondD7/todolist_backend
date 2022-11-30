using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodolistsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TodolistsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Todolists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todolist>>> GetTodolists()
        {
            return await _context.Todolists.ToListAsync();
        }

        // GET: api/Todolists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Todolist>> GetTodolist(int id)
        {
            var todolist = await _context.Todolists.FindAsync(id);

            if (todolist == null)
            {
                return NotFound();
            }

            return todolist;
        }

        // PUT: api/Todolists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodolist(int id, Todolist todolist)
        { 
            if (id != todolist.TableId)
            {
                return BadRequest();
            }

            _context.Entry(todolist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodolistExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/Todolists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Todolist>> PostTodolist(Todolist todolist)
        {
            _context.Todolists.Add(todolist);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodolist", new { id = todolist.TableId }, todolist);
        }

        // DELETE: api/Todolists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodolist(int id)
        {
            var todolist = await _context.Todolists.FindAsync(id);
            if (todolist == null)
            {
                return NotFound();
            }

            _context.Todolists.Remove(todolist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodolistExists(int id)
        {
            return _context.Todolists.Any(e => e.TableId == id);
        }
    }
}
