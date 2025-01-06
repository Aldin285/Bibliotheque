using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblio.Data;
using Biblio.Bibliotheque;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Biblio.Pages;

public class biblio1Model : PageModel
{
    private readonly bibliothequeContext context;

    public biblio1Model(bibliothequeContext context)=>
        this.context=context;
    
        
    public List<Livre> Livres { get; set; } = new ();

    // Crée une liste contenant tout les éléments de la base de données 
    public async Task OnGetAsync()=>
        Livres = await context.Livres.ToListAsync();

    // permet de lié livre_new au fichier cshtml
    [BindProperty]

    // variable pour ajouter un livre
    public Livre livre_new {get;set;}

    // variable pour afficher un message d'erreure
       public string Erreure { get; set; }

    // cette methode s'active quand la methode Post est activée ( bouton ajouter)
    public IActionResult OnPost(){
        context.Livres.Add(livre_new);
        if (livre_new.Auteur != null && livre_new.Date_Publication != null &&livre_new.Titre != null ){
            
            context.SaveChanges();
            TempData["Message"]=$"Le livre {Request.Form["Titre"]} est ajoutee avec succes";
        }else{
            TempData["Message"]=$"Veiller remplir toutes les cases";
        }
        
        return RedirectToPage();
    }
    
}