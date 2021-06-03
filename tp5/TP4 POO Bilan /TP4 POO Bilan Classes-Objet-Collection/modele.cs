using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace TP4_POO_Bilan_Classes_Objet_Collection
{
    public class modele
    {
        #region propriété
        private MySqlConnection myConnection; // objet de connexion
        private bool connopen = false; // test si la connexion est faite
        private bool chargement = false; // test si le chargement d'une requête est fait
        private DataTable dT1 = new DataTable();
        #endregion

        #region accesseur
        public bool Connopen { get => connopen; set => connopen = value; }
        public bool Chargement { get => chargement; set => chargement = value; }
        public DataTable DT1 { get => dT1; set => dT1 = value; }
        #endregion

        #region constructeur
        public modele() { }
        #endregion

        #region methode
        public void seconnecter()
        {
            // paramètres de connexion à modifier selon sa BD et son serveur de BD
            string myConnectionString = "Database=BD_IMMOBILIER;Data Source=192.168.221.1;User Id = tliaigre; Password =maticelo12; ";
            myConnection = new MySqlConnection(myConnectionString);
            try // tentative
            {
                myConnection.Open();
                connopen = true;
            }
            catch (Exception err)// gestion des erreurs
            {
                MessageBox.Show("Erreur ouverture bdd : " + err, "PBS connection",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                connopen = false;
            }
        }

        public void sedeconnecter()
        {
            if (!connopen)
                return;
            try
            {
                myConnection.Close();
                myConnection.Dispose();
                connopen = false;
            }
            catch (Exception err)
            {
                MessageBox.Show("Erreur fermeture bdd : " + err, "PBS deconnection",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Controleur.Vmodele.sedeconnecter();
        }

        /// <summary>
        /// Méthode générique pour charger les données issues d'une requête dans un dataTable
        /// </summary>
        /// <param name="requete"></param>
        /// <param name="DT"></param>
        public void charger(string requete, DataTable DT)
        {
            MySqlCommand command = myConnection.CreateCommand();
            MySqlDataReader reader;
            try
            {
                command.CommandText = requete;
                reader = command.ExecuteReader();
                DT.Load(reader);
                chargement = true;
            }
            catch (Exception err)
            {
                MessageBox.Show("Erreur chargement dataTable: " + err, "PBS table",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                chargement = false;
            }
        }

        public void charger_donnees(string table)
        {
            if (table == "bien") charger("select * from BIEN;", dT1);
        }

        /// <summary>
        /// Méthode qui permet d'ajouter un bien avec l'ensemble de ses données
        /// </summary>
        /// <returns>bool</returns>
        public bool AjoutBIEN(string adr, string v, int surf, int nbP, int nbC, int nbSE, int prix, int typeB)
        {
            try
            {
                bool ok = false;
                // préparation de la requête avec des paramètres
                string requete = "insert into bien values (NULL, @adresse, @ville, @surface, @nbP, @nbC, @nbSE, @p, @tb)";
                MySqlCommand command = myConnection.CreateCommand();
                command.CommandText = requete;
                // mise à jour des paramètres de la requête préparée avec les infos passées en paramètre de la méthode
                command.Parameters.AddWithValue("adresse", adr);
                command.Parameters.AddWithValue("ville", v);
                command.Parameters.AddWithValue("surface", surf);
                command.Parameters.AddWithValue("nbP", nbP);
                command.Parameters.AddWithValue("nbC", nbC);
                command.Parameters.AddWithValue("nbSE", nbSE);
                command.Parameters.AddWithValue("p", prix);
                command.Parameters.AddWithValue("tb", typeB);
                // A COMPLETER avec les paramètres manquants
                // Exécution de la requête
                int i = command.ExecuteNonQuery();
                // i est positif si l'insertion a pu avoir lieu
                return (i > 0);
            }
            catch
            {
                return false;
            }
        }
        public bool ModifBIEN(string adr, string v, int surf, int nbP, int nbC, int nbSE, int prix, int typeB, int idBien)
        {
            try
            {
                bool ok = false;
                string requete = "update BIEN set adresseBien = @adr, villeBien = @ville, surface = @surface, nbPieces = @nbP, nbChambres = @nbC, nbSallesEau = @nbSE, prix = @p, typeBien = @tb, idBien = idBien where idBien = @idBien";
                MySqlCommand command = myConnection.CreateCommand();
                command.CommandText = requete;
                command.Parameters.AddWithValue("adr", adr);
                command.Parameters.AddWithValue("ville", v);
                command.Parameters.AddWithValue("surface", surf);
                command.Parameters.AddWithValue("nbP", nbP);
                command.Parameters.AddWithValue("nbC", nbC);
                command.Parameters.AddWithValue("nbSE", nbSE);
                command.Parameters.AddWithValue("p", prix);
                command.Parameters.AddWithValue("tb", typeB);
                command.Parameters.AddWithValue("idBien", idBien);
                int i = command.ExecuteNonQuery();
                return (i > 0);
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}