using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DailyDiary.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore;

namespace DailyDiary.Pages.DiaryContent
{
    public class AddedContentModel : PageModel
    {

        // Dependency Injection
        private readonly ApplicationDbContext _db;

        // Contructor
        public AddedContentModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public Diary AddedDiary { get; set; }
        
        public async Task OnGet(int id)
        {

            // Check the variable type from the passing parameter
            Type t = id.GetType();

            if (t.Equals(typeof(int)) | t.Equals(typeof(long))) {
                AddedDiary = await _db.Diary.FindAsync(id);
            }
        }
    }
}
