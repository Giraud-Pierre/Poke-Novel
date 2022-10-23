using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MoveController : MonoBehaviour
{
    /*Ce script est fix� sur les boutons qui permettent les mouvements d'une sc�ne � une autre.
    Ils affichent la bonne boite, soit la boite de dialogue, soit la boite de choix en fonction de si un 
    dialogue doit �tre d�clench� sur cette sc�ne ou non et si ce dialogue ad�j� �t� d�clench� ou non. */


    [SerializeField] private GameObject choiceBox = default; //Prefab de la boite de choix de d�faut.
    [SerializeField] private GameObject dialogueBox = default; //Prefab de la boite de dialogue.
    [SerializeField] private Dialogues dialogues = default; //Liste de tous les dialogues du jeu.

    //Charge les diff�rents sprites des pi�ces.
    [SerializeField] private Sprite bedroomSprite = default; 
    [SerializeField] private Sprite livingroomSprite = default ;
    [SerializeField] private Sprite villageSprite = default;
    [SerializeField] private Sprite rivalHouseSprite = default;
    [SerializeField] private Sprite laboratorySprite = default;

    //Charge les diff�rents sprites des personnages.
    [SerializeField] private Sprite playerSprite = default;
    [SerializeField] private Sprite momSprite = default;
    [SerializeField] private Sprite rivalMomSprite = default;
    [SerializeField] private Sprite professorSprite = default;
    [SerializeField] private Sprite rivalSprite = default;
    [SerializeField] private Sprite randomAnnoyingDude = default;

    private GameObject canvas = default; //Va r�cup�rer le canvas pour acc�der au code du joueur.
    private GameObject position = default; //Boite qui affiche la pi�ce dans laquel le joueur se trouve.
    private GameObject background = default; //Image de fond qui affiche la sc�ne dans laquelle le joueur se trouve.

    private string Place = default; //Sc�ne vers laquelle le joueur se d�place s'il clique sur le boutton.
    private List<string> nextDialogue; //Dialogue � afficher si la prochain scene d�clenche un dialogue.
    private Player player; //Va r�cup�rer le joueur.

    public void SetMove(string NewPlace) //S�lectionne la scene vers laquel le boutton renvoie.
    {
        Place = NewPlace;
    }
    
    public void ChangeToDialogue(List<string> DialogueText, Sprite Char1, Sprite Char2, int laboratory)
    {
        /*Affiche le dialogue si la scene d�clenche un dialogue, avec le bon dialogue, les sprites des 
        deux personnages correspondant et indique si on se trouve dans le laboratoire et dans quelle �tape
        on se trouve vis-�-vis du laboratoire:
            - 0 = le joueur n'est pas dans le laboratoire;
            - 1 = le joueur est dans le laboratoire et n'a pas encore choisi son premier pokemon;
            - 2 = le joueur est dans le laboratoire et a choisi son premier pokemon (lance le dialogue RivalBattle);
            - 3 = le joueur est dans le laboratoire et a fini le combat contre le rival. */



        //G�n�re la boite de dialogue commme enfant du canvas.
        GameObject newDialogue = Instantiate(
            dialogueBox,
            canvas.transform //Position relative par rapport au canvas.
        );
        newDialogue.transform.SetParent(canvas.transform, false); //Enfant du canvas.

        //Affiche les bons sprites et la premi�re partie du dialogue.
        newDialogue.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Char1;
        newDialogue.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = Char2;
        newDialogue.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = DialogueText[0];

        //Retire la premi�re partie du dialogue qui a d�j� �t� affich� et g�n�re le bouton pour continuer le dialogue.
        DialogueText.RemoveAt(0);
        newDialogue.transform.GetChild(2).gameObject.GetComponent<ContinueDialogue>().SetButton(DialogueText,laboratory);
        
        //D�truit la boite en cours contenant les options de choix de mouvements.
        Destroy(GameObject.Find("MovementBox(Clone)"));

    }

    public void ChangeToNewChoice() //Si aucun dialogue ne doit �tre lanc�, affiche la boite de choix par d�faut.
    {
        //G�n�re correctement la boite de choix.
        GameObject newChoice = Instantiate(
            choiceBox,
            canvas.transform //Position relative au canvas.
        );
        newChoice.transform.SetParent(canvas.transform, false); //Enfant du canvas.

        //D�truit la boite en cours contenant les options de choix de mouvements.
        Destroy(GameObject.Find("MovementBox(Clone)"));
    } 

    public void Move()
    {
        //Fonction qui sera d�clench�e par le bouton.

        //R�cup�re les diff�rents objets de la sc�ne qui seront n�cessaire par la suite. 
        canvas = GameObject.Find("Canvas");
        background = GameObject.Find("Background");
        position = GameObject.Find("Position");

        //R�cup�re le joueur � partir du canvas.
        player = canvas.GetComponent<Player>();

        /*Met � jour la boite Position en haut de la sc�ne qui indique la position du joueur
        sauf si le bouton renvoie � la sc�ne "Route 101" car il s'agit de la fin du jeu et il
        n'y a pas encore de sc�ne pour cette route. */
        if (Place != "Route 101")
        {
            player.SetLocation(Place);
            position.GetComponent<TextMeshProUGUI>().text = Place;
        }

        
        switch (Place)
        {

            /*Switch qui d�cide des diff�rentes possibilit�s en fonction de la sc�ne vers lequel le bouton renvoie.
            Dans chaque cas, change l'image en fond (sauf dans le cas de la route 101 car il n'y en a pas) puis, 
            dans les cas o� un dialogue se passe, v�rifie si le dialogue a d�j� �t� d�clench�, le d�clare comme 
            d�clench� si ce n'�tait pas le cas et l'affiche, sinon affiche le menu choix par d�faut. */

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

                //Ici, on v�rifie si le joueur � des pokemon ou non car le dialogue n'est pas le m�me.
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
                /*On v�rifie ici aussi si le dresseur a des pokemon car s'il en a, le jeu se termine, 
                sinon un personnage bien intentionn� le renvoie vers le laboratoire. */

                if(player.GetPokemon(0) == null)
                //On ne v�rifie pas si le dialogue a d�j� eu lieu, car il peut se d�clencher autant de fois que n�cessaire.
                {
                    nextDialogue = new List<string>(dialogues.randomAnnoyingDude);
                    ChangeToDialogue(nextDialogue, playerSprite, randomAnnoyingDude, 0);
                }
                else
                {
                    player.SetDialogueRead(4); //D�clenche la fin du jeu.
                    nextDialogue = new List<string>(dialogues.end);
                    ChangeToDialogue(nextDialogue, playerSprite, rivalSprite, 0);
                }
                break;
        }
    }
}
