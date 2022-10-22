using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContinueDialogue : MonoBehaviour
{
    [SerializeField] GameObject choiceBox = default ;
    [SerializeField] GameObject choicePokemonBox = default ;

    private List<string> dialogue;
    private int laboratory;

    public void SetButton(List<string> newDialogue, int isLaboratory)
    {
        dialogue = newDialogue; //Contient la liste des prochianes répliques du dialogue

        /*0 si le joueur n'est pas dans le laboratoire, si c'est le cas:
            - 1 s'il n'a pas encore choisi son pokemon
            - 2 s'il vient de le choisir et que le combat contre le rival va se lancer
            - 3 si le combat contre le rival est fini*/
        laboratory = isLaboratory; 
    }

    public void Continue()
    {
        GameObject canvas = GameObject.Find("Canvas");

        //si le dialogue est fini, que le joueur est dans le laboratoire et que le joueur n'a pas encore choisi le pokemon
        if(dialogue.Count == 0 && laboratory == 1)
        {
            GameObject newChoiceBox = Instantiate(
                choicePokemonBox,
                canvas.transform
            );

            newChoiceBox.transform.SetParent(canvas.transform, false);
            Destroy(gameObject.transform.parent.gameObject);
        }
        //si le dialogue est fini et que le joueur vient de choisir son pokemon
        else if (dialogue.Count == 0 && laboratory == 2)
        {
            //lance le combat
        }

        //Si le dialogue est fini et que l'on n'est pas dans les cas précédent
        else if (dialogue.Count == 0) 
        {
            GameObject newChoiceBox = Instantiate(
                choiceBox,
                canvas.transform
            );

            newChoiceBox.transform.SetParent(canvas.transform, false);

            Destroy(gameObject.transform.parent.gameObject);
        }
        else //si le dialogue n'est pas fini
        {
            gameObject.transform.parent.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = dialogue[0];
            dialogue.RemoveAt(0);
        }
    }
}
