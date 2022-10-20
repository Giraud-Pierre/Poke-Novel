using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Pokemon : ScriptableObject
{
    public string pokemonName;
    public int health;
    public int attack;
    public int defense;
    public int speed;
    public List<string> capacities;
}
