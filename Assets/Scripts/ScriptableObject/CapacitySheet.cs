using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CapacitySheet : ScriptableObject
{
    //Fiche modèle pour les fiches d'attaque.

    //Nom en français de l'attaque en remplaçant la donnée "name" de base hérité du ScriptableObject (new).
    public new string name; 

    /*Définit le type d'attaque: 0 = attaque qui fait des dommages,
    1 = attaque qui augmente les stats du lanceur,
    2 = attaque qui diminue les stats de l'adversaire. */
    public int type = 0;

    /*Puissance de l'attaque:
    ..Multiplicateur de dommage si jamais c'est une attaque qui fait des dégâts
    ..Si c'est une attaque qui augmente ou diminue les stats : 
        - le chiffre des dizaines est soit 0 si cela 
            change un peu ou 1 si cela change beaucoup;
        - le chiffre des unités indique la stats concernées
            (0 attaque, 1 défense, 2 vitesse);
        - Exemple :01 = change un peu la défense. */
    public int power = 0; 

    public int precision = 100; //chance de toucher en %.
}
