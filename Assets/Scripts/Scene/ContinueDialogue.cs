using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContinueDialogue : MonoBehaviour
{
    //Script qui sera attacher au bouton continuer du dialogue et va gérer son fonctionnement.


    [SerializeField] GameObject choiceBox = default ; //Récupère la boite de choix pour l'afficher à la fin du dialogue.

    //Récupère la boite de choix du premier pokemon pour l'afficher après la discussion avec le professeur.
    [SerializeField] GameObject choicePokemonBox = default ; 

    private List<string> dialogue; //Récupère les dialogues suivants à afficher.
    private int laboratory; //Récupère les différentes situations vis-à-vis du laboratoire (voir fonction ci-dessous).

    public void SetButton(List<string> newDialogue, int isLaboratory)
    {
        //Permet de récupérer les prochains dialogues et la situation vis-à-vis du laboratoire.

        dialogue = newDialogue; //Contient la liste des prochianes répliques du dialogue

        /*0 si le joueur n'est pas dans le laboratoire, si c'est le cas:
            - 1 s'il n'a pas encore choisi son pokemon;
            - 2 s'il vient de le choisir et que le combat contre le rival va se lancer;
            - 3 si le combat contre le rival est fini. */
        laboratory = isLaboratory; 
    }

    public void Continue()
    {
        //Fonction qui sera déclenchée par le bouton.

        GameObject canvas = GameObject.Find("Canvas"); //Récupère le canvas

        //Si le dialogue est fini, que le joueur est dans le laboratoire et que le joueur n'a pas encore choisi le pokemon.
        if(dialogue.Count == 0 && laboratory == 1)
        {
            //Affiche correctement la boite de choix du premier pokemon.
            GameObject newChoiceBox = Instantiate(
                choicePokemonBox,
                canvas.transform //En position relative par rapport au canvas.
            );

            newChoiceBox.transform.SetParent(canvas.transform, false); //La déclare en temps qu'enfant du canvas.

            Destroy(gameObject.transform.parent.gameObject); //Détruit la boite de dialogue
        }

        //si le dialogue est fini et que le joueur vient de choisir son pokemon
        else if (dialogue.Count == 0 && laboratory == 2)
        {
            Debug.Log(GameObject.Find("Canvas").GetComponent<Player>().GetPokemon(0).GetName()); //TEMP Comment accèder au pokemon. TEMP
            //lance le combat
        }

        //Si le dialogue est fini et que l'on n'est pas dans un des cas précédents
        else if (dialogue.Count == 0) 
        {
            //Affiche boite de choix par défaut
            GameObject newChoiceBox = Instantiate(
                choiceBox,
                canvas.transform //En position relative par rapport au Canvas.
            );

            newChoiceBox.transform.SetParent(canvas.transform, false); //En tant qu'enfant du Canvas.

            Destroy(gameObject.transform.parent.gameObject); //Détruit la boite de dialogue
        }
        else //si le dialogue n'est pas fini
        {
            //On affiche la partie suivante du dialogue puis on la retire de la liste des dialogues à afficher.
            gameObject.transform.parent.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = dialogue[0];
            dialogue.RemoveAt(0);
        }
    }
}
