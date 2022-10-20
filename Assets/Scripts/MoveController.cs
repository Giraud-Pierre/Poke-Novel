using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] GameObject background = default;
    [SerializeField] GameObject ChoiceBox = default;

    [SerializeField] ScriptableObject player = default;

    [SerializeField] Sprite BedroomSprite = default;
    [SerializeField] Sprite LivingroomSprite = default ;
    [SerializeField] Sprite VillageSprite = default;
    [SerializeField] Sprite RivalHouseSprite = default;
    [SerializeField] Sprite LaboratorySprite = default;

    private string Place = default;

    public void SetMove(string NewPlace)
    {
        Place = NewPlace;
    }

    public void Move()
    {
        switch (Place)
        {
            case "Bedroom":
                background.GetComponent<Image>().sprite = BedroomSprite;
                
                break;
            case "Livingroom":

                break;
        }
    }
}
