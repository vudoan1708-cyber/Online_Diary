using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using DailyDiary.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DailyDiary.Pages.DiaryContent
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnv;

        public EditModel(ApplicationDbContext db, IWebHostEnvironment hostEnv)
        {
            _db = db;
            _hostEnv = hostEnv;
        }

        [BindProperty]
        public Diary EditedDiary { get; set; }

        [BindProperty]
        public IFormFile UpdatedPhoto { get; set; }

        public async Task OnGet(int id)
        {
            
            // Find the data that corresponds to the ID number
            EditedDiary = await _db.Diary.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {

            if (ModelState.IsValid)
            {

                var DiaryFromDb = await _db.Diary.FindAsync(EditedDiary.ID);

                DiaryFromDb.Title = EditedDiary.Title;
                DiaryFromDb.Author = EditedDiary.Author;
                DiaryFromDb.Content = EditedDiary.Content;
                DiaryFromDb.Date = EditedDiary.Date;

                //Console.WriteLine("{0}, {1}", DiaryFromDb.ImageUrl, EditedDiary.ImageUrl);

                // If there is any updates to the image
                if (UpdatedPhoto != null)
                {

                    // Check for a scenario where there was an image from the previous content
                    if (DiaryFromDb.ImageUrl != "" | DiaryFromDb.ImageUrl != null)
                    {
                        string[] Splitted_ImgUrl = DiaryFromDb.ImageUrl.Split("/");

                        // Get the full path to where the image located
                        string IMG_PATH = Path.Combine(_hostEnv.WebRootPath, "img", Splitted_ImgUrl[Splitted_ImgUrl.Length - 1]);

                        // Delete it
                        System.IO.File.Delete(IMG_PATH);
                    }
                       
                    // Create an image file and concatenate the path to the image
                    DiaryFromDb.ImageUrl = "/img/" + SaveChangedImage();
                }

                await _db.SaveChangesAsync();

                // Redirect back to the AddedContent page with updated info 
                //by passing an anonymous object that contains the ID number
                return RedirectToPage("AddedContent", new { id = DiaryFromDb.ID });
            } 
            return RedirectToPage();
        }

        public string SaveChangedImage()
        {

            // Reassign the filename with the same name from the database
            string filename = null;

            if (UpdatedPhoto != null)
            {
                string UPLOAD_DIR = Path.Combine(_hostEnv.WebRootPath, "img");

                // Otherwise, generate a string of unique ID number
                filename = Guid.NewGuid().ToString() + "-" + UpdatedPhoto.FileName;

                // Destination path to the image file
                string DESTINATION_PATH = Path.Combine(UPLOAD_DIR, filename);

                // Create a physical image file and write it to a destination path
                using var fileStream = new FileStream(DESTINATION_PATH, FileMode.Create);
                UpdatedPhoto.CopyTo(fileStream);
            }

            return filename;
        }
    }
}
