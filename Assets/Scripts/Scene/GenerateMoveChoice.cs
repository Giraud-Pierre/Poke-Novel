using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GenerateMoveChoice : MonoBehaviour
{
    /*Script qui sera attaché à la voite de choix des destinations. Il va génèrer
    correctement les boutons de choix des destinations. */

    //Récupère la table indiquant vers quel lieu le joueur peut se déplacer à partir du lieu où il est.
    [SerializeField] private MovementTable MovementTable = default; 

    [SerializeField] private GameObject buttonPrefab = default; //Prefab du bouton destination.

    private Player player; //Récupère le joueur.
    
    
    private void Start()
    {
        //Récupère le lieu dans lequel se trouve le joueur
        player = GameObject.Find("Canvas").GetComponent<Player>();

        //Lance la génération des boutons avec les bonnes destinations en fonction du lieu dans lequel se trouve le joueur.
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
        //Genère les boutons avec la liste de destination.

        //Récupère le premier bouton qui est déjà placé sur la boite de destination.
        GameObject currentButton = gameObject.transform.GetChild(0).gameObject;
        //Lui donne le nom correspondant à la destination et lui dit vers quelle scène il doit renvoyer.
        currentButton.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = mylist[0];
        currentButton.GetComponent<MoveController>().SetMove(mylist[0]);

        if (mylist.Count != 1)
        {
            for (int i = 1; i < mylist.Count; i++)
            {
                //Pour tous les boutons après le premier :
                GameObject nextButton = Instantiate(
                    buttonPrefab,
                    currentButton.transform //Génère le bouton à la position relative par rapport au bouton précédent.
                );
                nextButton.transform.SetParent(currentButton.transform, false); //Le déclare comme enfant du bouton précédent.

                //Lui donne le nom correspondant à la destination et lui dit vers quelle scène il doit renvoyer.
                nextButton.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = mylist[i];
                nextButton.GetComponent<MoveController>().SetMove(mylist[i]);

                //Actualise le bouton précédent pour la génération du bouton suivant.
                currentButton = nextButton;
            }
        }
    }
}
