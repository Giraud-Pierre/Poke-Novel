using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MoveController : MonoBehaviour
{
    /*Ce script est fixé sur les boutons qui permettent les mouvements d'une scène à une autre.
    Ils affichent la bonne boite, soit la boite de dialogue, soit la boite de choix en fonction de si un 
    dialogue doit être déclenché sur cette scène ou non et si ce dialogue adéjà été déclenché ou non. */


    [SerializeField] private GameObject choiceBox = default; //Prefab de la boite de choix de défaut.
    [SerializeField] private GameObject dialogueBox = default; //Prefab de la boite de dialogue.
    [SerializeField] private Dialogues dialogues = default; //Liste de tous les dialogues du jeu.

    //Charge les différents sprites des pièces.
    [SerializeField] private Sprite bedroomSprite = default; 
    [SerializeField] private Sprite livingroomSprite = default ;
    [SerializeField] private Sprite villageSprite = default;
    [SerializeField] private Sprite rivalHouseSprite = default;
    [SerializeField] private Sprite laboratorySprite = default;

    //Charge les différents sprites des personnages.
    [SerializeField] private Sprite playerSprite = default;
    [SerializeField] private Sprite momSprite = default;
    [SerializeField] private Sprite rivalMomSprite = default;
    [SerializeField] private Sprite professorSprite = default;
    [SerializeField] private Sprite rivalSprite = default;
    [SerializeField] private Sprite randomAnnoyingDude = default;

    private GameObject canvas = default; //Va récupérer le canvas pour accèder au code du joueur.
    private GameObject position = default; //Boite qui affiche la pièce dans laquel le joueur se trouve.
    private GameObject background = default; //Image de fond qui affiche la scène dans laquelle le joueur se trouve.

    private string Place = default; //Scène vers laquelle le joueur se déplace s'il clique sur le boutton.
    private List<string> nextDialogue; //Dialogue à afficher si la prochain scene déclenche un dialogue.
    private Player player; //Va récupérer le joueur.

    public void SetMove(string NewPlace) //Sélectionne la scene vers laquel le boutton renvoie.
    {
        Place = NewPlace;
    }
    
    public void ChangeToDialogue(List<string> DialogueText, Sprite Char1, Sprite Char2, int laboratory)
    {
        /*Affiche le dialogue si la scene déclenche un dialogue, avec le bon dialogue, les sprites des 
        deux personnages correspondant et indique si on se trouve dans le laboratoire et dans quelle étape
        on se trouve vis-à-vis du laboratoire:
            - 0 = le joueur n'est pas dans le laboratoire;
            - 1 = le joueur est dans le laboratoire et n'a pas encore choisi son premier pokemon;
            - 2 = le joueur est dans le laboratoire et a choisi son premier pokemon (lance le dialogue RivalBattle);
            - 3 = le joueur est dans le laboratoire et a fini le combat contre le rival. */



        //Génère la boite de dialogue commme enfant du canvas.
        GameObject newDialogue = Instantiate(
            dialogueBox,
            canvas.transform //Position relative par rapport au canvas.
        );
        newDialogue.transform.SetParent(canvas.transform, false); //Enfant du canvas.

        //Affiche les bons sprites et la première partie du dialogue.
        newDialogue.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Char1;
        newDialogue.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = Char2;
        newDialogue.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = DialogueText[0];

        //Retire la première partie du dialogue qui a déjà été affiché et génère le bouton pour continuer le dialogue.
        DialogueText.RemoveAt(0);
        newDialogue.transform.GetChild(2).gameObject.GetComponent<ContinueDialogue>().SetButton(DialogueText,laboratory);
        
        //Détruit la boite en cours contenant les options de choix de mouvements.
        Destroy(GameObject.Find("MovementBox(Clone)"));

    }

    public void ChangeToNewChoice() //Si aucun dialogue ne doit être lancé, affiche la boite de choix par défaut.
    {
        //Génère correctement la boite de choix.
        GameObject newChoice = Instantiate(
            choiceBox,
            canvas.transform //Position relative au canvas.
        );
        newChoice.transform.SetParent(canvas.transform, false); //Enfant du canvas.

        //Détruit la boite en cours contenant les options de choix de mouvements.
        Destroy(GameObject.Find("MovementBox(Clone)"));
    } 

    public void Move()
    {
        //Fonction qui sera déclenchée par le bouton.

        //Récupère les différents objets de la scène qui seront nècessaire par la suite. 
        canvas = GameObject.Find("Canvas");
        background = GameObject.Find("Background");
        position = GameObject.Find("Position");

        //Récupère le joueur à partir du canvas.
        player = canvas.GetComponent<Player>();

        /*Met à jour la boite Position en haut de la scène qui indique la position du joueur
        sauf si le bouton renvoie à la scène "Route 101" car il s'agit de la fin du jeu et il
        n'y a pas encore de scène pour cette route. */
        if (Place != "Route 101")
        {
            player.SetLocation(Place);
            position.GetComponent<TextMeshProUGUI>().text = Place;
        }

        
        switch (Place)
        {

            /*Switch qui décide des différentes possibilités en fonction de la scène vers lequel le bouton renvoie.
            Dans chaque cas, change l'image en fond (sauf dans le cas de la route 101 car il n'y en a pas) puis, 
            dans les cas où un dialogue se passe, vérifie si le dialogue a déjà été déclenché, le déclare comme 
            déclenché si ce n'était pas le cas et l'affiche, sinon affiche le menu choix par défaut. */

            case "Chambre":
                background.GetComponent<Image>().sprite = bedroomSprite;

                ChangeToNewChoice();
                break;



            case "Salon":
                background.GetComponent<Image>().sprite = livingroomSprite;

                if (player.CheckIfDialogueRead(0) == 0)
                {
                    nextDialogue = new List<string>(dialogues.mom);
                    ChangeToDialogue(nextDialogue, playerSprite, momSprite, 0);
                    player.SetDialogueRead(0);
                }
                else
                {
                    ChangeToNewChoice();
                }
                break;



            case "Village":
                background.GetComponent<Image>().sprite = villageSprite;

                ChangeToNewChoice();
                break;



            case "Maison du rival":
                background.GetComponent<Image>().sprite = rivalHouseSprite;

                //Ici, on vérifie si le joueur à des pokemon ou non car le dialogue n'est pas le même.
                if (player.CheckIfDialogueRead(1) == 0 && player.GetPokemon(0) == null) 
                {
                    nextDialogue = new List<string>(dialogues.rivalMomBeforeLab);
                    ChangeToDialogue(nextDialogue, playerSprite, rivalMomSprite, 0);
                    player.SetDialogueRead(1);
                }
                else if (player.CheckIfDialogueRead(2) == 0 && player.GetPokemon(0) != null)
                {
                    nextDialogue = new List<string>(dialogues.rivalMomAfterLab);
                    ChangeToDialogue(nextDialogue, playerSprite, rivalMomSprite, 0);
                    player.SetDialogueRead(2);
                }
                else
                {
                    ChangeToNewChoice();
                }
                break;



            case "Laboratoire":
                background.GetComponent<Image>().sprite = laboratorySprite;

                if(player.CheckIfDialogueRead(3) == 0)
                {
                    //Lance le dialogue avec le professeur juste avant le choix du premier pokemon.
                    nextDialogue = new List<string>(dialogues.professorPine);
                    ChangeToDialogue(nextDialogue, playerSprite, professorSprite, 1);
                    player.SetDialogueRead(3);
                }
                else
                {
                    ChangeToNewChoice();
                }
                break;



            case "Route 101":
                /*On vérifie ici aussi si le dresseur a des pokemon car s'il en a, le jeu se termine, 
                sinon un personnage bien intentionné le renvoie vers le laboratoire. */

                if(player.GetPokemon(0) == null)
                //On ne vérifie pas si le dialogue a déjà eu lieu, car il peut se déclencher autant de fois que nècessaire.
                {
                    nextDialogue = new List<string>(dialogues.randomAnnoyingDude);
                    ChangeToDialogue(nextDialogue, playerSprite, randomAnnoyingDude, 0);
                }
                else
                {
                    player.SetDialogueRead(4); //Déclenche la fin du jeu.
                    nextDialogue = new List<string>(dialogues.end);
                    ChangeToDialogue(nextDialogue, playerSprite, rivalSprite, 0);
                }
                break;
        }
    }
}
