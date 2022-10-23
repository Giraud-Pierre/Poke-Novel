using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PokemonChoiceButtonController : MonoBehaviour
{
    /*Gère les boutons du choix de premier pokemon.
    Donne le pokemon correspondant au joueur, lance le dialogue contre le rival
    et détruit la boite de choix du premier pokemon.*/

    [SerializeField] private GameObject dialogueBox = default; //Prefab de la prochaine boite à afficher.

    [SerializeField] private Dialogues dialogues = default; //Liste des dialogues.
    [SerializeField] private Pokedex pokedex = default; //Liste des pokemon.
    [SerializeField] private CapacityList capacityList = default; //Liste des capacités.
    
    //Sprite du joueur et du rival pour les afficher dans le dialogue qui va suivre.
    [SerializeField] private Sprite playerSprite = default;
    [SerializeField] private Sprite rivalSprite = default;

    private int pokemonIndex;//Va recueillir l'ID du pokemon choisi.
    private GameObject canvas; //Va recueillir le canvas pour pouvoir aller chercher le code du joueur qui est dessus.

    public void SetPokemonSheet(int newPokemonIndex) //Sélectionne l'ID du pokemon à ajouter si on clique sur le bouton.
    {
        pokemonIndex = newPokemonIndex;
    }
    
    private void ChangeToDialogue(List<string> dialogueText, Sprite char1, Sprite char2, int laboratory)
    {
        /*Lance le dialogue avec le rival qui va suivre après le choix du pokemon à partir
         des dialogues restant à afficher, des sprites des personnages du dialogues et du stade
        vis-à-vis du laboratoire (ici 2 = le personnage est dans le laboratoire
        et a choisi son pokemon). */


        //Instantie la boite de dialogue comme enfant du canvas.
        GameObject newDialogue = Instantiate(
            dialogueBox,
            canvas.transform //Positionne l'objet en position relative par rapport au canvas.
        );
        newDialogue.transform.SetParent(canvas.transform, false); //Déclare l'objet comme enfant du canvas.

        //Place les sprites et la première partie du dialogue aux bonnes places.
        newDialogue.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = char1;
        newDialogue.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = char2;
        newDialogue.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = dialogueText[0];

        //Retire la première partie du dialogue (comme elle a déjà été affiché) puis crée le bouton pour continuer le dialogue.
        dialogueText.RemoveAt(0);
        newDialogue.transform.GetChild(2).gameObject.GetComponent<ContinueDialogue>().SetButton(dialogueText, laboratory);
    }

    public void ChoosePokemon()
    {
        //Fonction qui sera déclenchée par le bouton.

        canvas = GameObject.Find("Canvas"); //Récupère le canvas.

        //Récupère le dialogue avec le rival et l'affiche.
        List<string> rivalDialogue = new List<string>(dialogues.rivalBattle);
        ChangeToDialogue(rivalDialogue, playerSprite, rivalSprite, 2);

        //Crée le pokemon, l'initialise avec le "pseudo-constructeur" et l'ajoute à l'inventaire du joueur. 
        Pokemon starter = canvas.AddComponent<Pokemon>();
        starter.SetPokemon(pokemonIndex, pokedex, capacityList);
        canvas.GetComponent<Player>().AddPokemon(starter);

        //Détruit la boite de choix du premier pokemon.
        Destroy(GameObject.Find("ChoicePokemonBox(Clone)"));
    }
}
