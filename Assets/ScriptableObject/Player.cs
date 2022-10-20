using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Player : ScriptableObject
{
    public List<string> pokemon;
    public string location = "Chambre";

    /*liste tous les dialogues déjà déclenchés(
            .. dans l'ordre, avec la maman du joueur, avec la maman du rival avant le lab, 
                maman du rival après le lab et avec le professeur;
            .. 0 pour non et 1 pour oui)*/
    public List<int> dialoguesAlreadyHad = new List<int> {0, 0, 0, 0,};
}
