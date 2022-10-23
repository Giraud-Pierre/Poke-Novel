using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Classe spéciale pour le joueur et ses différentes composantes. 


    public List<Pokemon> pokemons = new List<Pokemon>(); //Liste des pokemons du joueurs.
    private string location = "Chambre"; //Position actuelle du joueur.

    /*Liste tous les dialogues déjà déclenchés(
            .. dans l'ordre, avec la maman du joueur, avec la maman du rival avant le lab, 
                maman du rival après le lab et avec le professeur;
            .. 0 pour non et 1 pour oui). */
    private List<int> dialoguesAlreadyHad = new List<int> { 0, 0, 0, 0, };

    public void SetLocation(string newLocation) //Change la position du joueur.
    {
        location = newLocation;
    }

    public string GetLocation() //Donne la position du joueur.
    {
        return location;
    }

    public void AddPokemon(Pokemon newPokemon) //Ajoute un pokemon au joueur.
    {
        if(pokemons.Count < 6)
        {
            pokemons.Add(newPokemon);
        }
    }
    
    public Pokemon GetPokemon(int index) //Donne un pokemon du joueur.
    {
        if (pokemons.Count != 0) //vérifie que le joueur a au moins un pokemon.
        {
            return pokemons[index];
        }
        else
        {
            return null;
        }
    }

    public void SetDialogueRead(int index) //Déclare un dialogue comme ayant été lu.
    {
        dialoguesAlreadyHad[index] = 1;
    }

    public int CheckIfDialogueRead(int index) //Regarde si un dialogue a déjà été lu.
    {
        return dialoguesAlreadyHad[index];
    }
}
