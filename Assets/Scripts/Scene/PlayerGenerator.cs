using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{
    //Fix� sur le canvas et g�n�re le joueur au lancement du jeu.


    public Player red; //cr�e le joueur en public, qui sera donc accessible par les autres scripts.

    void Start()
    {
        //G�n�re le joueur en le fixant sur le canvas, qui est toujours pr�sent du d�but � la fin du jeu.
        red = gameObject.AddComponent<Player>(); 
    }
}
