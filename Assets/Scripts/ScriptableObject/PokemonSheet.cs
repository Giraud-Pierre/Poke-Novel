using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PokemonSheet : ScriptableObject
{
    //Fiche modèle pour chaque fiche de pokemon.

    //Nom de base en français du pokemon en remplaçant la donnée "name" de base hérité du ScriptableObject (new).
    public new string name;

    //Stats de base du pokemon, dans l'ordre MaxHealth, Attack, Defense, Speed.
    public List<int> baseStats = new List<int> { 0, 0, 0, 0 }; 

    public List<int> baseCapacities = new List<int>(); //Attaques de bases du pokemon.

    public Sprite sprite; //Sprite du pokemon.
}
