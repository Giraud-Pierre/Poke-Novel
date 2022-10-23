using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CapacityList : ScriptableObject
{
    //Liste de toutes les fiches d'attaques.
    public List<CapacitySheet> capacityList = new List<CapacitySheet>();
}
