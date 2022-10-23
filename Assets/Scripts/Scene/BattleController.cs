using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Rendering;

public class BattleController : MonoBehaviour
{
    /*Script qui sera fixé sur le canvas et qui va permettre le lancement et la gestion
     du combat. */

    enum WhoDied {nobody, player, opponent} //Etat du combat (si un des deux pokemon est KO).

    //Charge les différentes boites qui vont être nècessaire au combat.
    [SerializeField] private GameObject battleAttackBox  = default; //Boite de choix des attaques.
    [SerializeField] private GameObject battleBagBox = default; //Boite du sac en combat.
    [SerializeField] private GameObject battleMenuBox = default; //Boite du menu de base en combat.
    [SerializeField] private GameObject battlePokemonBox = default; //Boite d'affichage des pokemon en combat.
    [SerializeField] private GameObject fleeBox = default ; //Boite d'affichage de texte en combat.
    [SerializeField] private GameObject dialogueBoxPrefab  = default; //boite de dialogue à la fin du combat

    //Charge les différents sprite du joueur et du rival.
    [SerializeField] private Sprite playerSprite = default;
    [SerializeField] private Sprite rivalSprite = default;

    //Charge les différentes ScriptableObject qui regroupe les données sur les dialogues, les pokemon et les capacités.
    [SerializeField] private Dialogues dialogues = default;
    [SerializeField] private Pokedex pokedex = default;
    [SerializeField] private CapacityList capacityList = default;

    private Pokemon playerPokemon; //Récupère le pokemon du joueur.
    private Pokemon opponentPokemon; //Récupère le pokemon adverse.
    private WhoDied whoDied; //Etat de qui est KO.
    private List<string> DialogueAfterBattle; //Donne le dialogue à lancer après le combat.


    public void StartBattle()
    {
        //Lancé au début du combat


        whoDied = WhoDied.nobody; //Personne n'est KO au début.

        playerPokemon = gameObject.GetComponent<Player>().GetPokemon(0); //On récupère le pokemon du joueur

        if (playerPokemon == null) Debug.Log("Tu n’a pas de pokemon");
        else
        {
            //On génère le pokemon du rival, qui sera toujours le point faible de celui du joueur.
            opponentPokemon = InstantiateOpponentPokemon();

            //Initialise les modificateurs de stats des deux pokemons.
            playerPokemon.InitializeModifiers(); 
            opponentPokemon.InitializeModifiers();

            Menu(); //Lance le menu principal du combat.
        }
    }

    public void Menu()
    {
        //Menu principal du combat qui permet les choix entre attaques, sacs, pokemon et fuite.


        switch (whoDied)
            //Vérifie si un pokemon est KO.
        {
            case WhoDied.nobody : //Tant que personne n'est KO, On affiche le menu avec les bons sprites pour les 2 pokemons.
                GameObject dialogueBox = ChangeUI(battleMenuBox);
                dialogueBox.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = playerPokemon.GetSprite();
                dialogueBox.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = opponentPokemon.GetSprite();
                break;
            case WhoDied.player : //Si le pokemon du joueur est KO, lance le dialogue RivalDefeat.
                DialogueAfterBattle = new List<string>(dialogues.rivalDefeat);
                ChangeToDialogue(DialogueAfterBattle);
                break;
            case WhoDied.opponent : //Si le pokemon du rival est KO, lance le dialogue RivalVictory.
                DialogueAfterBattle = new List<string>(dialogues.rivalVictory);
                ChangeToDialogue(DialogueAfterBattle);
                break;
        }
    }

    public void ShowAttack()
    {
        //Lance le menu qui permet de choisir quelle attaque utiliser.

        //Affiche le menu des attaques avec les bon sprites.
        GameObject dialogueBox = ChangeUI(battleAttackBox); 
        dialogueBox.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = playerPokemon.GetSprite();
        dialogueBox.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = opponentPokemon.GetSprite();

        //Affiche correctement le nom des boutons d'attaque pour chaque attaque du pokemon du joueur.
        int attackIndex = 0;
        while(playerPokemon.GetCapacityName(attackIndex) != "")
        {
            dialogueBox.transform.GetChild(attackIndex + 2).GetChild(0).
                gameObject.GetComponent<TextMeshProUGUI>().
                text = playerPokemon.GetCapacityName(attackIndex);

            attackIndex++;
        }
        //Détruit les boutons non attribué quand le pokemon n'a plus d'autre attaque. 
        for(;attackIndex < 4; attackIndex++) { Destroy(dialogueBox.transform.GetChild(attackIndex + 2).gameObject); }
    }

    public void UseAttack(int index)
    {
        /*Lance l'attaque. Le pokemon du joueur attaque puis le pokemon du rival
        attaque, indépendemment de la vitesse. */


        List<string> dialogueText = new List<string>(); //Va récupèrer la listes des messages à afficher.
        string message = ""; //Récupère chaque message à afficher un par un.

        //Paramètre la seed du UnityEngine.Random de façon aléatoire en fonction de l'heure.
        Random.InitState((int)System.DateTime.Now.Ticks); 

        int precision = playerPokemon.GetCapacityPrecision(index); //Récupère la précision de l'attaque du joueur
        if (precision < Mathf.RoundToInt(Random.value * 100)) //Regarde si le joueur a raté son attaque.
        {
            //On ajoute alors à la liste des messages que le pokemon a raté son attaque.
            dialogueText.Add(
                string.Format("{0} rate son attaque", playerPokemon.GetName()) 
            );
        }
        else { //S'il n'a pas raté, lance l'attaque.
            message = DoCapacity(playerPokemon, opponentPokemon, index); //Effectue l'attaque et renvoie le message à afficher
            dialogueText.Add(message); //Rajoute le message à afficher à la liste des messages à afficher
        }

        if (opponentPokemon.GetHealth() != 0) //Si le pokemon du rival n'est pas KO.
        {
            //Sélectionne une attaque au hasard dans les attaques du pokemon du rival. 
            index = Mathf.RoundToInt(Random.Range(0f, opponentPokemon.GetNumberCapacities() - 1));

            precision = opponentPokemon.GetCapacityPrecision(index); //Récupère sa précision.
            if (precision < Mathf.RoundToInt(Random.value * 100))  //Regarde s'il a raté son attaque.
            {
                dialogueText.Add(
                    string.Format("{0} rate son attaque", opponentPokemon.GetName())
                );
            }
            else { //S'il n'a pas raté, lance l'attaque.
                message = DoCapacity( opponentPokemon, playerPokemon, index);
                dialogueText.Add(message);

                if (playerPokemon.GetHealth() == 0) //Si le pokemon du joueur est KO.
                {
                    //Ajoute à la liste de message que le pokemon du joueur est KO.
                    dialogueText.Add(
                        string.Format("{0} est KO ", playerPokemon.GetName())
                    ); 
                    whoDied = WhoDied.player; //Déclare le pokemon du joueur comme KO.
                }
            }
        }
        else //Si le pokemon du rival est KO 
        {
            //Ajoute à la liste de message que le pokemon du rival est KO.
            dialogueText.Add(
                string.Format("{0} est KO ", opponentPokemon.GetName())
            );
            whoDied = WhoDied.opponent;//Déclare le pokemon du rival comme KO.
        }

        AttackDialogue(dialogueText); //Affiche les messages consécutifs à l'attaque.
    }

    public void Pokemon() //Affiche le menu Pokemon en combat.
    {
        ChangeUI(battlePokemonBox);
    }

    public void Bag() //Affiche le menu sac en combat.
    {
        ChangeUI(battleBagBox);
    }

    public void Flee() //Affiche la boite de fuite.
    {
        //Génère la boite de fuite en mettant les bons sprites.
        GameObject dialogueBox = ChangeUI(fleeBox);
        dialogueBox.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = playerPokemon.GetSprite();
        dialogueBox.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = opponentPokemon.GetSprite();

        //Dit au joueur qu'il ne peut pas fuir un combat contre un dresseur.
        dialogueBox.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().
            text = "Vous ne pouvez pas fuir d'un combat contre un dresseur !";

        //Il n'y a pas de message après le précédent, donc on met une liste vide.
        dialogueBox.transform.GetChild(2).gameObject.GetComponent<BattleButtonController>().SetDialogue(new List<string>());
    }

    private Pokemon InstantiateOpponentPokemon() //Génère le pokemon starter du rival.
    {
        Pokemon starter = gameObject.AddComponent<Pokemon>(); //génère le pokemon.

        //Choisi l'id du pokemon qui sera la faiblesse du starter du pokemon du joueur.
        int pokemonIndex = playerPokemon.GetIndex() + 1;
        if (pokemonIndex == 3) pokemonIndex = 0;

        //Utilise le "pseudo-constructeur" de pokemon pour initialiser le pokemon du rival.
        starter.SetPokemon(pokemonIndex, pokedex, capacityList); 

        return starter; //Renvoie le pokemon du rival.
    }

    private GameObject ChangeUI(GameObject nextMenu) //Génère la prochaine boite à afficher.
    {
        GameObject newMenu = Instantiate(
            nextMenu,
            gameObject.transform //En position relative par rapport au canvas.
        );

        newMenu.transform.SetParent(gameObject.transform, false); //En tant qu'enfant du canvas.

        Destroy(gameObject.transform.GetChild(2).gameObject); //Détruit la boite précédente

        return newMenu; //Renvoie la nouvelle boite affichée.
    }

    private void AttackDialogue(List<string> dialogueText)
    {
        //Affiche les messages consécutifs à l'attaque du joueur.


        //Génère la boite de fuite et l'utilise comme modèle pour ces messages.
        GameObject dialogueBox = ChangeUI(fleeBox); 

        //Affiche les bons sprites et la première partie du dialogue.
        dialogueBox.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = playerPokemon.GetSprite();
        dialogueBox.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = opponentPokemon.GetSprite();
        dialogueBox.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = dialogueText[0];

        //Retire la première partie de la liste qui a déjà été affichée et génère le bouton pour continuer le dialogue.
        dialogueText.RemoveAt(0);

        //Fourni la liste des messages restant à afficher aux boutons continuer de la fleeBox.
        dialogueBox.transform.GetChild(2).gameObject.GetComponent<BattleButtonController>().SetDialogue(dialogueText);
    }

    private void ChangeToDialogue(List<string> dialogueText)
    {
        //Lance le dialogue à la fin du combat.


        //Génère la boite de dialogue.
        GameObject newDialogue = ChangeUI(dialogueBoxPrefab);

        //Affiche les bons sprites et la première partie du dialogue.
        newDialogue.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = playerSprite;
        newDialogue.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = rivalSprite;
        newDialogue.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = dialogueText[0];

        //Retire la première partie du dialogue qui a déjà été affichée et génère le bouton pour continuer le dialogue.
        dialogueText.RemoveAt(0);

        /*Paramètre le bouton pour continuer le dialogue en rentrant le reste des dialogues à afficher et la situation
         vis-à-vis du laboratoire (ici 3 = le joueur est dans le laboratiore et le combat contre le rival est fini. */
        newDialogue.transform.GetChild(2).gameObject.GetComponent<ContinueDialogue>().SetButton(dialogueText, 3);
    }

    private string GetStatsType(int index)
    {
        /*Permet d'obtenir le nom de la stats en cas d'augmentation ou de diminution
        de stat pour l'afficher correctement dans le message à la fin. */


        if (index == 0) return "L'attaque";
        else if (index == 1) return "La defense";
        else if (index == 2) return "La vitesse";
        else return "Erreur d'indice de stats";
    }

    private string DoCapacity(Pokemon initiator, Pokemon target, int indexCapacity)
    {
        /*Calcul des effets de l'attaque en fonction du pokemon lanceur, du pokemon cible 
        et du numéro de la capcité dans la liste de capacité du lanceur.
        
         Renvoi le message à afficher en conséquence de cette attaque. */


        if(initiator.GetCapacityType(indexCapacity) == 0)
            //Si l'attaque est une attaque qui fait des dégâts.
        {
            //Récupère les stats modifiés pour le calcul des dégâts.
            float initiatorAttack = initiator.GetStats()[1] * initiator.GetModifiers()[0];
            int capacityPower = initiator.GetCapacityPower(indexCapacity);
            float targetDefence = target.GetStats()[2] * target.GetModifiers()[1];

            //Calcul des dégâts en arrondissant à l'entier le plus proche.
            int damage = Mathf.RoundToInt(
                                            (float)initiatorAttack * (float)capacityPower * ((float)targetDefence 
                                            / 
                                            (1500f + (float)targetDefence))
                                         );

            //Change la vie de la cible (les overflows sont gérés par la fonction SetHealth de la classe Pokemon).
            target.SetHealth(target.GetHealth() - damage);

            //Construit et renvoie le message à afficher en conséquence de cette attaque.
            return string.Format("{0} lance {1}. \n{2} subit {3} dégâts",
                                    initiator.GetName(),
                                    initiator.GetCapacityName(indexCapacity),
                                    target.GetName(),
                                    damage
                                );
        }

        else if (initiator.GetCapacityType(indexCapacity) == 1)
            //Si l'attaque est une attaque qui augmente une stat du lanceur.
        {
            /*On récupère la puissance qui va déterminer si l'attaque augmente
            beaucoup ou non et quelle stat elle augmente. */
            int power = initiator.GetCapacityPower(indexCapacity);

            if(power < 10) 
                //Si l'attaque augmente un peu la stat.
            {
                //Modifie le modificateur de la stat correspondante du lanceur.
                initiator.SetModifier(initiator.GetModifiers()[power] + 0.25f * initiator.GetModifiers()[power], power);

                //Construit et renvoie le message consécutif à l'attaque.
                return string.Format("{0} lance {1}. \n{2} de {3} augmente",
                                    initiator.GetName(),
                                    initiator.GetCapacityName(indexCapacity),
                                    GetStatsType(power), //Donne le nom de la stat augmentée.
                                    initiator.GetName()
                                );
            }
            else
            //Si l'attaque augmente beaucoup la stat.
            {
                power = -10; //On retire la partie du power qui sert à indiquer ce "beaucoup".

                //Modifie le modificateur de la stat correspondante du lanceur.
                initiator.SetModifier(initiator.GetModifiers()[power] + 0.50f * initiator.GetModifiers()[power], power);

                //Construit et renvoie le message consécutif à l'attaque.
                return string.Format("{0} lance {1}. \n{2} de {3} augmente beaucoup",
                                    initiator.GetName(),
                                    initiator.GetCapacityName(indexCapacity),
                                    GetStatsType(power), //Donne le nom de la stat augmentée.
                                    initiator.GetName()
                                );
            }
        }
        else if (initiator.GetCapacityType(indexCapacity) == 2)
        //Si l'attaque est une attaque qui diminue une stat de la cible.
        {
            /*On récupère la puissance qui va déterminer si l'attaque diminue
            beaucoup ou non et quelle stat elle diminue. */
            int power = initiator.GetCapacityPower(indexCapacity);

            if (power < 10)
            //Si l'attaque diminue un peu la stat.
            {
                //Modifie le modificateur de la stat correspondante de la cible.
                target.SetModifier(target.GetModifiers()[power] - 0.15f * target.GetModifiers()[power], power);

                //Construit et renvoie le message consécutif à l'attaque.
                return string.Format("{0} lance {1}. \n{2} de {3} diminue",
                                    initiator.GetName(),
                                    initiator.GetCapacityName(indexCapacity),
                                    GetStatsType(power), //Donne le nom de la stat diminuée.
                                    target.GetName()
                                );
            }
            else
            //Si l'attaque diminue beaucoup la stat.
            {
                power = -10; //On retire la partie du power qui sert à indiquer ce "beaucoup".

                //Modifie le modificateur de la stat correspondante de la cible.
                target.SetModifier(target.GetModifiers()[power] - 0.30f * target.GetModifiers()[power], power);

                //Construit et renvoie le message consécutif à l'attaque.
                return string.Format("{0} lance {1}. \n{2} de {3} diminue beaucoup",
                                    initiator.GetName(),
                                    initiator.GetCapacityName(indexCapacity), //Donne le nom de la stat diminuée.
                                    GetStatsType(power),
                                    target.GetName()
                                );
            }
        }
        else
        //Si le type ne correspond à aucun des types précédents, renvoie un erreur.
        {
            return "Erreur de type de capacité.";
        }
    }
}
