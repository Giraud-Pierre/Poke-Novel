using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    //Fonction qui va gérer les boutons du menu start.


    [SerializeField] private GameObject currentMenu = default; //Récupère le menu sur lequel on se trouve.
    [SerializeField] private GameObject nextMenu = default; //Récupère le prochain menu à afficher.

    public void ChangeMenu()
    {
        GameObject canvas = GameObject.Find("Canvas"); //Récupère le canvas.

        //Affiche correctement le prochain menu.
        GameObject newMenu = Instantiate(
            nextMenu,
            canvas.transform //En position relative par rapport au Canvas.
        );

        newMenu.transform.SetParent (canvas.transform, false); //En tant qu'enfant du Canvas.

        Destroy(currentMenu); //Détruit le menu actuel.
    }
}
