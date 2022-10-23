using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GenerateMoveChoice : MonoBehaviour
{
    /*Script qui sera attach� � la voite de choix des destinations. Il va g�n�rer
    correctement les boutons de choix des destinations. */

    //R�cup�re la table indiquant vers quel lieu le joueur peut se d�placer � partir du lieu o� il est.
    [SerializeField] private MovementTable MovementTable = default; 

    [SerializeField] private GameObject buttonPrefab = default; //Prefab du bouton destination.

    private Player player; //R�cup�re le joueur.
    
    
    private void Start()
    {
        //R�cup�re le lieu dans lequel se trouve le joueur
        player = GameObject.Find("Canvas").GetComponent<Player>();

        //Lance la g�n�ration des boutons avec les bonnes destinations en fonction du lieu dans lequel se trouve le joueur.
        switch (player.GetLocation())
        {
            case "Chambre":
                GenerateButton(MovementTable.Bedroom);
                break ;
            case "Salon":
                GenerateButton(MovementTable.Livingroom);
                break ;
            case "Village":
                GenerateButton(MovementTable.Village);
                break;
            case "Maison du rival":
                GenerateButton(MovementTable.RivalHouse);
                break;
            case "Laboratoire":
                GenerateButton(MovementTable.Laboratory);
                break;
        }
    }

    private void GenerateButton(List<string> mylist)
    {
        //Gen�re les boutons avec la liste de destination.

        //R�cup�re le premier bouton qui est d�j� plac� sur la boite de destination.
        GameObject currentButton = gameObject.transform.GetChild(0).gameObject;
        //Lui donne le nom correspondant � la destination et lui dit vers quelle sc�ne il doit renvoyer.
        currentButton.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = mylist[0];
        currentButton.GetComponent<MoveController>().SetMove(mylist[0]);

        if (mylist.Count != 1)
        {
            for (int i = 1; i < mylist.Count; i++)
            {
                //Pour tous les boutons apr�s le premier :
                GameObject nextButton = Instantiate(
                    buttonPrefab,
                    currentButton.transform //G�n�re le bouton � la position relative par rapport au bouton pr�c�dent.
                );
                nextButton.transform.SetParent(currentButton.transform, false); //Le d�clare comme enfant du bouton pr�c�dent.

                //Lui donne le nom correspondant � la destination et lui dit vers quelle sc�ne il doit renvoyer.
                nextButton.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = mylist[i];
                nextButton.GetComponent<MoveController>().SetMove(mylist[i]);

                //Actualise le bouton pr�c�dent pour la g�n�ration du bouton suivant.
                currentButton = nextButton;
            }
        }
    }
}
