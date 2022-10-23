using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContinueDialogue : MonoBehaviour
{
    //Script qui sera attacher au bouton continuer du dialogue et va g�rer son fonctionnement.


    [SerializeField] GameObject choiceBox = default ; //Récupère la boite de choix pour l'afficher à la fin du dialogue.
    [SerializeField] GameObject endBox = default ; //Récupère l'image de fin.

    //Récupère la boite de choix du premier pokemon pour l'afficher aprés la discussion avec le professeur.
    [SerializeField] GameObject choicePokemonBox = default ;


    [SerializeField] Sprite endSprite = default; //Récupère le sprite pour l'image de fin.

    private List<string> dialogue; //Récupére les dialogues suivants à afficher.
    private int laboratory; //Récupère les différentes situations vis-à-vis du laboratoire (voir fonction ci-dessous).

    public void SetButton(List<string> newDialogue, int isLaboratory)
    {
        //Permet de récupérer les prochains dialogues et la situation vis-à-vis du laboratoire.

        dialogue = newDialogue; //Contient la liste des prochianes r�pliques du dialogue

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

        if(dialogue.Count == 0 && laboratory == 1)
        //Si le dialogue est fini, que le joueur est dans le laboratoire et que le joueur n'a pas encore choisi le pokemon.
        {
            //Affiche correctement la boite de choix du premier pokemon.
            GameObject newChoiceBox = Instantiate(
                choicePokemonBox,
                canvas.transform //En position relative par rapport au canvas.
            );

            newChoiceBox.transform.SetParent(canvas.transform, false); //La déclare en temps qu'enfant du canvas.

            Destroy(gameObject.transform.parent.gameObject); //Détruit la boite de dialogue
        }

        else if (dialogue.Count == 0 && laboratory == 2)
        //si le dialogue est fini et que le joueur vient de choisir son pokemon
        {
            //Debug.Log(GameObject.Find("Canvas").GetComponent<Player>().GetPokemon(0).GetName()); //TEMP Comment acc�der au pokemon. TEMP
            GameObject.Find("Canvas").GetComponent<BattleController>().StartBattle();
        }
        
        else if(dialogue.Count == 0 && canvas.GetComponent<Player>().CheckIfDialogueRead(4) == 1)
        //Si le dialogue est fini et que l'on est à la fin du jeu
        {
            //Génère la boite de fin du jeu correctement.
            GameObject thisIsTheEnd = Instantiate(
                        endBox,
                        canvas.transform); //En position relative par rapport au canvas.
            thisIsTheEnd.transform.SetParent(canvas.transform, false); //Comme enfant du canvas.
            thisIsTheEnd.GetComponent<Image>().sprite = endSprite; //Met le sprite final.
        }
        
        else if (dialogue.Count == 0)
        //Si le dialogue est fini et que l'on n'est pas dans un des cas précédents
        {
            //Affiche boite de choix par d�faut
            GameObject newChoiceBox = Instantiate(
                choiceBox,
                canvas.transform //En position relative par rapport au Canvas.
            );

            newChoiceBox.transform.SetParent(canvas.transform, false); //En tant qu'enfant du Canvas.

            Destroy(gameObject.transform.parent.gameObject); //Détruit la boite de dialogue.
        }

        else 
        //si le dialogue n'est pas fini.
        {
            //On affiche la partie suivante du dialogue puis on la retire de la liste des dialogues à afficher.
            gameObject.transform.parent.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = dialogue[0];
            dialogue.RemoveAt(0);
        }
    }
}
