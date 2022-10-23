using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PokemonMenuController : MonoBehaviour
{
    /*Script qui sera fix� sur la boite de choix du pokemon � afficher et qui va param�trer 
    les boutons pour afficher les pokemons. */


    [SerializeField] GameObject noPokemonBox; //R�cup�re la boite qui indique que le joueur ne poss�de pas de pokemon.

    private GameObject canvas; //R�cup�re le canvas.
    private Player player; //R�cup�re le joueur.

    void Start()
    {
        canvas = GameObject.Find("Canvas"); //On r�cup�re le canvas.
        player = canvas.GetComponent<Player>(); //On r�cup�re le joueur.

        if (player.GetPokemon(0) == null) //Si le joueur ne poss�de pas de pokemon.
        {
            //Efface tous les boutons.
            for (int buttonIndex = 0; buttonIndex < 6; buttonIndex++) { Destroy(gameObject.transform.GetChild(buttonIndex).gameObject); }

            //Affiche correctement la boite qui dit que le joueur n'a pas de pokemon.
            GameObject noPokemon = Instantiate(
                noPokemonBox,
                gameObject.transform //En position relative par rapport � la PokemonBox.
            );

            noPokemon.transform.SetParent(gameObject.transform, false); //La d�clare en temps qu'enfant de la PokemonBox.
        }

        else //sinon;
        {
            int buttonIndex = 0;
            do //Param�tre autant de bouton que le joueur a de pokemon (maximum 6) .
            {
                //R�cup�re le bouton de num�ro "Bouton index".
                GameObject currentButton = gameObject.transform.GetChild(buttonIndex).gameObject;

                //Place le sprite du pokemon "index"i�me du joueur sur le bouton.
                currentButton.GetComponent<Image>().sprite = player.GetPokemon(buttonIndex).GetSprite();

                //Place le bon pokemon dans le code du bouton.
                currentButton.GetComponent<ShowPokemon>().SetPokemon(player.GetPokemon(buttonIndex));

                buttonIndex++; //Passe au bouton suivant
            } while (player.GetPokemon(buttonIndex) != null); //Tant que le dresseur a des pokemon.

            //Supprime les boutons restant qui n'ont pas �t� param�tr�s.
            for(; buttonIndex < 6; buttonIndex++) { Destroy(gameObject.transform.GetChild(buttonIndex).gameObject); }
        }

    }
}
