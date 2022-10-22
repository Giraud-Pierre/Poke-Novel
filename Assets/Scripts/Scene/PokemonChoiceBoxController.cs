using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PokemonChoiceBoxController : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab = default;
    [SerializeField] private Pokedex pokedex = default;


    // Start is called before the first frame update
    void Start()
    {
        GenerateButton(new List<int> { 0, 1, 2});
    }

    private void GenerateButton(List<int> starters)
    {
        GameObject currentButton = gameObject.transform.GetChild(1).gameObject;

        currentButton.GetComponent<PokemonChoiceButtonController>().SetPokemonSheet(0);
        currentButton.GetComponent<Image>().sprite = pokedex.pokedex[0].sprite;

        for (int i = 1; i < starters.Count; i++)
        {
            GameObject nextButton = Instantiate(
                buttonPrefab,
                currentButton.transform
            );
            nextButton.transform.SetParent(currentButton.transform, false);
            nextButton.GetComponent<PokemonChoiceButtonController>().SetPokemonSheet(i);
            nextButton.GetComponent<Image>().sprite = pokedex.pokedex[i].sprite;


            currentButton = nextButton;
        }
    }
}
