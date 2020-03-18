using ProjetLinq.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2
{
    class Program
    {
        static void Main(string[] args)
        {
            InitialiserDatas();

            Console.WriteLine("Afficher la liste des prénoms des auteurs dont le nom commence par 'G'");
            var listeAuteursG = ListeAuteurs.Where(a => a.Nom.Substring(0, 1).ToLower().Equals("g"));
            foreach (var auteur in listeAuteursG)
            {
                Console.WriteLine($"{auteur.Prenom}");
            }


            Console.WriteLine("\nAfficher l'auteur ayant écrit le plus de livres");
            var auteurPlusPresent = ListeAuteurs.OrderBy(a => ListeLivres.OrderBy(l => l.Auteur == a).Count()).First();
            Console.WriteLine($"{auteurPlusPresent.Nom} {auteurPlusPresent.Prenom}");


            Console.WriteLine("\nAfficher le nombre moyen de pages par livre par auteur");
            var listeAuteurs = ListeAuteurs.OrderBy(a => a.Nom);
            foreach (var auteur in listeAuteurs)
            {
                var livresParAuteur = ListeLivres.Where(l => l.Auteur.Equals(auteur));
                if (livresParAuteur.Count() != 0)
                {
                    double moyennePages = livresParAuteur.Average(l => l.NbPages);
                    Console.WriteLine($"{auteur.Nom} {auteur.Prenom} {moyennePages}");
                }
            }


            Console.WriteLine("\nAfficher le titre du livre avec le plus de pages");
            var livrePlusLong = ListeLivres.OrderByDescending(l => l.NbPages).First();
            Console.WriteLine($"{livrePlusLong.Titre} {livrePlusLong.Synopsis}");


            Console.WriteLine("\nAfficher combien ont gagné les auteurs en moyenne (moyenne des factures)");
            var listedesFactures = ListeAuteurs.Average(a => a.Factures.Sum(f => f.Montant));
            Console.WriteLine(listedesFactures.ToString());


            Console.WriteLine("\nAfficher les auteurs et la liste de leurs livres");
            foreach (var auteur in listeAuteurs)
            {
                Console.WriteLine($"{auteur.Nom} {auteur.Prenom} : ");

                var listeLivres = ListeLivres.Where(l => l.Auteur.Nom == auteur.Nom);
                foreach (var livre in listeLivres)
                {
                    Console.WriteLine($" - {livre.Titre} {livre.Synopsis}");
                }
            }


            Console.WriteLine("\nAfficher les titres de tous les livres triés par ordre alphabétique");
            var listeLivresAlpha = ListeLivres.OrderBy(l => l.Titre);
            foreach (var livre in listeLivresAlpha)
            {
                Console.WriteLine($"{livre.Titre} {livre.Synopsis}");
            }


            Console.WriteLine("\nAfficher la liste des livres dont le nombre de pages est supérieur à la moyenne");
            var tailleMoyennedesLivres = ListeLivres.Average(l => l.NbPages);
            foreach (var auteur in listeAuteurs)
            {
                var moyennePage = ListeLivres.Where(l => l.Auteur.Nom == auteur.Nom);
                foreach (var livre in moyennePage)
                {
                    if (livre.NbPages > tailleMoyennedesLivres)
                    {
                        Console.WriteLine($"{livre.Titre} {livre.Synopsis}");
                    }
                }
            }


            Console.WriteLine("\nAfficher l'auteur ayant écrit le moins de livres");
            var auteurMoinsPresent = ListeAuteurs.OrderBy(a => ListeLivres.OrderBy(l => l.Auteur == a).Count()).Last();
            Console.WriteLine($"{ auteurMoinsPresent.Nom } {auteurMoinsPresent.Prenom}");


            Console.ReadKey();
        }

        private static List<Auteur> ListeAuteurs = new List<Auteur>();
        private static List<Livre> ListeLivres = new List<Livre>();

        private static void InitialiserDatas()
        {
            ListeAuteurs.Add(new Auteur("GROUSSARD", "Thierry"));
            ListeAuteurs.Add(new Auteur("GABILLAUD", "Jérôme"));
            ListeAuteurs.Add(new Auteur("HUGON", "Jérôme"));
            ListeAuteurs.Add(new Auteur("ALESSANDRI", "Olivier"));
            ListeAuteurs.Add(new Auteur("de QUAJOUX", "Benoit"));
            ListeLivres.Add(new Livre(1, "C# 4", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 533));
            ListeLivres.Add(new Livre(2, "VB.NET", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 539));
            ListeLivres.Add(new Livre(3, "SQL Server 2008", "SQL, Transact SQL", ListeAuteurs.ElementAt(1), 311));
            ListeLivres.Add(new Livre(4, "ASP.NET 4.0 et C#", "Sous visual studio 2010", ListeAuteurs.ElementAt(3), 544));
            ListeLivres.Add(new Livre(5, "C# 4", "Développez des applications windows avec visual studio 2010", ListeAuteurs.ElementAt(2), 452));
            ListeLivres.Add(new Livre(6, "Java 7", "les fondamentaux du langage", ListeAuteurs.ElementAt(0), 416));
            ListeLivres.Add(new Livre(7, "SQL et Algèbre relationnelle", "Notions de base", ListeAuteurs.ElementAt(1), 216));
            ListeAuteurs.ElementAt(0).addFacture(new Facture(3500, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(0).addFacture(new Facture(3200, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(1).addFacture(new Facture(4000, ListeAuteurs.ElementAt(1)));
            ListeAuteurs.ElementAt(2).addFacture(new Facture(4200, ListeAuteurs.ElementAt(2)));
            ListeAuteurs.ElementAt(3).addFacture(new Facture(3700, ListeAuteurs.ElementAt(3)));
        }
    }
}
