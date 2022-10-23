using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon : MonoBehaviour
{
    //Classe spéciale pour gérer les pokemons.


    //nom du pokemon en remplaçant la donnée "name" de base hérité du MonoBehaviour (new)
    private new string name; 
    private int level;    //Niveau du pokemon.
    private int index;    //Indice du pokemon dans le pokemon.

    private int health; //Vie du pokemon à l'instant t.
    private int maxHealth; //Vie maximale du pokemon.

    //statistiques du pokemon.
    private int attack;
    private int defense;
    private int speed;
    List<float> modifiers = new List<float>(); //Modificateur temporaire qui seront modifé pendant un combat.

    private List<CapacitySheet> capacities = new List<CapacitySheet>(); //liste des attaques du pokemon.

    /*Crée le pokemon à partir de son numéro, du pokedex et de la liste des attaques.
    
    Une sorte de pseudo-constructeur que l'on doit appeler après de la génération du pokemon
    car les classes héritant de MonoBehaviour ne supporte pas bien les constructeurs. 

    Ce script n'étant sur aucun GameObject, il ne peut pas prendre de SerializedField et
    pokedex et capacityList étant des assets, ils ne peuvent pas être trouvé facilement
    avec un GameObject.Find par exemple. On les place donc en arguments dans la fonction, car
    le code qui crée le pokemon, qui lui doit être sur un GameObject, y a facilement accès. */
    public void SetPokemon(int pokemonIndex, Pokedex pokedex, CapacityList capacityList) 
    {

        PokemonSheet sheet = pokedex.pokedex[pokemonIndex]; //va chercher la fiche du pokemon

        index = pokemonIndex;   //Remplit les valeurs de base.
        name = sheet.name;
        maxHealth = sheet.baseStats[0];
        attack = sheet.baseStats[1];
        defense = sheet.baseStats[2];
        speed = sheet.baseStats[3];

        level = 5; //Les pokemon sont initialisé au niveau 5 pour l'instant, pas d'expérience ou de monté en niveau possible

        health = maxHealth; //on démarre avec tous les points de vies

        //assigne toutes les attaques de bases du pokemon aux fiches CapacitySheets correspondantes
        foreach (int capacity in sheet.baseCapacities) 
        {
            AddCapacity(capacityList.capacityList[capacity]);
        }
    }

    public int GetIndex() //Renvoi l'indice du pokemon dans le pokedex.
    {
        return index;
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

    public List<int> GetStats() //Donne les stats du pokemon.
    {
        return new List<int> { maxHealth,attack,defense,speed};
    }

    public void InitializeModifiers() //Initialise les modificateurs au début du combat.
    {
        modifiers = new List<float> { 1, 1, 1 };
    }

    public void SetModifier(float newModifier, int statsIndex) //modifie un modificateur.
    {
        modifiers[statsIndex] = newModifier;
    }

    public List<float> GetModiiers() //Récupère tous les modificateurs
    {
        return modifiers;
    }

    public void SetHealth(int newHealth) //permet de changer la vie du pokemon.
    {
        //Vérifie que la vie que l'on essaye de mettre rentre dans les critère.
        if (newHealth <= maxHealth && newHealth >= 0) 
        {
            health = newHealth;
        }
        //Sinon on la fixe au maximum si on voulait la mettre à un nombre supérieur
        else if(newHealth > maxHealth)
        {
            health = maxHealth;
        }
        //ou à 0 si on voulait la fixer à un nombre négatif.
        else if(newHealth < 0)
        {
            health = 0;
        }
    }

    public int GetHealth() //Permet d'obtenir la vie du pokemon.
    {
        return health;
    }

    public void AddCapacity(CapacitySheet newCapacity) //Ajoute une capacité.
    {
        capacities.Add(newCapacity);
    }

    public void DeleteCapacity(int index) //enlève une capacité du pokemon
    {
        capacities.RemoveAt(index);
    }

    /*Méthodes pour obtenir les caractéristiques des attaques, dans l'ordre:
    le nom, le type d'attaque, la puissance de l'attaque et sa précision.*/
    public int GetNumberCapacities()
    {
        return capacities.Count;
    }
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
