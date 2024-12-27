using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Biblio.Pages
{
    public class biblio1 : PageModel
    {
        private readonly ILogger<biblio1> _logger;

        public biblio1(ILogger<biblio1> logger)
        {
            _logger = logger;
        }

        public static void aff(){
            Console.WriteLine("azddzd");
        }

        public IActionResult OnPost(){
            return RedirectToPage();
        }

        public string TimeOfDay { get; set; }
        public void OnGet()
        {
            TimeOfDay = "evening";
            if(DateTime.Now.Hour < 18){
                TimeOfDay = "afternoon";
            }
            if(DateTime.Now.Hour < 12){
                TimeOfDay = "morning";
            }
        }
    
    }
}