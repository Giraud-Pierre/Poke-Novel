using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PokemonChoiceButtonController : MonoBehaviour
{
    [SerializeField] private GameObject choosePokemonBox = default;
    [SerializeField] private GameObject dialogueBox = default;

    [SerializeField] private Dialogues dialogues = default;
    [SerializeField] private Pokedex pokedex = default;
    [SerializeField] private CapacityList capacityList = default;

    [SerializeField] private Sprite playerSprite = default;
    [SerializeField] private Sprite rivalSprite = default;

    private int pokemonIndex;
    private GameObject canvas;

    public void SetPokemonSheet(int newPokemonIndex)
    {
        pokemonIndex = newPokemonIndex;
    }

    private void ChangeToDialogue(List<string> dialogueText, Sprite char1, Sprite char2, int laboratory)
    {
        GameObject newDialogue = Instantiate(
            dialogueBox,
            canvas.transform
        );
        newDialogue.transform.SetParent(canvas.transform, false);

        newDialogue.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = char1;
        newDialogue.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = char2;
        newDialogue.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = dialogueText[0];
        dialogueText.RemoveAt(0);
        newDialogue.transform.GetChild(2).gameObject.GetComponent<ContinueDialogue>().SetButton(dialogueText, laboratory);
    }

    public void ChoosePokemon()
    {
        canvas = GameObject.Find("Canvas");


        List<string> rivalDialogue = new List<string>(dialogues.rivalBattle);
        ChangeToDialogue(rivalDialogue, playerSprite, rivalSprite, 2);

        Pokemon starter = canvas.AddComponent<Pokemon>();
        starter.SetPokemon(pokemonIndex, pokedex, capacityList);
        canvas.GetComponent<Player>().AddPokemon(starter);

        Destroy(choosePokemonBox);
    }
}
