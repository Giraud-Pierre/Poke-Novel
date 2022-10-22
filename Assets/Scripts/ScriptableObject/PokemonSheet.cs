using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PokemonSheet : ScriptableObject
{
    //nom de base en français du pokemon en remplaçant la donnée "name" de base hérité du ScriptableObject (new)
    public new string name;
    
    public List<int> baseStats = new List<int> { 0, 0, 0, 0 }; //stats de base du pokemon
    public List<int> baseCapacities = new List<int>(); //attaques de bases du pokemon
}
