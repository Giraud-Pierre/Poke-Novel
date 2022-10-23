using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon : MonoBehaviour
{
    //Classe sp�ciale pour g�rer les pokemons.


    //nom du pokemon en rempla�ant la donn�e "name" de base h�rit� du MonoBehaviour (new)
    private new string name;
    private int level;    //Niveau du pokemon.
    private int index;    //Indice du pokemon dans le pokemon.
    private Sprite pokemonSprite; //Sprite du pokemon.

    private int health; //vie du pokemon � l'instant t
    private int maxHealth; //vie maximale du pokemon

    //statistiques du pokemon.
    private int attack;
    private int defense;
    private int speed;
    List<float> modifiers = new List<float>(); //Modificateur temporaire qui seront modif� pendant un combat.

    private List<CapacitySheet> capacities = new List<CapacitySheet>(); //liste des attaques du pokemon.

    /*Cr�e le pokemon � partir de son num�ro, du pokedex et de la liste des attaques.
    
    Une sorte de pseudo-constructeur que l'on doit appeler apr�s de la g�n�ration du pokemon
    car les classes h�ritant de MonoBehaviour ne supporte pas bien les constructeurs. 

    Ce script n'�tant sur aucun GameObject, il ne peut pas prendre de SerializedField et
    pokedex et capacityList �tant des assets, ils ne peuvent pas �tre trouv� facilement
    avec un GameObject.Find par exemple. On les place donc en arguments dans la fonction, car
    le code qui cr�e le pokemon, qui lui doit �tre sur un GameObject, y a facilement acc�s. */
    public void SetPokemon(int pokemonIndex, Pokedex pokedex, CapacityList capacityList) 
    {

        PokemonSheet sheet = pokedex.pokedex[pokemonIndex]; //va chercher la fiche du pokemon

        index = pokemonIndex;   //Remplit les valeurs de base.
        pokemonSprite = sheet.sprite;
        name = sheet.name;
        maxHealth = sheet.baseStats[0];
        attack = sheet.baseStats[1];
        defense = sheet.baseStats[2];
        speed = sheet.baseStats[3];

        level = 5; //Les pokemon sont initialis� au niveau 5 pour l'instant, pas d'exp�rience ou de mont� en niveau possible

        health = maxHealth; //on d�marre avec tous les points de vies

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

    public Sprite GetSprite() //Donne le sprite du pokemon.
    {
        return pokemonSprite;
    }

    public int GetLevel() //Donne le niveau du pokemon
    {
        return level;
    }

    public List<int> GetStats() //Donne les stats du pokemon.
    {
        return new List<int> { maxHealth,attack,defense,speed};
    }

    public void InitializeModifiers() //Initialise les modificateurs au d�but du combat.
    {
        modifiers = new List<float> { 1, 1, 1 };
    }

    public void SetModifier(float newModifier, int statsIndex) //modifie un modificateur.
    {
        modifiers[statsIndex] = newModifier;
    }

    public List<float> GetModifiers() //R�cup�re tous les modificateurs
    {
        return modifiers;
    }

    public void SetHealth(int newHealth) //permet de changer la vie du pokemon.
    {
        //V�rifie que la vie que l'on essaye de mettre rentre dans les crit�re.
        if (newHealth <= maxHealth && newHealth >= 0) 
        {
            health = newHealth;
        }
        //Sinon on la fixe au maximum si on voulait la mettre � un nombre sup�rieur
        else if(newHealth > maxHealth)
        {
            health = maxHealth;
        }
        //ou � 0 si on voulait la fixer � un nombre n�gatif.
        else if(newHealth < 0)
        {
            health = 0;
        }
    }

    public int GetHealth() //Permet d'obtenir la vie du pokemon.
    {
        return health;
    }

    public void AddCapacity(CapacitySheet newCapacity) //Ajoute une capacit�.
    {
        capacities.Add(newCapacity);
    }

    public void DeleteCapacity(int index) //enl�ve une capacit� du pokemon
    {
        capacities.RemoveAt(index);
    }

    /*M�thodes pour obtenir les caract�ristiques des attaques, dans l'ordre:
    le nom, le type d'attaque, la puissance de l'attaque et sa pr�cision.*/
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
