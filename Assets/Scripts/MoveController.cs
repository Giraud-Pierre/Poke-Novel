using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MoveController : MonoBehaviour
{
    [SerializeField] private GameObject ChoiceBox = default;
    [SerializeField] private GameObject DialogueBox = default;

    [SerializeField] private Player player = default;
    [SerializeField] private Dialogues Dialogue = default;

    [SerializeField] private Sprite BedroomSprite = default;
    [SerializeField] private Sprite LivingroomSprite = default ;
    [SerializeField] private Sprite VillageSprite = default;
    [SerializeField] private Sprite RivalHouseSprite = default;
    [SerializeField] private Sprite LaboratorySprite = default;

    [SerializeField] private Sprite PlayerSprite = default;
    [SerializeField] private Sprite MomSprite = default;
    [SerializeField] private Sprite RivalMomSprite = default;
    [SerializeField] private Sprite ProfessorSprite = default;
    [SerializeField] private Sprite RivalSprite = default;
    [SerializeField] private Sprite RandomAnnoyingDude = default;

    private GameObject canvas = default;
    private GameObject position = default;
    private GameObject background = default;
    private string Place = default;
    private List<string> nextDialogue;

    public void SetMove(string NewPlace)
    {
        Place = NewPlace;
    }

    public void ChangeToDialogue(List<string> DialogueText, Sprite Char1, Sprite Char2)
    {
        GameObject newDialogue = Instantiate(
            DialogueBox,
            canvas.transform
        );
        newDialogue.transform.SetParent(canvas.transform, false);

        newDialogue.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Char1;
        newDialogue.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = Char2;
        newDialogue.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = DialogueText[0];
        DialogueText.RemoveAt(0);
        newDialogue.transform.GetChild(2).gameObject.GetComponent<ContinueDialogue>().SetButton(DialogueText);
        

        Destroy(GameObject.Find("MovementBox(Clone)"));

    }

    public void ChangeToNewChoice()
    {

        GameObject newChoice = Instantiate(
            ChoiceBox,
            canvas.transform
        );

        newChoice.transform.SetParent(canvas.transform, false);

        Destroy(GameObject.Find("MovementBox(Clone)"));
    } 

    public void Move()
    {
        canvas = GameObject.Find("Canvas");
        background = GameObject.Find("Background");
        position = GameObject.Find("Position");

        if (Place != "Route 101")
        {
            player.location = Place;
            position.GetComponent<TextMeshProUGUI>().text = Place;
        }


        switch (Place)
        {
            case "Chambre":
                background.GetComponent<Image>().sprite = BedroomSprite;

                ChangeToNewChoice();
                break;



            case "Salon":
                background.GetComponent<Image>().sprite = LivingroomSprite;

                if (player.dialoguesAlreadyHad[0] == 0)
                {
                    nextDialogue = new List<string>(Dialogue.mom);
                    ChangeToDialogue(nextDialogue, PlayerSprite, MomSprite);
                    player.dialoguesAlreadyHad[0] = 1;
                }
                else
                {
                    ChangeToNewChoice();
                }
                break;



            case "Village":
                background.GetComponent<Image>().sprite = VillageSprite;

                ChangeToNewChoice();
                break;



            case "Maison du rival":
                background.GetComponent<Image>().sprite = RivalHouseSprite;

                if(player.dialoguesAlreadyHad[1] == 0 && player.pokemon.Count == 0)
                {
                    nextDialogue = new List<string>(Dialogue.rivalMomBeforeLab);
                    ChangeToDialogue(nextDialogue, PlayerSprite, RivalMomSprite);
                    player.dialoguesAlreadyHad[1] = 1;
                }
                else if (player.dialoguesAlreadyHad[2] == 0 && player.pokemon.Count != 0)
                {
                    nextDialogue = new List<string>(Dialogue.rivalMomAfterLab);
                    ChangeToDialogue(nextDialogue, PlayerSprite, RivalMomSprite);
                    player.dialoguesAlreadyHad[2] = 1;
                }
                else
                {
                    ChangeToNewChoice();
                }
                break;



            case "Laboratoire":
                background.GetComponent<Image>().sprite = LaboratorySprite;

                if(player.dialoguesAlreadyHad[3] == 0)
                {
                    nextDialogue = new List<string>(Dialogue.professorPine);
                    ChangeToDialogue(nextDialogue, PlayerSprite, ProfessorSprite);
                    player.dialoguesAlreadyHad[3] = 1;
                }
                else
                {
                    ChangeToNewChoice();
                }
                break;



            case "Route 101":
                if(player.pokemon.Count != 0)
                {
                    nextDialogue = new List<string>(Dialogue.randomAnnoyingDude);
                    ChangeToDialogue(nextDialogue, PlayerSprite, RivalSprite);
                }
                else
                {
                    nextDialogue = new List<string>(Dialogue.randomAnnoyingDude);
                    ChangeToDialogue(nextDialogue, PlayerSprite, RandomAnnoyingDude);
                }
                break;
        }
    }
}
