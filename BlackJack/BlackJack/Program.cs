using System;

namespace BlackJack
{
    class Program
    {
        static Player[] players = new Player[5];
        static int pointer = 0;

        class JouerCarte 
        {
            public string Suit;
            public int Valeur;
            public int Points;

            // Constructeur alternatif avec 2 paramètres - Int pour Suit, Int pour Valeur
            // utiliser dans la fonction genererCartes()
            public JouerCarte(int s, int v)
            {
                Valeur = v; // Définit la valeur de la carte à la valeur de v (le deuxième argument)
                switch (s) // Instruction de cas basée sur la valeur de s
                {
                    case 1: // Si s == 1, alors régle le Suit sur Trèfles
                        Suit = "Trèfles";
                        break; // sert a quitter condition
                    case 2: // Si s == 2, alors régle la Suit sur Carreau
                        Suit = "Carreau";
                        break;
                    case 3: // Si s == 3, alors régle le Suit sur Coeur
                        Suit = "Coeur";
                        break;
                    case 4: // Si s == 4, alors régle le Suit sur Pique
                        Suit = "Pique";
                        break;
                }

                if (Valeur > 10)
                {
                    Points = 10;
                }
                else if (Valeur == 1) // Si c'est un as
                {
                    Points = 11; // Fixe les points à 11
                }
                else
                {
                    Points = Valeur;
                }
            }
        }

        class Player
        {
            public JouerCarte[] main;
            public int CarteEnMain;
            public int points;
            public string nom;

            public Player()
            {
                main = new JouerCarte[5];
                CarteEnMain = 0;
                points = 0;
            }

        }

        static void Main(string[] args)
        {
            UnJoueur();

            Console.ReadLine();

        }


        // Génère le jeu de 52 cartes
        static JouerCarte[] genererCartes()
        {
            JouerCarte[] deck = new JouerCarte[52]; // Déclare un tableau de JouerCarte d'une taille de 52
            int compteur = 0; // nous dit où enregistrer la valeur suivante dans le tableau

            // Boucle for imbriquée pour générer les 52 cartes - 4 combinaisons possibles avec 13 valeurs possibles chacune
            for (int suit = 1; suit < 5; suit++) // Parcoure les 4 combinaisons possibles
            {
                for (int Valeur = 1; Valeur < 14; Valeur++) // Boucle sur les 13 valeurs possibles
                {
                    deck[compteur] = new JouerCarte(suit, Valeur); // Génére une nouvelle carte et la stocker dans le deck
                    compteur++; // Incrémentation du compteur
                }
            }

            return deck; // Renvoie le deck terminé
        }

        // Procédure pour mélanger le jeu de cartes
        static void mélangerJeux(ref JouerCarte[] deck)
        {
            Random rnd = new Random(); // Crée un nouvel objet Random
            JouerCarte temp; // Crée une variable pour stocker temporairement une Carte à Jouer
            int num; // Crée une variable entière pour stocker les nombres générés aléatoirement

            for (int i = 0; i < deck.Length; i++)  // Boucle sur chaque index du tableau
            {
                num = rnd.Next(0, deck.Length); // Génère un nombre aléatoire 


                // Échange le contenu de deck[i] et deck[num]
                temp = deck[i];
                deck[i] = deck[num];
                deck[num] = temp;
            }

            // Les changements automatiquement appliqués au deck dans le programme principal
        }

        static void CarteSortie(JouerCarte card)
        {
            switch (card.Valeur) // Instruction de cas basée sur la valeur de la carte
            {

                // pour 1 - "L'As de ..."
                case 1:
                    Console.WriteLine("L'As de {0}", card.Suit);
                    break; // sert a quitter condition

                //  pour 11 - "Le valet de ..."
                case 11:
                    Console.WriteLine("Le valet de {0}", card.Suit);
                    break;


                //  pour 12 - "La Reine de..."
                case 12:
                    Console.WriteLine("La Reine de {0}", card.Suit);
                    break;

                // pour 13 - "Le Roi de..."
                case 13:
                    Console.WriteLine("Le Roi de {0}", card.Suit);
                    break;

                // Case pour tout le reste
                default:
                    Console.WriteLine("Le {0} de {1}", card.Valeur, card.Suit);
                    break;
            }
        }

        // Affiche les détails d'une carte à l'aide de symboles - par exemple/ As
        static void SymboleCarteSortie(JouerCarte card)
        {
            switch (card.Valeur)  // Instruction de cas basée sur la valeur de la carte
            {
                //  pour 1 - "L'As de ..."
                case 1:
                    Console.WriteLine("As{0} ", card.Suit);
                    break;

                // pour 11 - "Le valet de ..."
                case 11:
                    Console.WriteLine("Valet{0} ", card.Suit);
                    break;

                //  pour 12 - "La Reine de..."
                case 12:
                    Console.WriteLine("Reine{0} ", card.Suit);
                    break;

                //  pour 13 - "Le Roi de..."
                case 13:
                    Console.WriteLine("Roi{0} ", card.Suit);
                    break;
                //  pour tout le reste 
                default:
                    Console.WriteLine("{0}{1} ", card.Valeur, card.Suit);
                    break;
            }
        }

        // Affiche toutes les cartes dans la main d'un joueur avec leur total de points
        static void sortieMain(Player player)
        {
            // Affiche "Main actuelle : "
            Console.WriteLine("Main actuelle : ");
            // Boucle sur toutes les cartes en main
            for (int i = 0; i < player.CarteEnMain; i++)
            {
                SymboleCarteSortie(player.main[i]);
            }
            Console.WriteLine("Points actuels : {0}.", player.points);
        }

        static void dessinerCarte(JouerCarte[] deck, ref Player player)
        {
            JouerCarte cartesuivante = deck[pointer];

            // Ajoute la prochaine carte du paquet à la main du joueur
            if (player.CarteEnMain < 5)
            {
                player.main[player.CarteEnMain] = cartesuivante;

                // Incrémente le nombre de cartes dans la main du joueur
                player.CarteEnMain++;

                // Ajoute la valeur en points de la nouvelle carte au total du joueur
                player.points += cartesuivante.Points;

                // Affiche les détails de la carte

                // Incrémente le pointeur
                pointer++;
            }
        }


        // Vérifie si le joueur a dépassé 21 points
        // Affiche le total de points du joueur
        static bool VérifierPoints(Player player)
        {
            // Affiche le total de points du joueur

            // Vérifie si le joueur est en panne
            if (player.points > 21)
            {
                Console.WriteLine("Bousiller!");
                return false;
            }
            else
            {
               // Renvoie vrai si le joueur est toujours en vie
               return true;
            }
        }

        // Compare le joueur et le Croupier(dealeur)
        static void CalculerGagnant(Player player, Player dealer)
        {
            // Le joueur gagne si...
            if (dealer.points > 21 || player.CarteEnMain == 5 && dealer.CarteEnMain != 5)
            {
                Console.WriteLine("{0} gagne !", player.nom);
            }

            // La partie se termine par un match nul si... 
            else if (dealer.points == player.points)
            {
                Console.WriteLine("nul!");
            }
            // Sinon, le croupier a gagné
            else if (dealer.points == 21 && player.points != 21 || player.CarteEnMain < 5)
            {
                Console.WriteLine("{0} victoires", dealer.points);
            }
            else if (player.CarteEnMain == 5 && dealer.CarteEnMain == 5)
            {
                if (player.points > dealer.points)
                {
                    Console.WriteLine("{0} gagne ! Tour de 5 cartes : plus élevé que les croupiers.", player.nom);
                }

                else if (player.points == dealer.points)
                {
                    Console.WriteLine("C'est un match nul ! Tour de 5 cartes : idem !");
                }

                Console.WriteLine("{0} gagne ! Tour de 5 cartes : moins que les croupiers.", dealer.nom);
            }
        }

        // Vérifie si le joueur a des as avec une valeur en points de 11 (élevé)
        // Si le joueur est sur le point de faire faillite, changez l'as en une valeur de point de 1 (faible)
        // Puis mettre à jour le score du joueur
        static void VérifierAs(ref Player player)
        {
            bool modifié = false;  // Signale si nous avons déjà changé un as
            if (player.points > 21)
            {
                for (int i = 0; i < player.CarteEnMain; i++)
                {
                    if (player.main[i].Points == 11 && modifié == false)  // Si c'est un as élevé
                    {
                        player.main[i].Points = 1; // Change-le en as bas
                        player.points -= 10; // Retire 10 points aux points du joueur
                        modifié = true;
                    }
                }
            }

        }

        static void UnJoueur()
        {
            string Rejouer = "Indéfini";
            do
            {
                // Génére le jeu de cartes et le mélange
                JouerCarte[] deck = genererCartes();
                mélangerJeux(ref deck);

                // Crée les deux objets joueurs
                Player player = new Player();
                Console.WriteLine("entrez votre nom:");
                player.nom = Console.ReadLine();

                Player dealer = new Player();
                Console.WriteLine("Un nom pour le Croupier :");
                dealer.nom = Console.ReadLine();

                // Pioche les deux premières cartes pour le joueur
                dessinerCarte(deck, ref player);
                dessinerCarte(deck, ref player);


                VérifierAs(ref player); // Appele VerifierAs pour voir si nous pouvons empêcher le joueur de faire faillite
                sortieMain(player);
                VérifierPoints(player);  // Affiche le total de points du joueur
                bool vivant = true;

                string choice = "Indéfini";

                while (vivant == true && choice != "COLLER")
                {
                    Console.WriteLine("Frapper ou Coller ?");
                    choice = Console.ReadLine().ToUpper();
                    if (choice == "FRAPPER") // Si l'utilisateur demande à frapper alors
                    {
                        dessinerCarte(deck, ref player);

                        // Si le joueur a toujours un total de points valide, vivant restera vrai
                        // Si le joueur est maintenant bousiller, vivant deviendra faux et la boucle se terminera
                        VérifierAs(ref player); // Appele VerifierAs pour voir si nous pouvons empêcher le joueur de faire faillite
                        sortieMain(player);
                        vivant = VérifierPoints(player);
                    }
                }
                // Si le joueur n'est pas éliminé, c'est au tour du croupier
                if (vivant == true)
                {
                    bool dealerVivant = true;

                    Console.WriteLine();
                    Console.WriteLine("Tour du Croupier");
                    dessinerCarte(deck, ref dealer);
                    dessinerCarte(deck, ref dealer);

                    VérifierAs(ref dealer); // Appele VérifierAs pour voir si nous pouvons empêcher le dealer de faire faillite
                    sortieMain(dealer);
                    VérifierPoints(dealer);

                    while (dealerVivant == true)
                    {
                        Console.WriteLine("Appuyez sur Entrée pour continuer");
                        Console.ReadLine();

                        // Tire la prochaine carte du croupier et vérifiez s'il est toujours en vie
                        dessinerCarte(deck, ref dealer);

                        VérifierAs(ref dealer); // Appele VérifierAs pour voir si nous pouvons empêcher le dealeur de faire faillite
                        sortieMain(dealer);
                        dealerVivant = VérifierPoints(dealer);
                    }
                }

                // Calcule et affiche le gagnant
                CalculerGagnant(player, dealer);

                Console.WriteLine("Voulez-vous rejouer ? Oui/Non");
                Rejouer = Console.ReadLine().ToUpper();
            } while (Rejouer == "OUI");
        }
    }
}

