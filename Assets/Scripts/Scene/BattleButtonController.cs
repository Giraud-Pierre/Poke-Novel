using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleButtonController : MonoBehaviour
{
    private enum ButtonType
    {
        menu,
        getAttacks,
        pokemon,
        bag,
        flee,
        attack1,
        attack2,
        attack3,
        attack4,
        next
    };

    [SerializeField] private ButtonType buttonType = ButtonType.menu;
    private List<string> dialogue;

    public void OnClick()
    {
        BattleController battleController = GameObject.Find("Canvas").gameObject.GetComponent<BattleController>();

        switch (buttonType)
        {
            case ButtonType.menu :
                battleController.Menu();
                break;
            case ButtonType.getAttacks :
                battleController.ShowAttack();
                break;
            case ButtonType.pokemon :
                battleController.Pokemon();
                break;
            case ButtonType.bag :
                battleController.Bag();
                break;
            case ButtonType.flee :
                battleController.Flee();
                break;
            case ButtonType.attack1 :
                battleController.UseAttack(0);
                break;
            case ButtonType.attack2 :
                battleController.UseAttack(1);
                break;
            case ButtonType.attack3 :
                battleController.UseAttack(2);
                break;
            case ButtonType.attack4 :
                battleController.UseAttack(3);
                break;
            case ButtonType.next :
                if (dialogue.Count != 0)
                {
                    gameObject.transform.parent.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = dialogue[0];
                    dialogue.RemoveAt(0);
                }
                else {
                    battleController.Menu();
                }
                break;
        }
    }

    public void SetDialogue(List<string> newDialogue)
    {
        //Permet de r�cup�rer les prochains dialogues
        dialogue = newDialogue; //Contient la liste des prochianes r�pliques du dialogue
    }
}
