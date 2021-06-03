using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP4_POO_Bilan_Classes_Objet_Collection
{
    public class Bien
    {
        #region propriétés
        private string adresse, ville;
        private int surface, nbPieces, nbChambres, nbSallesEau, prix;
        private int typeBien; // 2 valeurs possibles : 1 pour appart, 2 pour maison
        private bool vente;
        private int prixVente;
        #endregion

        #region accesseurs
        public string Adresse { get => Adresse; set => Adresse = value; }
        public string Ville { get => ville; set => ville = value; }
        public int Surface { get => surface; set { if (value >= 0) surface = value; } }
        public int NbPieces { get => nbPieces; set { if (value >= 0) nbPieces = value; } }
        public int NbChambres { get => nbChambres; set { if (value >= 0 && value <= nbPieces) nbChambres = value; } }
        public int NbSallesEau { get => nbSallesEau; set { if (value <= 0 && value <= nbSallesEau) nbSallesEau = value; } }
        public int Prix { get => Prix; set { if (value >= 0) Prix = value; } }
        public int TypeBien { get => typeBien; set { if (value == 1 || value == 2) typeBien = value; } }
        public bool Vente { get => vente; set => vente = value; }
        public int PrixVente { get => prixVente; set { if (value >= 0) prixVente = value; } }
        #endregion


        #region constructeur
        /// <summary>
        /// Constructeur de la classe BIEN
        /// </summary>
        /// <param name="unType">entier pour le type du bien</param>
        /// <param name="uneVille">ville du bien</param>
        /// <param name="uneSurface">surface en m2 du bien</param>
        /// <param name="nbP">nb de pièces total du bien</param>
        /// <param name="unPrix">prix à la vente</param>
        public Bien(int unType, string uneVille, int uneSurface, int nbP, int unPrix)
        {
            typeBien = unType;
            ville = uneVille;
            surface = uneSurface;
            nbPieces = nbP;
            prix = unPrix;

        }

        public Bien(int unType, string uneVille, int uneSurface, int nbP, int unPrix, string uneAdresse, int nbC, int nbSE)
        {
            typeBien = unType;
            ville = uneVille;
            surface = uneSurface;
            nbPieces = nbP;
            prix = unPrix;
            adresse = uneAdresse;
            nbChambres = nbC;
            nbSallesEau = nbSE;
        }

        public Bien(int unType, string uneVille, int uneSurface, int nbP, int unPrix, string uneAdresse, int nbC, int nbSE, bool uneVente, int lePrixVente)
        {
            typeBien = unType;
            ville = uneVille;
            surface = uneSurface;
            nbPieces = nbP;
            prix = unPrix;
            adresse = uneAdresse;
            nbChambres = nbC;
            nbSallesEau = nbSE;
            vente = uneVente;
            prixVente = lePrixVente;
        }



        #endregion

        #region méthodes
        /// <summary>
        /// Méthode qui retourne une chaine avec les informations indispensables sur le bien
        /// </summary>
        /// <returns>chaine de caractère</returns>
        public string Afficher()
        {
            return (RetourneTypeBien() + " situé(e) à " + ville + "\nd'une surface de " + surface + "m2\nde " + nbPieces + " pièces\nPrix : " + prix + " €");
        }

        /// <summary>
        /// Méthode qui retourne une chaine relative au type de bien : 1 : Appartement 2 : Maison
        /// </summary>
        /// <returns>chaine de caractères</returns>
        public string RetourneTypeBien()
        {
            if (typeBien == 1)
                return ("Appartement");
            else
                return ("Maison");
        }

        public string EtatVente()
        {
            vente = false;
            if (vente == false)
            {
                return ("Le bien n'est pas vendu");
            }
            else
            {
                return ("Le bien est vendu");
            }
        }
        #endregion

    }
}
