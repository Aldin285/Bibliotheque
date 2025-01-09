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


    public class Bibliotheque{

        public static void Main(){
            try {
                Livre livre = new Livre(1,"Le Petit Prince","Antoine de Saint-Exupéry","1943");
                XmlSerializer xs = new XmlSerializer (typeof(Livre));
                StringWriter sw = new StringWriter();
                
            //   ajout_livre();
            //   get_livres();
                // get_livre();
                // suppression_livre(1);
                Intro();
                
            }catch (Exception e){
                Console.WriteLine($"ERREUR: {e.Message}");
            } 
        }

        // emplacement du fichier xml
        const string xmlfile = "bibliotheque.xml";

        // fonction pour ajouter un livre au fichier xml
        public static void Intro(){
         
            int etat = 0;
            
            while(etat!=1){
                Console.WriteLine($"----------------");
                Console.WriteLine("Bonjour, Que voulez-vous faire? (veiller selectionner un numéro)");
                Console.WriteLine($"----------------");
                Console.WriteLine($"1-Ajouter un livre");
                Console.WriteLine($"2-Afficher tout les livres");
                Console.WriteLine($"3-Rechercher un livre");
                Console.WriteLine($"4-Supprimer un livre");
                Console.WriteLine($"5-Quitter");
                int choix = Convert.ToInt32(Console.ReadLine());

                if(choix==1){
                    ajout_livre();
                }else if(choix==2){
                    get_livres();
                }else if(choix==3){
                    get_livre();
                }else if(choix==4){
                    suppression_livre();
                }else if(choix==5){
                    etat=1;
                }
            }
        }
        public static void ajout_livre( ){
            Console.WriteLine($"ID:");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Titre:");
            string titre = Console.ReadLine();
            Console.WriteLine($"Auteur:");
            string Auteur = Console.ReadLine();
            Console.WriteLine($"Date de pubication:");
            string date = Console.ReadLine();

            Livre book = new Livre(id,titre,Auteur,date);
            

            var xdoc =XDocument.Load(xmlfile);
            var xelement = new XElement("Livre",new XAttribute("ID",book.Id),
                                        new XElement("Titre",book.Titre),
                                        new XElement("Auteur",book.Auteur),
                                        new XElement("Date_de_Publication",book.Date_Publication)
                                        );

             var livres= xdoc.Root.Descendants("Livre")
                .Select(x=> new Livre(int.Parse(x.Attribute("ID").Value),x.Element("Titre").Value,x.Element("Auteur").Value,x.Element("Date_de_Publication").Value)) ;

            // pour éviter qu'un livre ajouté aie le meme id d'un livre existant 
            
            foreach ( var L in livres){
                if (L.Id==book.Id){
                    throw new ArgumentException("Cet identifiant est déja utilisé");
                }
            }
            xdoc.Root.Add(xelement);
            xdoc.Save(xmlfile);
            Console.WriteLine("Nouveau Element Ajouté");
        } 

// fonction pour supprimer un livre au fichier xml par rappot à son ID 
        public static void suppression_livre(){
            Console.WriteLine($"Veiller saisir l'id du livre à supprimer:");
            int id = Convert.ToInt32(Console.ReadLine());
            var xdoc =XDocument.Load(xmlfile);
            // la partie FirstOrDefault cherche le premier element qui possede le meme id saisie (renvoie null si l'id saisie est introuvable)
            //x represente chaque élément <Livre> du fichier xml
            //? evite qu'une exception soie renvoyée si l'ID est introuvable et met la valeur null à la place 

            
            var livre_a_supprimer= xdoc.Root.Descendants("Livre")
                .FirstOrDefault(x=> x.Attribute("ID")?.Value== id.ToString()) ;
            // ducoup si id=1  livre_a_supprimer renvoie:
            
            // <Livre ID="1">
            // <Titre>oz</Titre>
            // <Auteur>oz</Auteur>
            // <Date_de_Publication>11/10/2001</Date_de_Publication>
            // </Livre>
            
                if (livre_a_supprimer != null)
                {
                    livre_a_supprimer.Remove(); 
                    xdoc.Save(xmlfile);
                    Console.WriteLine($"----------------");
                    Console.WriteLine($"le livre avec l'ID {id} a été suprimé");
                    Console.WriteLine($"----------------");
                }
                else
                {
                    throw new ArgumentException("Livre introuvable");
                }

            }
        

        //Afficher les livres dans le fichier XML
        public static void get_livres(){
            var xdoc =XDocument.Load(xmlfile);
            //cette partie du code extrait tout les éléments faisant partie de l'attribut Livre et crée des instances Livre
            //ducoup dans chaque instence on fait en sorte que les éléments soient dans le bon ordre par rapport à la class Livre
            //alors la variable livres est une sorte de liste contenant des instance Livre avec un nombre d'instance égale au nombre de livres dans le fichier XML
            
            //int.Parse lit la valeur de l'attribut ID, la converti en int et la met dans le parametre id de l'instance livres 
            var livres= xdoc.Root.Descendants("Livre")
                .Select(x=> new Livre(int.Parse(x.Attribute("ID").Value),x.Element("Titre").Value,x.Element("Auteur").Value,x.Element("Date_de_Publication").Value)) ;

            
            foreach ( var L in livres){
                Console.WriteLine($" ID:{L.Id} - Titre: {L.Titre} - Auteur: {L.Auteur} - Date de Publication: {L.Date_Publication}");
            }
        }


        // Aficher un livre par élément précis
        public static void get_livre(){
            var xdoc =XDocument.Load(xmlfile);
            var livres= xdoc.Root.Descendants("Livre")
                .Select(x=> new Livre(int.Parse(x.Attribute("ID").Value),x.Element("Titre").Value,
                x.Element("Auteur").Value,x.Element("Date_de_Publication").Value)) ;

            Console.WriteLine($"----------------");
            Console.WriteLine("Par rapport à quoi voulez-vous chercher votre livre (séléctioner le nombre):");
            Console.WriteLine("1-ID");
            Console.WriteLine("2-Titre");
            Console.WriteLine("3-Auteur");
            Console.WriteLine("4-Date de Publication");
            Console.WriteLine($"----------------");
            int a = Convert.ToInt32(Console.ReadLine());
            
            int nb_recherches=0;

            switch(a){
                case 1:
                   Console.WriteLine("Quel est l'ID du film:");
                   int id= Convert.ToInt32(Console.ReadLine());

                   foreach ( var L in livres){
                    // je garde le break dans le premier if car chaque livre doit avoir un ID unique
                    if (L.Id==id){
                        Console.WriteLine($"----------------");
                        Console.WriteLine($"ID: {L.Id}");
                        Console.WriteLine($"Titre: {L.Titre}");
                        Console.WriteLine($"Auteur: {L.Auteur}");
                        Console.WriteLine($"Date de Publication: {L.Date_Publication}");
                        Console.WriteLine($"----------------");
                        break;
                    } else{
                        nb_recherches++;
                    }
                    if (nb_recherches==livres.Count()){
                        Console.WriteLine("Livre introuvable");
                        break;
                    }
                   }

                   break;
                case 2:
                   Console.WriteLine("Quel est le titre du film:");
                   string titre= Console.ReadLine();

                   foreach ( var L in livres){
                    // j'ai enlevé le break dans le premier if en cas où il y a des livres avec le meme titre
                    if (L.Titre.Contains(titre,StringComparison.CurrentCultureIgnoreCase)){
                        Console.WriteLine($"----------------");
                        Console.WriteLine($"ID: {L.Id}");
                        Console.WriteLine($"Titre: {L.Titre}");
                        Console.WriteLine($"Auteur: {L.Auteur}");
                        Console.WriteLine($"Date de Publication: {L.Date_Publication}");
                        Console.WriteLine($"----------------");
                    } else{
                        nb_recherches++;
                    }
                    if (nb_recherches==livres.Count()){
                        Console.WriteLine("Livre introuvable");
                        break;
                    }
                   }

                   break;
                case 3:
                   Console.WriteLine("Quel est l'auteur du film:");
                   string auteur= Console.ReadLine();
                   foreach ( var L in livres){
                    // j'ai enlevé le break dans le premier if en cas où il y a des livres avec le meme nom d'auteur
                    if (L.Auteur.Contains(auteur,StringComparison.CurrentCultureIgnoreCase)){
                        Console.WriteLine($"----------------");
                        Console.WriteLine($"ID: {L.Id}");
                        Console.WriteLine($"Titre: {L.Titre}");
                        Console.WriteLine($"Auteur: {L.Auteur}");
                        Console.WriteLine($"Date de Publication: {L.Date_Publication}");
                        Console.WriteLine($"----------------");
                    } else{
                        nb_recherches++;
                    }
                    if (nb_recherches==livres.Count()){
                        Console.WriteLine("Livre introuvable");
                        break;
                    }
                   }

                   break;
                case 4:
                   Console.WriteLine("Quel est la Date de Publication du film:");
                   string date_publication= Console.ReadLine();
                   foreach ( var L in livres){
                    // j'ai enlevé le break dans le premier if en cas où il y a des livres avec le meme titre
                    // meme si le user ne connait pas la date exacte, il suffit juste qu'il choisisse une année (200==> entre 2000 et 2009,199==>1990-1999)
                    if (L.Date_Publication.Contains(date_publication,StringComparison.CurrentCultureIgnoreCase)){
                        Console.WriteLine($"----------------");
                        Console.WriteLine($"ID: {L.Id}");
                        Console.WriteLine($"Titre: {L.Titre}");
                        Console.WriteLine($"Auteur: {L.Auteur}");
                        Console.WriteLine($"Date de Publication: {L.Date_Publication}");
                        Console.WriteLine($"----------------");
                    } else{
                        nb_recherches++;
                    }
                    if (nb_recherches==livres.Count()){
                        Console.WriteLine("Livre introuvable");
                        break;
                    }
                   }
                   break;
                default:
                    Console.WriteLine("Veiller choisir parmis les chifres proposés");
                    break;
            
            }

         }
    }
}