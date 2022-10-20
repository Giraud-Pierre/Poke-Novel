using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Player : ScriptableObject
{
    public List<string> pokemon;
    public string Location;

    //liste tous les dialogues d�j� d�clench�s (ordre similaire � dialogue, 0 pour non et 1 pour oui)
    public List<int> dialoguesAlreadyHad = new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0};
}
