using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GenerateMoveChoice : MonoBehaviour
{
    [SerializeField] private MovementTable MovementTable = default;
    [SerializeField] private GameObject buttonPrefab = default;

    private Player player;
    
    
    private void Start()
    {
        player = GameObject.Find("Canvas").GetComponent<Player>();

        switch (player.GetLocation())
        {
            case "Chambre":
                GenerateButton(MovementTable.Bedroom);
                break ;
            case "Salon":
                GenerateButton(MovementTable.Livingroom);
                break ;
            case "Village":
                GenerateButton(MovementTable.Village);
                break;
            case "Maison du rival":
                GenerateButton(MovementTable.RivalHouse);
                break;
            case "Laboratoire":
                GenerateButton(MovementTable.Laboratory);
                break;
        }
    }

    private void GenerateButton(List<string> mylist)
    {
        GameObject currentButton = gameObject.transform.GetChild(0).gameObject;
        currentButton.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = mylist[0];
        currentButton.GetComponent<MoveController>().SetMove(mylist[0]);

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
                nextButton.GetComponent<MoveController>().SetMove(mylist[i]);

                currentButton = nextButton;
            }
        }
    }
}
