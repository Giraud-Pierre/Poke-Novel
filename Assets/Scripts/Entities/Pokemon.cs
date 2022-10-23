using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon : MonoBehaviour
{
    //Classe spéciale pour g�rer les pokemons.


    //nom du pokemon en remplaçant la donnée "name" de base hérité du MonoBehaviour (new).
    private new string name;

    private int level;    //Niveau du pokemon.
    private int index;    //Indice du pokemon dans le pokemon.
    private Sprite pokemonSprite; //Sprite du pokemon.

    private int health; //vie du pokemon à l'instant t.
    private int maxHealth; //vie maximale du pokemon.

    //statistiques de base du pokemon.
    private int attack;
    private int defense;
    private int speed;
    List<float> modifiers = new List<float>(); //Modificateurs temporaires qui seront modifiés pendant un combat.

    private List<CapacitySheet> capacities = new List<CapacitySheet>(); //liste des attaques du pokemon.

    public void SetPokemon(int pokemonIndex, Pokedex pokedex, CapacityList capacityList) 
    {
        /*Crée le pokemon à partir de son numéro, du pokedex et de la liste des attaques.
    
        Une sorte de pseudo-constructeur que l'on doit appeler après de la génération du pokemon
        car les classes héritant de MonoBehaviour ne supporte pas bien les constructeurs. 

        Ce script n'étant sur aucun GameObject, il ne peut pas prendre de SerializedField et
        pokedex et capacityList étant des assets, ils ne peuvent pas être trouvé facilement
        avec un GameObject.Find par exemple. On les place donc en arguments dans la fonction, car
        le code qui crée le pokemon, qui lui doit être sur un GameObject, y a facilement accès. */


        PokemonSheet sheet = pokedex.pokedex[pokemonIndex]; //va chercher la fiche du pokemon.

        index = pokemonIndex;   //Remplit les valeurs de base.
        pokemonSprite = sheet.sprite;
        name = sheet.name;
        maxHealth = sheet.baseStats[0];
        attack = sheet.baseStats[1];
        defense = sheet.baseStats[2];
        speed = sheet.baseStats[3];

        //Les pokemon sont initialisé et reste au niveau 5 pour l'instant, pas d'expérience ou de montée en niveau possible.
        level = 5; 

        health = maxHealth; //on démarre avec tous les points de vies.

        //assigne toutes les attaques de bases du pokemon aux fiches CapacitySheets correspondantes.
        foreach (int capacity in sheet.baseCapacities) 
        {
            AddCapacity(capacityList.capacityList[capacity]);
        }
    }

    public int GetIndex() //Renvoi l'indice du pokemon dans le pokedex.
    {
        return index;
    }
    public void Rename(string newName) //renommer le pokemon.
    {
        name = newName;
    }

    public string GetName() //Donne le nom du pokemon.
    {
        return name;
    }

    public Sprite GetSprite() //Donne le sprite du pokemon.
    {
        return pokemonSprite;
    }

    public int GetLevel() //Donne le niveau du pokemon.
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

    public List<float> GetModifiers() //Récupère tous les modificateurs.
    {
        return modifiers;
    }

    public void SetHealth(int newHealth) //permet de changer la vie du pokemon.
    {
        //Vérifie que la vie ne soit pas supérieure à la valeur max ou négative.
        if (newHealth <= maxHealth && newHealth >= 0) 
        {
            health = newHealth;
        }
        //Sinon on la fixe au maximum si on voulait la mettre à un nombre supérieur à la vie max;
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

    public void DeleteCapacity(int index) //enlève une capacité du pokemon.
    {
        capacities.RemoveAt(index);
    }

    public int GetNumberCapacities() //Donne le nombre de capacité du pokemon.
    {
        return capacities.Count;
    }
    public string GetCapacityName(int index) //Donne le nom de la capacité numéro "index" du pokemon.
    {
        //Vérifie que le pokemon possède une capacité à cet index.
        if (index < capacities.Count) { return capacities[index].name; } 
        else { return ""; } //Sinon renvoie un string vide.
    }

    public int GetCapacityType(int index) //Donne le type de la capacité numéro "index" du pokemon.
    {
        //Vérifie que le pokemon possède une capacité à cet index.
        if (index < capacities.Count) { return capacities[index].type; }
        else { return -1; } //Sinon renvoie -1.
    }

    public int GetCapacityPower(int index) //Donne la puissance de la capacité numéro "index" du pokemon.
    {
        //Vérifie que le pokemon possède une capacité à cet index
        if (index < capacities.Count) { return capacities[index].power; }
        else { return -1; }//Sinon renvoie -1.
    }
    public int GetCapacityPrecision(int index) //Donne la précision de la capacité numéro "index" du pokemon.
    {
        //Vérifie que le pokemon possède une capacité à cet index
        if (index < capacities.Count) { return capacities[index].precision; }
        else { return -1; }//Sinon renvoie -1.
    }
}
