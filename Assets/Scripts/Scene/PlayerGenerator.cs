using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{
    //Fixé sur le canvas et génère le joueur au lancement du jeu.


    public Player red; //crée le joueur en public, qui sera donc accessible par les autres scripts.

    void Start()
    {
        //Génère le joueur en le fixant sur le canvas, qui est toujours présent du début à la fin du jeu.
        red = gameObject.AddComponent<Player>(); 
    }
}
