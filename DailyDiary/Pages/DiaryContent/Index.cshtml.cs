using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DailyDiary.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DailyDiary.Pages.DiaryContent
{
    public class IndexModel : PageModel
    {

        // Dependency Injection
        private readonly ApplicationDbContext _db;

        // Contructor
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Diary> Diaries { get; set; }

        // Extract Diaries Data from the Diary model in the database
        public async Task OnGet()
        {

            // Get the Diary model and turn it into a list (IEnumerable) 
            Diaries = await _db.Diary.ToListAsync();
        }

        // When the delete button gets clicked
        public async Task<IActionResult> OnPostDelete(int id)
        {

            // Find the data with the corresponding ID number
            var DeletedDiary = _db.Diary.Find(id);

            if (DeletedDiary != null)
            {

                // Remove it from the Diary model
                _db.Diary.Remove(DeletedDiary);

                // Push the changes to the database
                await _db.SaveChangesAsync();

                return RedirectToPage();
            } else
            {
                return NotFound();
            }
        }
    }
}