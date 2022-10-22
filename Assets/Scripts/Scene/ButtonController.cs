using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject currentMenu = default;
    [SerializeField] private GameObject nextMenu = default;

    public void ChangeMenu()
    {
        GameObject canvas = GameObject.Find("Canvas");

        GameObject newMenu = Instantiate(
            nextMenu,
            canvas.transform
        );

        newMenu.transform.SetParent (canvas.transform, false);

        Destroy(currentMenu);
    }
}
