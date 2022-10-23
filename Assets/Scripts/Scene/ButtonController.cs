using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    //Fonction qui va g�rer les boutons du menu start.


    [SerializeField] private GameObject currentMenu = default; //R�cup�re le menu sur lequel on se trouve.
    [SerializeField] private GameObject nextMenu = default; //R�cup�re le prochain menu � afficher.

    public void ChangeMenu()
    {
        GameObject canvas = GameObject.Find("Canvas"); //R�cup�re le canvas.

        //Affiche correctement le prochain menu.
        GameObject newMenu = Instantiate(
            nextMenu,
            canvas.transform //En position relative par rapport au Canvas.
        );

        newMenu.transform.SetParent (canvas.transform, false); //En tant qu'enfant du Canvas.

        Destroy(currentMenu); //D�truit le menu actuel.
    }
}
