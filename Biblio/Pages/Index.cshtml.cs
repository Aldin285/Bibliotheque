using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblio.Data;
using Biblio.Bibliotheque;
using Microsoft.EntityFrameworkCore;

namespace Biblio.Pages;

public class IndexModel : PageModel
{
    private readonly bibliothequeContext context;
    public IndexModel(bibliothequeContext context)=>
        this.context=context;
    
        
    public List<Livre> Livres { get; set; } = new ();
    public async Task OnGetAsync()=>
        Livres = await context.Livres.ToListAsync();
    
}
