using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace label
{
    public partial class Form1 : Form
    {
        Label[,] lbl = new Label[4, 4]; //tableau de Labels 4 lignes et 4 colonnes
        int[,] grille = new int[4, 4];  //tableau du jeu
        Color[] color = { Color.Snow, Color.Beige, Color.MistyRose, Color.SandyBrown, Color.Orange, Color.Tomato, };

        public Form1()
        {
            InitializeComponent();
            InitializeJeu();
        }

        private void InitializeJeu()
        {
            // Création des labels
            for (int ligne = 0; ligne < 4; ligne++)
            {
                for (int colonne = 0; colonne < 4; colonne++)
                {
                    lbl[ligne, colonne] = new Label(); //Création du label

                    // le 20 + 100 * colonne détermine où placer le premier label dans le form en X
                    // le 20 + 100 * ligne détermine où placer le label en Y
                    // le 90, 90 est la taille du label
                    lbl[ligne, colonne].Bounds = new Rectangle(20 + 100 * colonne, 20 + 100 * ligne, 90, 90);

                    // met le texte au milieu du label
                    lbl[ligne, colonne].TextAlign = ContentAlignment.MiddleCenter;
                    lbl[ligne, colonne].Font = new Font("Arial", 20);

                    Controls.Add(lbl[ligne, colonne]); //Ajout visible à la page
                }
            }

            // génération des 2 nb aléatoires sans vérification de fin de jeu
            for (int i = 0; i < 2; i++)
                newVide();
            affiche(); // s'occupe des textes et couleurs
        }

        private void affiche()
        {
            //réaffiche tout le tableau avec les bonnes couleurs et les bons textes, conformément au tableau jeu
            for (int ligne = 0; ligne < 4; ligne++)
            {
                for (int colonne = 0; colonne < 4; colonne++)
                {
                    if (grille[ligne, colonne] > 0) // plot avec puissance de 2, pour faire 2 puissance n on fait 1<<n
                        lbl[ligne, colonne].Text = (1 << grille[ligne, colonne]).ToString();
                    else
                        lbl[ligne, colonne].Text = "";
                    lbl[ligne, colonne].BackColor = color[grille[ligne, colonne]];
                }
            }
        }

        // Faire apparaître un plot aléatoirement, on vérifiera si c'est la fin du jeu ou non
        private void newVide()
        {
            //génération d'un tableau des cases vides (en numérotant de 0 à 16)
            List<int> aVide = new List<int>();
            for (int ligne = 0; ligne < 4; ligne++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (grille[ligne, col] == 0)
                    {
                        aVide.Add(4 * ligne + col); //stocker le num de case (4*ligne+col)
                    }
                }
            }

            //prendre une de ces cases aléatoirement
            if (aVide.Count > 0) //cas où qq chose bouge encore, et qu'il y a de la place
            {
                Random aleatoire = new Random();
                int nalea = aleatoire.Next(aVide.Count); //Génère un entier aléatoire
                grille[aVide[nalea] / 4, aVide[nalea] % 4] = 1; //met un 2 (2 puissance 1) dans la case choisie
                affiche();
            }
        }
    }
}
