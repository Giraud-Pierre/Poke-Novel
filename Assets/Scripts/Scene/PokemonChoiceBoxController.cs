using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PokemonChoiceBoxController : MonoBehaviour
{
    /*Script plac� sur la boite de choix du premier pokemon et g�n�re les boutons 
    pour choisir le-dit pokemon. */

    [SerializeField] private GameObject buttonPrefab = default; //Prefab du bouton de choix du pokemon.
    [SerializeField] private Pokedex pokedex = default; //Liste des pokemons.


    void Start()
    {
        //G�n�re les boutons avec les pokemons correspondants aux ID 0, 1 et 2.
        GenerateButton(new List<int> { 0, 1, 2}); 
    }

    private void GenerateButton(List<int> starters) //Gen�re les boutons.
    {
        //R�cup�re le premier bouton qui est d�j� plac� sur la boite de choix du premier pokemon.
        GameObject currentButton = gameObject.transform.GetChild(1).gameObject;

        //S�lectionne la bonne fiche PokemonSheet pour le premier bouton et affiche le bon sprite.
        currentButton.GetComponent<PokemonChoiceButtonController>().SetPokemonSheet(starters[0]);
        currentButton.GetComponent<Image>().sprite = pokedex.pokedex[starters[0]].sprite;

        for (int i = 1; i < starters.Count; i++) //Pour tous les starters apr�s le premier.
        {
            /*Instantie chaque bouton en tant que fils du bouton pr�c�dent, pour qu'il soit bien positionn�
            (position relative par rapport � son parent). */ 
            GameObject nextButton = Instantiate(
                buttonPrefab,
                currentButton.transform //Position relative au parent.
            );
            nextButton.transform.SetParent(currentButton.transform, false); //Place l'objet comme enfant du pr�c�dent boutton.

            //S�lectionne la bonne fiche PokemonSheet pour chaque bouton et affiche le bon sprite.
            nextButton.GetComponent<PokemonChoiceButtonController>().SetPokemonSheet(starters[i]);
            nextButton.GetComponent<Image>().sprite = pokedex.pokedex[starters[i]].sprite;


            currentButton = nextButton; //Met-�-jour le current bouton pour pouvoir cr�e le suivant comme il faut.
        }
    }
}
