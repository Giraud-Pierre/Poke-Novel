using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Pokemon : ScriptableObject
{
    public string name;
    public int health;
    public int attack;
    public int defense;
    public int speed;
    public List<string> capacities;
}
