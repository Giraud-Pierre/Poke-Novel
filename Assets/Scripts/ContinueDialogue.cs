using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContinueDialogue : MonoBehaviour
{
    [SerializeField] GameObject choiceBox = default ;

    private List<string> dialogue;

    public void SetButton(List<string> newDialogue)
    {
        dialogue = newDialogue;
    }

    public void Continue()
    {
        GameObject canvas = GameObject.Find("Canvas");

        if (dialogue.Count == 0)
        {
            GameObject newChoiceBox = Instantiate(
                choiceBox,
                canvas.transform
            );

            newChoiceBox.transform.SetParent(canvas.transform, false);

            Destroy(gameObject.transform.parent.gameObject);
        }
        else
        {
            gameObject.transform.parent.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = dialogue[0];
            dialogue.RemoveAt(0);
        }
    }
}
