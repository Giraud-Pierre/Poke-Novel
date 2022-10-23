using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PokemonChoiceBoxController : MonoBehaviour
{
    /*Script placé sur la boite de choix du premier pokemon et génère les boutons 
    pour choisir le-dit pokemon. */

    [SerializeField] private GameObject buttonPrefab = default; //Prefab du bouton de choix du pokemon.
    [SerializeField] private Pokedex pokedex = default; //Liste des pokemons.


    void Start()
    {
        //Génère les boutons avec les pokemons correspondants aux ID 0, 1 et 2.
        GenerateButton(new List<int> { 0, 1, 2}); 
    }

    private void GenerateButton(List<int> starters) //Genère les boutons.
    {
        //Récupère le premier bouton qui est déjà placé sur la boite de choix du premier pokemon.
        GameObject currentButton = gameObject.transform.GetChild(1).gameObject;

        //Sélectionne la bonne fiche PokemonSheet pour le premier bouton et affiche le bon sprite.
        currentButton.GetComponent<PokemonChoiceButtonController>().SetPokemonSheet(starters[0]);
        currentButton.GetComponent<Image>().sprite = pokedex.pokedex[starters[0]].sprite;

        for (int i = 1; i < starters.Count; i++) //Pour tous les starters après le premier.
        {
            /*Instantie chaque bouton en tant que fils du bouton précédent, pour qu'il soit bien positionné
            (position relative par rapport à son parent). */ 
            GameObject nextButton = Instantiate(
                buttonPrefab,
                currentButton.transform //Position relative au parent.
            );
            nextButton.transform.SetParent(currentButton.transform, false); //Place l'objet comme enfant du précédent boutton.

            //Sélectionne la bonne fiche PokemonSheet pour chaque bouton et affiche le bon sprite.
            nextButton.GetComponent<PokemonChoiceButtonController>().SetPokemonSheet(starters[i]);
            nextButton.GetComponent<Image>().sprite = pokedex.pokedex[starters[i]].sprite;


            currentButton = nextButton; //Met-à-jour le current bouton pour pouvoir crée le suivant comme il faut.
        }
    }
}
