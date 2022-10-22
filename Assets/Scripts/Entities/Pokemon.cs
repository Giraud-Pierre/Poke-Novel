using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon : MonoBehaviour
{
    [SerializeField] Pokedex pokedex;
    [SerializeField] CapacityList capacityList;

    //nom du pokemon en remplaçant la donnée "name" de base hérité du MonoBehaviour (new)
    private new string name; 
    private int level;    //niveau du pokemon
    private int health; //vie du pokemon à l'instant t
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

        level = 5; //Les pokemon sont initialisé au niveau 5 pour l'instant, pas d'expérience ou de monté en niveau possible

        health = maxHealth; //on démarre avec tous les points de vies

        //assigne toutes les attaques de bases du pokemon à la fiche correspondante
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
        //vérifie que la vie que l'on essaye de mettre rentre dans les critère
        if (newHealth <= maxHealth && newHealth >= 0) 
        {
            health = newHealth;
        }
    }

    public int GetHealth() //permet d'obtenir la vie du pokemon
    {
        return health;
    }

    public void AddCapacity(CapacitySheet newCapacity) //Ajoute une capcité
    {
        capacities.Add(newCapacity);
    }

    public void DeleteCapacity(int index) //enlève une capacité du pokemon
    {
        capacities.RemoveAt(index);
    }

    //Méthodes pour obtenir les caractéristiques des attaques, dans l'ordre:
    //le nom, le type d'attaque, la puissance de l'attaque et sa précision
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
