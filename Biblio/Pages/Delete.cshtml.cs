using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblio.Data;
using Biblio.Bibliotheque;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Biblio.Pages;

public class DeleteModel : PageModel
{
    private readonly bibliothequeContext context;

    public DeleteModel(bibliothequeContext context)=>
        this.context=context;
    
    
    [BindProperty (SupportsGet =true)]
    public int Id{get; set;}
    public Livre Livre {get;set;}

    public async Task OnGetAsync(){
        Livre = await context.Livres.FindAsync(Id);
   
    }

    public Livre Livre_delete {get;set ;}

    public IActionResult OnPost(Livre livre){
        Livre_delete = Delete(livre);
        
        return RedirectToPage("/Index");
    }

  public List<Livre> Livres { get; set; } = new ();
    public Livre Delete(Livre livre_updated){
        Livre livre= context.Livres.FirstOrDefault(l => l.Id == livre_updated.Id);
        if (livre != null){
            context.Livres.Remove(livre);
            TempData["Message"]=$"Le livre {Request.Form["Titre"]} a ete supprimer avec succes";
            context.SaveChanges();
            
        }else{
             TempData["Message"]="Livre intouvable";
        }
        context.SaveChanges();
        return livre;
    }
    
}
