using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PokemonSheet : ScriptableObject
{
    //nom de base en fran�ais du pokemon en rempla�ant la donn�e "name" de base h�rit� du ScriptableObject (new)
    public new string name;
    
    public List<int> baseStats = new List<int> { 0, 0, 0, 0 }; //stats de base du pokemon
    public List<int> baseCapacities = new List<int>(); //attaques de bases du pokemon
}
