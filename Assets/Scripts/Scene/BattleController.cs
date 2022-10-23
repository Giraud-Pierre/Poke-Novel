using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleController : MonoBehaviour
{
    enum WhoDied {nobody, player, opponent}

    [SerializeField] private GameObject battleAttackBox  = default;
    [SerializeField] private GameObject battleBagBox = default;
    [SerializeField] private GameObject battleMenuBox = default;
    [SerializeField] private GameObject battlePokemonBox = default;
    [SerializeField] private GameObject fleeBox = default ;
    [SerializeField] private GameObject dialogueBoxPrefab  = default;
    [SerializeField] private Dialogues dialogues = default; //Liste de tous les dialogues du jeu.
    [SerializeField] private Sprite playerSprite = default;
    [SerializeField] private Sprite rivalSprite = default;
    [SerializeField] private Pokedex pokedex = default;
    [SerializeField] private CapacityList capacityList = default;

    private Pokemon playerPokemon;
    private Pokemon opponentPokemon;
    private WhoDied whoDied;


    public void StartBattle()
    {
        whoDied = WhoDied.nobody;

        playerPokemon = gameObject.GetComponent<Player>().GetPokemon(0);

        if (playerPokemon == null) Debug.Log("Tu n’a pas de pokemon");
        else
        {
            opponentPokemon = InstantiateOpponentPokemon();

            playerPokemon.InitializeModifiers();
            opponentPokemon.InitializeModifiers();

            Menu();
        }
    }

    public void Menu()
    {
        switch (whoDied)
        {
            case WhoDied.nobody :
                GameObject dialogueBox = ChangeUI(battleMenuBox);
                dialogueBox.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = playerPokemon.GetSprite();
                dialogueBox.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = opponentPokemon.GetSprite();
                break;
            case WhoDied.player :
                ChangeToDialogue(dialogues.rivalDefeat);
                break;
            case WhoDied.opponent :
                ChangeToDialogue(dialogues.rivalVictory);
                break;
        }
    }

    public void ShowAttack()
    {
        GameObject dialogueBox = ChangeUI(battleAttackBox);
        dialogueBox.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = playerPokemon.GetSprite();
        dialogueBox.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = opponentPokemon.GetSprite();
    }

    public void UseAttack(int index)
    {
        List<string> dialogueText = new List<string>();
        string message = "";

        int precision = playerPokemon.GetCapacityPrecision(index);
        if (precision < Mathf.RoundToInt(Random.value * 100))
        {
            dialogueText.Add(
                string.Format("{0} rate son attaque", playerPokemon.GetName())
            );
        }
        else {
            message = DoCapacity(playerPokemon, opponentPokemon, index);
            dialogueText.Add(message);
        }

        if (opponentPokemon.GetHealth() != 0)
        {
            index = Mathf.RoundToInt(Random.Range(0, opponentPokemon.GetNumberCapacities() - 1));

            precision = opponentPokemon.GetCapacityPrecision(index);
            if (precision < Mathf.RoundToInt(Random.value * 100))
            {
                dialogueText.Add(
                    string.Format("{0} rate son attaque", opponentPokemon.GetName())
                );
            }
            else {
                message = DoCapacity( opponentPokemon, playerPokemon, index);
                dialogueText.Add(message);

                if (playerPokemon.GetHealth() == 0)
                {
                    dialogueText.Add(
                        string.Format("{0} est KO ", playerPokemon.GetName())
                    );
                    whoDied = WhoDied.player;
                }
            }
        }
        else {
            dialogueText.Add(
                string.Format("{0} est KO ", opponentPokemon.GetName())
            );
            whoDied = WhoDied.opponent;
        }

        AttackDialogue(dialogueText);
    }

    public void Pokemon()
    {
        ChangeUI(battlePokemonBox);
    }

    public void Bag()
    {
        ChangeUI(battleBagBox);
    }

    public void Flee()
    {
        GameObject dialogueBox = ChangeUI(fleeBox);
        dialogueBox.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = playerPokemon.GetSprite();
        dialogueBox.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = opponentPokemon.GetSprite();
        dialogueBox.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = "Vous ne pouvez pas fuir d'un combat contre un dresseur !";
    }

    private Pokemon InstantiateOpponentPokemon()
    {
        Pokemon starter = gameObject.AddComponent<Pokemon>();

        int pokemonIndex = playerPokemon.GetIndex() + 1;
        if (pokemonIndex == 3) pokemonIndex = 0;

        starter.SetPokemon(pokemonIndex, pokedex, capacityList);

        return starter;
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

    private void AttackDialogue(List<string> dialogueText)
    {
        GameObject dialogueBox = ChangeUI(fleeBox);

        //Affiche les bons sprites et la premi�re partie du dialogue.
        dialogueBox.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = playerPokemon.GetSprite();
        dialogueBox.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = opponentPokemon.GetSprite();
        dialogueBox.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = dialogueText[0];

        //Retire la premi�re partie du dialogue qui a d�j� �t� affich� et g�n�re le bouton pour continuer le dialogue.
        dialogueText.RemoveAt(0);

        dialogueBox.transform.GetChild(2).gameObject.GetComponent<BattleButtonController>().SetDialogue(dialogueText);
    }

    private void ChangeToDialogue(List<string> dialogueText)
    {
        GameObject newDialogue = ChangeUI(dialogueBoxPrefab);

        //Affiche les bons sprites et la premi�re partie du dialogue.
        newDialogue.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = playerSprite;
        newDialogue.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = rivalSprite;
        newDialogue.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = dialogueText[0];

        //Retire la premi�re partie du dialogue qui a d�j� �t� affich� et g�n�re le bouton pour continuer le dialogue.
        dialogueText.RemoveAt(0);

        /*0 si le joueur n'est pas dans le laboratoire, si c'est le cas:
            - …
            - 3 si le combat contre le rival est fini. */
        newDialogue.transform.GetChild(2).gameObject.GetComponent<ContinueDialogue>().SetButton(dialogueText, 3);
    }

    private string GetStatsType(int index)
    {
        if (index == 0) return "L'attaque";
        else if (index == 1) return "La defense";
        else if (index == 2) return "La vitesse";
        else return "Erreur de stats";
    }

    private string DoCapacity(Pokemon initiator, Pokemon target, int indexCapacity)
    {
        if(initiator.GetCapacityType(indexCapacity) == 0)
        {
            float initiatorAttack = initiator.GetStats()[1] * initiator.GetModifiers()[0];
            int capacityPower = initiator.GetCapacityPower(indexCapacity);
            float targetDefence = target.GetStats()[2] * target.GetModifiers()[1];

            int damage = Mathf.RoundToInt(
                                            (float)initiatorAttack * (float)capacityPower * ((float)targetDefence 
                                            / 
                                            (1500f + (float)targetDefence))
                                         );

            target.SetHealth(target.GetHealth() - damage);

            return string.Format("{0} lance {1}. \n{2} subit {3} dégâts",
                                    initiator.GetName(),
                                    initiator.GetCapacityName(indexCapacity),
                                    target.GetName(),
                                    damage
                                );
        }
        else if (initiator.GetCapacityType(indexCapacity) == 1)
        {
            int power = initiator.GetCapacityPower(indexCapacity);
            if(power < 10)
            {
                initiator.SetModifier(initiator.GetModifiers()[power] + 0.25f * initiator.GetModifiers()[power], power);

                return string.Format("{0} lance {1}. \n{2} de {3} augmente",
                                    initiator.GetName(),
                                    initiator.GetCapacityName(indexCapacity),
                                    GetStatsType(power),
                                    initiator.GetName()
                                );
            }
            else
            {
                power = -10;
                initiator.SetModifier(initiator.GetModifiers()[power] + 0.50f * initiator.GetModifiers()[power], power);
                initiator.SetModifier(initiator.GetModifiers()[power] + 0.25f * initiator.GetModifiers()[power], power);

                return string.Format("{0} lance {1}. \n{2} de {3} augmente beaucoup",
                                    initiator.GetName(),
                                    initiator.GetCapacityName(indexCapacity),
                                    GetStatsType(power),
                                    initiator.GetName()
                                );
            }
        }
        else if (initiator.GetCapacityType(indexCapacity) == 2)
        {
            int power = initiator.GetCapacityPower(indexCapacity);
            if (power < 10)
            {
                target.SetModifier(target.GetModifiers()[power] - 0.15f * target.GetModifiers()[power], power);

                return string.Format("{0} lance {1}. \n{2} de {3} diminue",
                                    initiator.GetName(),
                                    initiator.GetCapacityName(indexCapacity),
                                    GetStatsType(power),
                                    target.GetName()
                                );
            }
            else
            {
                power = -10;
                target.SetModifier(target.GetModifiers()[power] - 0.30f * target.GetModifiers()[power], power);

                return string.Format("{0} lance {1}. \n{2} de {3} diminue beaucoup",
                                    initiator.GetName(),
                                    initiator.GetCapacityName(indexCapacity),
                                    GetStatsType(power),
                                    target.GetName()
                                );
            }
        }
        else
        {
            return "Erreur de type de capacité.";
        }
    }
}
