using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowPokemon : MonoBehaviour
{
    //Script qui sera attach� aux boutons du menu Pokemon pour afficher le pokemon correspondant.


    [SerializeField] private GameObject SelectedPokemonBox = default; //R�cup�re le prochain menu � afficher.

    private Pokemon pokemon; //Va recueillir le pokemon � afficher.

    public void SetPokemon(Pokemon newPokemon) //Attribue le pokemon � afficher � la variable pokemon.
    {
        pokemon = newPokemon;
    }

    public void ShowSelectedPokemon() //Affiche le pokemon souhait�.
    {
        GameObject canvas = GameObject.Find("Canvas"); //R�cup�re le canvas.

        //Affiche correctement la boite dupokmeon.
        GameObject newBox = Instantiate(
            SelectedPokemonBox,
            canvas.transform //En position relative par rapport au canvas.
        );

        newBox.transform.SetParent(canvas.transform, false); //La d�clare en temps qu'enfant du canvas.

        newBox.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = pokemon.GetSprite(); //Met le sprite du pokmon.

        //Place les attaques dans le texte � cet effet .
        newBox.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = string.Format(
                                                                                                    "<align=center>Capacit�s</align>\r\n{0}\r\n{1}\r\n{2}\r\n{3}",
                                                                                                    pokemon.GetCapacityName(0),
                                                                                                    pokemon.GetCapacityName(1),
                                                                                                    pokemon.GetCapacityName(2),
                                                                                                    pokemon.GetCapacityName(3)
                                                                                                );
        //Place les stats dans le dialogue;
        List<int> stats = pokemon.GetStats();
        newBox.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = string.Format(
                                                                               "<align=center>Stats</align>\r\nVie max {0}\r\nVie {1}\r\nAttaque {2}\r\nDefense {3}\r\nVitesse {4}",
                                                                               stats[0],
                                                                               pokemon.GetHealth(),
                                                                               stats[1],
                                                                               stats[2],
                                                                               stats[3]
                                                                                                );

        Destroy(gameObject.transform.parent.gameObject); //D�truit la boite de choix du pokemon (boite pr�c�dente).
    }
}
