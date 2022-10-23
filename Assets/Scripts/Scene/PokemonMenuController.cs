using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PokemonMenuController : MonoBehaviour
{
    /*Script qui sera fixé sur la boite de choix du pokemon à afficher et qui va paramétrer 
    les boutons pour afficher les pokemons. */


    [SerializeField] GameObject noPokemonBox; //Récupère la boite qui indique que le joueur ne possède pas de pokemon.

    private GameObject canvas; //Récupère le canvas.
    private Player player; //Récupère le joueur.

    void Start()
    {
        canvas = GameObject.Find("Canvas"); //On récupère le canvas.
        player = canvas.GetComponent<Player>(); //On récupère le joueur.

        if (player.GetPokemon(0) == null) //Si le joueur ne possède pas de pokemon.
        {
            //Efface tous les boutons.
            for (int buttonIndex = 0; buttonIndex < 6; buttonIndex++) { Destroy(gameObject.transform.GetChild(buttonIndex).gameObject); }

            //Affiche correctement la boite qui dit que le joueur n'a pas de pokemon.
            GameObject noPokemon = Instantiate(
                noPokemonBox,
                gameObject.transform //En position relative par rapport à la PokemonBox.
            );

            noPokemon.transform.SetParent(gameObject.transform, false); //La déclare en temps qu'enfant de la PokemonBox.
        }

        else //sinon;
        {
            int buttonIndex = 0;
            do //Paramètre autant de bouton que le joueur a de pokemon (maximum 6) .
            {
                //Récupère le bouton de numéro "Bouton index".
                GameObject currentButton = gameObject.transform.GetChild(buttonIndex).gameObject;

                //Place le sprite du pokemon "index"ième du joueur sur le bouton.
                currentButton.GetComponent<Image>().sprite = player.GetPokemon(buttonIndex).GetSprite();

                //Place le bon pokemon dans le code du bouton.
                currentButton.GetComponent<ShowPokemon>().SetPokemon(player.GetPokemon(buttonIndex));

                buttonIndex++; //Passe au bouton suivant
            } while (player.GetPokemon(buttonIndex) != null); //Tant que le dresseur a des pokemon.

            //Supprime les boutons restant qui n'ont pas été paramétrés.
            for(; buttonIndex < 6; buttonIndex++) { Destroy(gameObject.transform.GetChild(buttonIndex).gameObject); }
        }

    }
}
