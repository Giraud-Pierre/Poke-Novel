using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MovementTable : ScriptableObject
{
    //Tables des directions possibles en fonction de la position actuelle.

    public List<string> Bedroom = new List<string> {"Salon" };
    public List<string> Livingroom = new List<string> { "Chambre", "Village" };
    public List<string> Village = new List<string> { "Salon", "Route 101", "Maison du rival", "Laboratoire" };
    public List<string> RivalHouse = new List<string> { "Village" };
    public List<string> Laboratory = new List<string> { "Village" };
}

