using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Pokedex : ScriptableObject
{
    public List<PokemonSheet> pokedex = new List<PokemonSheet>();
}
