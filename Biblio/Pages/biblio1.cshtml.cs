using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblio.Data;
using Biblio.Bibliotheque;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Biblio.Pages;

public class biblio1Model : PageModel
{
    private readonly bibliothequeContext context;

    public biblio1Model(bibliothequeContext context)=>
        this.context=context;
    
        
    public List<Livre> Livres { get; set; } = new ();

    // Permet d'afficher le contenu dans la page
    public async Task OnGetAsync()=>
        Livres = await context.Livres.ToListAsync();

    // permet de lié livre_new au fichier cshtml
    [BindProperty]
    public Livre livre_new {get;set;}

    // cette methode s'active quand la methode Post est activée ( bouton ajouter)
    public IActionResult OnPost(){
        context.Livres.Add(livre_new);
        
        context.SaveChanges();
        return RedirectToPage();
    }
    
}


//    public string TimeOfDay { get; set; }
// public void OnGet()
        // {
        //     TimeOfDay = "evening";
        //     if(DateTime.Now.Hour < 18){
        //         TimeOfDay = "afternoon";
        //     }
        //     if(DateTime.Now.Hour < 12){
        //         TimeOfDay = "morning";
        //     }
        // }