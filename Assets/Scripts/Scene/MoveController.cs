using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MoveController : MonoBehaviour
{
    [SerializeField] private GameObject ChoiceBox = default;
    [SerializeField] private GameObject dialogueBox = default;
    [SerializeField] private Dialogues dialogues = default;

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
    private Player player;

    public void SetMove(string NewPlace)
    {
        Place = NewPlace;
    }

    public void ChangeToDialogue(List<string> DialogueText, Sprite Char1, Sprite Char2, int laboratory)
    {
        GameObject newDialogue = Instantiate(
            dialogueBox,
            canvas.transform
        );
        newDialogue.transform.SetParent(canvas.transform, false);

        newDialogue.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Char1;
        newDialogue.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = Char2;
        newDialogue.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = DialogueText[0];
        DialogueText.RemoveAt(0);
        newDialogue.transform.GetChild(2).gameObject.GetComponent<ContinueDialogue>().SetButton(DialogueText,laboratory);
        

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

        player = canvas.GetComponent<Player>();

        if (Place != "Route 101")
        {
            player.SetLocation(Place);
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

                if (player.CheckIfDialogueRead(0) == 0)
                {
                    nextDialogue = new List<string>(dialogues.mom);
                    ChangeToDialogue(nextDialogue, PlayerSprite, MomSprite, 0);
                    player.SetDialogueRead(0);
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

                if(player.CheckIfDialogueRead(1) == 0 && player.GetPokemon(0) == null)
                {
                    nextDialogue = new List<string>(dialogues.rivalMomBeforeLab);
                    ChangeToDialogue(nextDialogue, PlayerSprite, RivalMomSprite, 0);
                    player.SetDialogueRead(1);
                }
                else if (player.CheckIfDialogueRead(2) == 0 && player.GetPokemon(0) != null)
                {
                    nextDialogue = new List<string>(dialogues.rivalMomAfterLab);
                    ChangeToDialogue(nextDialogue, PlayerSprite, RivalMomSprite, 0);
                    player.SetDialogueRead(2);
                }
                else
                {
                    ChangeToNewChoice();
                }
                break;



            case "Laboratoire":
                background.GetComponent<Image>().sprite = LaboratorySprite;

                if(player.CheckIfDialogueRead(3) == 0)
                {
                    nextDialogue = new List<string>(dialogues.professorPine);
                    ChangeToDialogue(nextDialogue, PlayerSprite, ProfessorSprite, 1);
                    player.SetDialogueRead(3);
                }
                else
                {
                    ChangeToNewChoice();
                }
                break;



            case "Route 101":
                if(player.GetPokemon(0) == null)
                {
                    nextDialogue = new List<string>(dialogues.randomAnnoyingDude);
                    ChangeToDialogue(nextDialogue, PlayerSprite, RandomAnnoyingDude, 0);
                }
                else
                {
                    nextDialogue = new List<string>(dialogues.randomAnnoyingDude);
                    ChangeToDialogue(nextDialogue, PlayerSprite, RivalSprite, 0);
                }
                break;
        }
    }
}
