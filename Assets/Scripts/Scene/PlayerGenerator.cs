using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{
    public Player red; //cr�e le joueur qui sera accessible depuis des programmes ext�rieurs

    // Start is called before the first frame update
    void Start()
    {
        red = gameObject.AddComponent<Player>();
    }
}
