using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblio.Data;
using Biblio.Bibliotheque;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Biblio.Pages;

public class updateModel : PageModel
{
    private readonly bibliothequeContext context;

    public updateModel(bibliothequeContext context)=>
        this.context=context;
    
    
    // permet de lié l'id à la page 
    // genéralement, sauf la methode Post peut renvoyer une valeur
    // c'est grace à (SupportsGet =true) qu'on peut lié l'id à la page avec la méthode get
    [BindProperty (SupportsGet =true)]
    public int Id{get; set;}
    public Livre Livre {get;set;}

    // la methode FindAsync stock l'id du livre séléctioné et le renvoie à la page 
    public async Task OnGetAsync(){
        Livre = await context.Livres.FindAsync(Id);
   
    }

    // pour mettre à jour les données et les afficher
    public Livre Un_Livre {get;set ;}

    public IActionResult OnPost(Livre livre){
        Un_Livre = Update(livre);
        
        return RedirectToPage();
    }

// variable pour afficher un message de succès ou d'erreure
 public string Message { get; set; }
 

// methode pour mettre à jour les infos d'un livre
// NOTE A SOI-MEME: n'oublie pas de sauvgarder dans la meme methode les modifications après les avoir modifié
// NOTE : il fallait utiliser TempData["Message"] aulieu de ViwaData["Message"] pour afficher le message après avoir modifier les infos
//        la valeur de Message ne change pas meme si la condition est réalisée ()

// la différence entre ViewData et TempData est que V reste que pour la requete actuelle et se réintialise après un Redirect
// tandis que T est s'applique après avoir fait un redirect et apès avoir été lu une fois 
     public List<Livre> Livres { get; set; } = new ();
    public Livre Update(Livre livre_updated){
        Livre livre= context.Livres.FirstOrDefault(l => l.Id == livre_updated.Id);
        if (livre != null){
            livre.Titre=livre_updated.Titre;
            livre.Auteur=livre_updated.Auteur;
            livre.Date_Publication=livre_updated.Date_Publication;
            TempData["Message"]="Informations mis à jour";
            // TempData.Keep("Message");
            Message="INFOS mis à jour";
            context.SaveChanges();
            
        }else{
             TempData["Message"]="Livre intouvable";
             Message="Livre intouvable";
        }
        context.SaveChanges();
        return livre;
    }
    
}
