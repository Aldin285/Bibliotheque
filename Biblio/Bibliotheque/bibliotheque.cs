using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;

namespace Biblio.Bibliotheque{
    public class Livre{
        public int Id {get;set;}
        public string Titre {get;set;}

        public string Auteur{get;set;}

        public string Date_Publication{get;set;}

        public Livre(){}

        public Livre (int id,string nom,string auteur,string date_Publication){
            this.Id = id;
            this.Titre = nom;
            this.Auteur = auteur;
            this.Date_Publication = date_Publication;
        }
    }
}

