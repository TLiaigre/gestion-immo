using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP4_POO_Bilan_Classes_Objet_Collection
{
    public partial class FormBiensImmobiliers : Form
    {
        private Bien unBien;
        List<Bien> lesBiens = new List<Bien>();
        public FormBiensImmobiliers()
        {
            InitializeComponent();
        }


        private void TbSurface_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != Convert.ToChar(Keys.Back))
            {
                MessageBox.Show("Erreur, caractères autorisés : chiffre seulement", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Handled = true; // efface le dernier caractère saisi
            }
        }

        private void TbPrix_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != Convert.ToChar(Keys.Back))
            {
                MessageBox.Show("Erreur, caractères autorisés : chiffre seulement", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Handled = true; // efface le dernier caractère saisi
            }
        }

        private void BtnAnnuler_Click(object sender, EventArgs e)
        {
            rbAppart.Checked = false;
            rbMaison.Checked = false;
            tbSurface.Clear();
            tbAdresse.Clear();
            tbVille.Clear();
            numNbChambres.Value = 0;
            numNbPieces.Value = 0;
            numNbSalleEau.Value = 0;
            tbPrix.Clear();
        }

        private void BtnAjouter_Click(object sender, EventArgs e)
        {
            int typeB=2; // maison par défaut
            // saisies  de type de bien, ville, surface, nbPieces et prix obligatoires
            if ((rbAppart.Checked || rbMaison.Checked) && tbVille.Text != "" && tbSurface.Text != "" && numNbPieces.Value !=0 && tbPrix.Text !="")
            {
                if (rbAppart.Checked) typeB = 1;

                // A COMPLETER : instancier l'objet unBien avec les informations saisies (par le constructeur et les accesseurs si besoin)
                unBien = new Bien(typeB, tbVille.Text, Convert.ToInt32(tbSurface.Text), Convert.ToInt32(numNbPieces.Value), Convert.ToInt32(tbPrix.Text), tbAdresse.Text, Convert.ToInt32(numNbChambres.Value), Convert.ToInt32(numNbSalleEau));

                // A COMPLETER : Afficher dans un MessageBox toutes les informations du bien (penser à utiliser la méthode Afficher() de la classe BIEN)
                MessageBox.Show(unBien.Afficher());

                // A COMPLETER : Ajouter ce bien à la collection lesBiens
                lesBiens.Add(unBien);


        BtnAnnuler_Click(sender, e); // Annulation pour vider les zones de saisies
                FormBiensImmobiliers_Load(sender, e); // pour mettre à jour la comboBox
            }
            else
            {
                MessageBox.Show("Erreur, il faut saisir au moins le type de bien, la ville, la surface, le nb de pièces et le prix pour valider", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnQuitter_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormBiensImmobiliers_Load(object sender, EventArgs e)
        {
            // A comprendre : que fait cette méthode ? quand est-elle déclenchée ?
            cbListeBiens.Items.Clear();
            Controleur.init();
            Controleur.Vmodele.seconnecter();
            if (Controleur.Vmodele.Connopen)
            {
                MessageBox.Show("connection ok");
            }
            else
            {
                MessageBox.Show("erreur de connection");
            }
            foreach (Bien B in lesBiens)
            {
                cbListeBiens.Items.Add(B.RetourneTypeBien()+" - " + B.Ville + " " + B.NbPieces + " pièces - " + B.Prix+ "€");
            }
        }

        private void CbListeBiens_SelectedIndexChanged(object sender, EventArgs e)
        {
            // A comprendre
            int index = cbListeBiens.SelectedIndex;
            if (index !=-1)
            {
                Bien B = lesBiens[index];
                rbAppart.Checked = (B.TypeBien == 1);
                rbMaison.Checked = (B.TypeBien == 2);
                tbSurface.Text = B.Surface.ToString();
                tbAdresse.Text = B.Adresse;
                tbVille.Text = B.Ville;
                numNbPieces.Value = B.NbPieces;
                numNbChambres.Value = B.NbChambres;
                numNbSalleEau.Value = B.NbSallesEau;
                tbPrix.Text = B.Prix.ToString();
            }
        }

        private void BtnSupprimer_Click(object sender, EventArgs e)
        {
            int index = cbListeBiens.SelectedIndex;
            if (index != -1)
            {
                DialogResult c = MessageBox.Show("Etes-vous sûr de vouloir supprimer ce bien ?", "CONFIRMATION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (c == DialogResult.Yes)
                {
                    // A COMPLETER : supprimer le bien sélectionné de la collection lesBiens
                    lesBiens.RemoveAt(cbListeBiens.SelectedIndex);

                    BtnAnnuler_Click(sender, e); // Annulation pour vider les zones de saisies
                    FormBiensImmobiliers_Load(sender, e); // pour mettre à jour la comboBox
                }
            }
            
        }

        private void BtnModifier_Click(object sender, EventArgs e)
        {
            int index = cbListeBiens.SelectedIndex;
            if (index != -1)
            {
                int typeB = 2; // maison par défaut
                               // saisies  de type de bien, ville, surface, nbPieces et prix obligatoires
                if ((rbAppart.Checked || rbMaison.Checked) && tbVille.Text != "" && tbSurface.Text != "" && numNbPieces.Value != 0 && tbPrix.Text != "")
                {
                    if (rbAppart.Checked) typeB = 1;
                    lesBiens[index].TypeBien = typeB;

                    // A COMPLETER : mettre à jour toutes les autres propriétés du bien sélectionné à partir des informations saisies sur l'interface
                    lesBiens[cbListeBiens.SelectedIndex] = new Bien(typeB, tbVille.Text, Convert.ToInt32(tbSurface.Text), Convert.ToInt32(numNbPieces.Value), Convert.ToInt32(tbPrix.Text), tbAdresse.Text, Convert.ToInt32(numNbChambres.Value), Convert.ToInt32(numNbSalleEau));

                    MessageBox.Show("Bien modifié");

                    BtnAnnuler_Click(sender, e); // Annulation pour vider les zones de saisies
                    FormBiensImmobiliers_Load(sender, e); // pour mettre à jour la comboBox
                }
            }
        }

        private void BtVente_Click(object sender, EventArgs e)
        {

        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Controleur.Vmodele.sedeconnecter();
        }
    }
}
