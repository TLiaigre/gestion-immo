using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP4_POO_Bilan_Classes_Objet_Collection
{
    public static class Controleur
    {
        #region propriété
        private static modele vmodele;
        #endregion

        #region accesseur
        public static modele Vmodele { get => vmodele; set => vmodele = value; }
        #endregion

        #region constructeur
        #endregion

        #region methode
        public static void init() {
            Vmodele = new modele();
        }
        #endregion
    }
}
