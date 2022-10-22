using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{
    public Player red; //crée le joueur qui sera accessible depuis des programmes extérieurs

    // Start is called before the first frame update
    void Start()
    {
        red = gameObject.AddComponent<Player>();
    }
}
