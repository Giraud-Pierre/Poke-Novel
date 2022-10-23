using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PokemonMenuController : MonoBehaviour
{
    [SerializeField] GameObject noPokemonBox;

    private GameObject canvas;
    private Player player;

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        player = canvas.GetComponent<Player>();

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
        else
        {
            int buttonIndex = 0;
            do
            {
                GameObject currentButton = gameObject.transform.GetChild(buttonIndex).gameObject;
                currentButton.GetComponent<Image>().sprite = player.GetPokemon(buttonIndex).GetSprite();
                currentButton.GetComponent<ShowPokemon>().SetPokemon(player.GetPokemon(buttonIndex));
                buttonIndex++;
            } while (player.GetPokemon(buttonIndex) != null);
            for(; buttonIndex < 6; buttonIndex++) { Destroy(gameObject.transform.GetChild(buttonIndex).gameObject); }
        }

    }
}
