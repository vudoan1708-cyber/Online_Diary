using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DailyDiary.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Http;

namespace DailyDiary.Pages.DiaryContent
{
    public class NewContentModel : PageModel
    {

        private readonly ApplicationDbContext _db;

        private readonly IWebHostEnvironment _hostingEnvironment;

        public NewContentModel(ApplicationDbContext db, IWebHostEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
        }


        [BindProperty]
        public Diary Diary { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {

            //Check For Validation
            if (ModelState.IsValid)
            {

                // Check if there is an image file inputted in the field
                // By checking if the asp-for value "Photo" is ever called
                if (Photo != null)
                {

                    // Change the typical url and 
                    // Save the image to the img folder in the static wwwroot folder
                    Diary.ImageUrl = "/img/" + SaveImageFile();
                }

                // Queue up the posted data 
                await _db.Diary.AddAsync(Diary);

                // Push the data to the database
                await _db.SaveChangesAsync();

                // Return back to the Index page
                return RedirectToPage("Index");
            } else
            {
                return Page();
            }

        }

        public string SaveImageFile()
        {
            string filename = null;

            // If the image url is not null
            if (Photo != null)
            {
                // Destination folder
                string UPLOAD_DIR = Path.Combine(_hostingEnvironment.WebRootPath, "img");

                // Generate a unique id for the image name
                filename = Guid.NewGuid().ToString() + "-" + Photo.FileName;
                string DESTINATION_PATH = Path.Combine(UPLOAD_DIR, filename);

                // Create new physical file and write it to the destination path, if the file exists overwrite it
                using var fileStream = new FileStream(DESTINATION_PATH, FileMode.Create);
                Photo.CopyTo(fileStream);
            }

            return filename;
        }
    }
}
