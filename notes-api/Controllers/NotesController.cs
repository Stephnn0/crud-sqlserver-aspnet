using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using notes_api.Data;
using notes_api.Models;

namespace notes_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {

        private readonly DataContext _dbContext;

        public NotesController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }



        [HttpGet]
        public async Task<ActionResult<List<Notes>>> GetNotes()
        {
            var notes = await _dbContext.Note.ToListAsync();
            return Ok(notes);
        }


        //create request object for not include id
        [HttpPost]
        public async Task<ActionResult<List<Notes>>> AddNote(Notes note)
        {
            _dbContext.Note.Add(note);
            await _dbContext.SaveChangesAsync();
            return Ok(await _dbContext.Note.ToListAsync());

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<List<Notes>>> UpdateNote(int id)
        {
            var note = await _dbContext.Note.FindAsync(id);

            if(note is null)
            {
                return BadRequest("Note not found");
            }

            return Ok(note);
        }

        [HttpPut]
        public async Task<ActionResult<List<Notes>>> UpdateNote(Notes updatedNote)
        {
            var dbNote = await _dbContext.Note.FindAsync(updatedNote.Id);
            if(dbNote is null)
            {
                return NotFound("note not found");
            }
            dbNote.Name = updatedNote.Name;
            dbNote.Description = updatedNote.Description;

            await _dbContext.SaveChangesAsync();
            
            return Ok(await _dbContext.Note.ToListAsync());

        }


        [HttpDelete]
        public async Task<ActionResult<List<Notes>>> DeleteNote(int id)
        {
            var dbNote = await _dbContext.Note.FindAsync(id);
            if (dbNote is null)
            {
                return NotFound("note not found");
            }
            _dbContext.Note.Remove(dbNote);

            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Note.ToListAsync());

        }













    }
}
