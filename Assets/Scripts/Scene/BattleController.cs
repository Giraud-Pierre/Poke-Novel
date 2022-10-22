using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    [SerializeField] private GameObject BattleAttackBox;
    [SerializeField] private GameObject BattleBagBox;
    [SerializeField] private GameObject BattleMenuBox;
    [SerializeField] private GameObject BattlePokemonBox;
    [SerializeField] private GameObject FleeBox;

    // private void Awake()
    // {
    //     Debug.Log();
    // }

    public void ShowAttack()
    {
        ChangeUI(BattleMenuBox);
    }

    public void UseAttack(int index)
    {
        GameObject pokemon = gameObject.GetComponent<Player>().GetPokemon(0);

        if (pokemon == null) Debug.Log("Tu n’a pas de pokemon");
        else
        {
            int precision = pokemon.GetCapacityPrecision(index);
            if ( precision < Mathf.RoundToInt(Random.value * 100))
            {
                GameObject dialogueBox = ChangeUI(FleeBox);
                // message raté l’attaque
            }
            else
            {
                // Fais l’attaque
            }
        }
    }

    public void Pokemon()
    {
        ChangeUI(BattlePokemonBox);
    }

    public void Bag()
    {
        ChangeUI(BattleBagBox);
    }

    public void Flee()
    {
        GameObject dialogueBox = ChangeUI(FleeBox);
        // message
    }

    private GameObject ChangeUI(GameObject nextMenu)
    {
        GameObject newMenu = Instantiate(
            nextMenu,
            gameObject.transform
        );

        newMenu.transform.SetParent(gameObject.transform, false);

        Destroy(gameObject.transform.GetChild(2).gameObject);

        return newMenu;
    }
}
