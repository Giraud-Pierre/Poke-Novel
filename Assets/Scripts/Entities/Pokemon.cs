using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon : MonoBehaviour
{
    [SerializeField] Pokedex pokedex;
    [SerializeField] CapacityList capacityList;

    //nom du pokemon en rempla�ant la donn�e "name" de base h�rit� du MonoBehaviour (new)
    private new string name; 
    private int level;    //niveau du pokemon
    private int health; //vie du pokemon � l'instant t
    private int maxHealth; //vie maximale du pokemon
    private int attack;
    private int defense;
    private int speed;
    private List<CapacitySheet> capacities; //liste des attaques du pokemon

    public Pokemon(int pokemonIndex) //constructeur du pokemon
    {
        PokemonSheet sheet = pokedex.pokedex[pokemonIndex]; //va chercher la fiche du pokemon

        name = sheet.name;
        maxHealth = sheet.baseStats[0];
        attack = sheet.baseStats[1];
        defense = sheet.baseStats[2];
        speed = sheet.baseStats[3];

        level = 5; //Les pokemon sont initialis� au niveau 5 pour l'instant, pas d'exp�rience ou de mont� en niveau possible

        health = maxHealth; //on d�marre avec tous les points de vies

        //assigne toutes les attaques de bases du pokemon � la fiche correspondante
        foreach (int capacity in sheet.baseCapacities) 
        {
            AddCapacity(capacityList.capacityList[capacity]);
        }
    }

    public void Rename(string newName) //renommer le pokemon
    {
        name = newName;
    }

    public string GetName() //Donne le nom du pokemon
    {
        return name;
    }

    public int GetLevel() //Donne le niveau du pokemon
    {
        return level;
    }

    public List<int> GetStats() //Donne les stats du pokemon
    {
        return new List<int> { maxHealth,attack,defense,speed};
    }

    public void SetHealt(int newHealth) //permet de changer la vie du pokemon
    {
        //v�rifie que la vie que l'on essaye de mettre rentre dans les crit�re
        if (newHealth <= maxHealth && newHealth >= 0) 
        {
            health = newHealth;
        }
    }

    public int GetHealth() //permet d'obtenir la vie du pokemon
    {
        return health;
    }

    public void AddCapacity(CapacitySheet newCapacity) //Ajoute une capcit�
    {
        capacities.Add(newCapacity);
    }

    public void DeleteCapacity(int index) //enl�ve une capacit� du pokemon
    {
        capacities.RemoveAt(index);
    }

    //M�thodes pour obtenir les caract�ristiques des attaques, dans l'ordre:
    //le nom, le type d'attaque, la puissance de l'attaque et sa pr�cision
    public string GetCapacityName(int index) 
    {
        return capacities[index].name;
    }

    public int GetCapacityType(int index)
    {
        return capacities[index].type;
    }

    public int GetCapacityPower(int index)
    {
        return capacities[index].power;
    }
    public int GetCapacityPrecision(int index)
    {
        return capacities[index].precision;
    }
}
