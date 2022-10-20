using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Pokedex : ScriptableObject
{
    public List<int> Charmander = new List<int> {19,11,9,11 };
    public List<string> CharmanderBaseCapcities = new List<string> { "Scratch","Growl"};

    public List<int> Squirtle = new List<int> { 19, 9, 11, 9 };
    public List<string> SquirtleBaseCapcities = new List<string> { "Tackle", "TailWhip" };

    public List<int> Bulbazar = new List<int> { 19, 9, 9, 9 };
    public List<string> BulbazarBaseCapcities = new List<string> { "Tackle", "Growl" };
}
