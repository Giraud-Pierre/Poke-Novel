using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GenerateMoveChoice : MonoBehaviour
{
    [SerializeField] private GameObject player = default;
    [SerializeField] private GameObject buttonPrefab = default;

    private void Start()
    {
        GenerateButton(new List<string>{"zone1","zone2","zone3"});
        //  switch (player.place) {
        //     case bedroom:
        //         GenerateButton();
        //         break;
        //  }
    }

    private void GenerateButton(List<string> mylist)
    {
        GameObject currentButton = gameObject.transform.GetChild(0).gameObject;
        currentButton.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = mylist[0];
        // currentButton.GetComponent<MoveController>().MovePlace(mylist[0]);

        if (mylist.Count != 1)
        {
            for (int i = 1; i < mylist.Count; i++)
            {
                GameObject nextButton = Instantiate(
                    buttonPrefab,
                    currentButton.transform
                );
                nextButton.transform.SetParent(currentButton.transform, false);
                nextButton.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = mylist[i];
                // nextButton.GetComponent<MoveController>().MovePlace(mylist[i]);

                currentButton = nextButton;
            }
        }
    }
}
