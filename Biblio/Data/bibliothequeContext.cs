using Biblio.Bibliotheque;
using Microsoft.EntityFrameworkCore;
namespace Biblio.Data;

// DbContext ajoute la IDisposable qui permet de retirer 
// du contenu quand on finit de l'utiliser 
public class bibliothequeContext : DbContext
{
    // DbSet crée un lien avec la class Livre du fichier
    // Bibliotheque\bibliotheque.cs qui est une table dans la base de données
    // le nom des colones sera le nom des parametres de la class Livre (ex:ID)
    public DbSet<Livre> Livres { get; set; }
    
    // OnConfiguring permet de configurer le contenu
    // dans ce cas, on utilise SQLite
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"Data source=bibliotheque.db");
    }

    // puiseque la methode Seed() renvoie une instance (objet, variable) de type ModeBuilder,
    // on peut la relié à la methode OnModelCreating qui utilise le meme type d'instance
     protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new bibliothequeConfiguration()).Seed();
    }
    
}