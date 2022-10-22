using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private List<Pokemon> pokemons = new List<Pokemon>(); //liste des pokemons du joueurs
    private string location = "Chambre"; //position actuelle du joueur

    /*liste tous les dialogues d�j� d�clench�s(
            .. dans l'ordre, avec la maman du joueur, avec la maman du rival avant le lab, 
                maman du rival apr�s le lab et avec le professeur;
            .. 0 pour non et 1 pour oui)*/
    private List<int> dialoguesAlreadyHad = new List<int> { 0, 0, 0, 0, };

    public void SetLocation(string newLocation) //change la position du joueur
    {
        location = newLocation;
    }

    public string GetLocation() //Donne la position du joueur
    {
        return location;
    }

    public void AddPokemon(Pokemon newPokemon) //Ajoute un pokemon au joueur
    {
        if(pokemons.Count < 6)
        {
            pokemons.Add(newPokemon);
        }
    }
    
    public Pokemon GetPokemon(int index) //Donne un pokemon du joueur
    {
        if (pokemons.Count != 0) //v�rifie que le joueur a au moins un pokemon
        {
            return pokemons[index];
        }
        else
        {
            return null;
        }
    }

    public void SetDialogueRead(int index) //D�clare un dialogue comme ayant �t� lu
    {
        dialoguesAlreadyHad[index] = 1;
    }

    public int CheckIfDialogueRead(int index) //Regarde si un dialogue a d�j� �t� lu
    {
        return dialoguesAlreadyHad[index];
    }
}
