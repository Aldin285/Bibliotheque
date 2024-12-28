using Biblio.Bibliotheque;
using Microsoft.EntityFrameworkCore;
namespace Biblio.Data;
public static class ModelBuilderExtensions
{
    // la methode Seed est une extension de ModelBuilder qui est utilisée dans le fichier Context
    // 
    public static ModelBuilder Seed(this ModelBuilder modelBuilder)
    {
        // HasData permet de gérer la base de données on ajoutants les nouveaux éléments dans la BDD SQLite
        // eviter l'ajout d'éléments existants, et modifier le contenu en cas de modification 
        modelBuilder.Entity<Livre>().HasData(
        new Livre
        {
            Id = 1,
            Titre = "Le Petit Prince",
            Auteur = "Antoine de Saint-Exupéry",
            Date_Publication = "1943"
        },
        new Livre
        {
            Id = 2,
            Titre = "SharkNado",
            Auteur = "Shark",
            Date_Publication = "unknown"
        });
        return modelBuilder;
    }
}