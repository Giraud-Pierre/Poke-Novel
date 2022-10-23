using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Pokedex : ScriptableObject
{
    //Liste de toutes les fiches de pokemon.

    public List<PokemonSheet> pokedex = new List<PokemonSheet>();
}
