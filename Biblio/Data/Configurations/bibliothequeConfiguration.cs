using Biblio.Bibliotheque;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Biblio.Data
{
    // cette classe est enregistrer dans la methode OnModelCreating dans le fichier bibliothequeContext.cs
    public class bibliothequeConfiguration : IEntityTypeConfiguration<Livre>
    {
        // la methode Configure appartient à l'interface IEntityTypeConfiguration<TEntity>
        public void Configure(EntityTypeBuilder<Livre> builder)
        {
            // map (met) la propriété Titre dans la colone qui convient 
            builder.Property(p => p.Titre).HasColumnName("Titre");
        }
    }
}