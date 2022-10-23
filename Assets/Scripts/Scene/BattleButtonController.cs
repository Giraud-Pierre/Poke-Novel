using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleButtonController : MonoBehaviour
{
    //Script qui va être fixé sur les boutons des menu en combat et gérer leur fonctionnement.


    private enum ButtonType //Enumère les différents menu possible sur lesquels peuvent être les boutons.
    {
        menu, //Menu princpal.
        getAttacks, //Menu de sélection de l'attaque.
        pokemon, //Menu de visualisation des pokemon.
        bag, //Menu du sac.
        flee, //Menu de la fuite.
        attack1, //Bouton de l'attaque 1 sur le menu de sélection de l'attaque.
        attack2, //Bouton de l'attaque 2 sur le menu de sélection de l'attaque.
        attack3, //Bouton de l'attaque 3 sur le menu de sélection de l'attaque.
        attack4, //Bouton de l'attaque 4 sur le menu de sélection de l'attaque.
        next //Bouton continuer sur les messages affichés en combat.
    };

    //On va rentrer ici le menu sur lequel se trouve le bouton (par défaut sur le menu principal de combat).
    [SerializeField] private ButtonType buttonType = ButtonType.menu; 

    //Va récupérer le reste du texte à afficher si on doit afficher un texte.
    private List<string> dialogue;

    public void OnClick() //Gère ce que fait le bouton en fonction du menu sur lequel il est
    {
        /*Récupère le script BattleController sur lequel se trouve
        les fonctions qui vont être appelées dans les différents cas */
        BattleController battleController = GameObject.Find("Canvas").gameObject.GetComponent<BattleController>();

        switch (buttonType)
        {
            //Appelle les différentes fonction  de BattleController en fonction du bouton

            case ButtonType.menu :
                battleController.Menu();
                break;
            case ButtonType.getAttacks :
                battleController.ShowAttack();
                break;
            case ButtonType.pokemon :
                battleController.Pokemon();
                break;
            case ButtonType.bag :
                battleController.Bag();
                break;
            case ButtonType.flee :
                battleController.Flee();
                break;
            case ButtonType.attack1 :
                battleController.UseAttack(0);
                break;
            case ButtonType.attack2 :
                battleController.UseAttack(1);
                break;
            case ButtonType.attack3 :
                battleController.UseAttack(2);
                break;
            case ButtonType.attack4 :
                battleController.UseAttack(3);
                break;
            case ButtonType.next :
                if (dialogue.Count != 0) //Si la liste des messages n'est pas finie.
                {
                    //On affiche le message suivant.
                    gameObject.transform.parent.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = dialogue[0];

                    //On retire ce message de la liste des messages.
                    dialogue.RemoveAt(0);
                }
                else { //Si la liste de message est fini, on revient sur le menu principal de combat.
                    battleController.Menu();
                }
                break;
        }
    }

    public void SetDialogue(List<string> newDialogue)
    {
        //Permet de récupèrer les prochains dialogues
        dialogue = newDialogue; //Contient la liste des prochianes répliques du dialogue
    }
}
