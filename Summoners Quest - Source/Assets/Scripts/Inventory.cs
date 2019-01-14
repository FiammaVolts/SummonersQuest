using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private CanvasManager _canvasManager;

    void Start()
    {
        _canvasManager = CanvasManager.instance;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (_canvasManager.interactionPanel == false)
            {
                _canvasManager.ShowInventoryPage();
            }
            else
            {
                _canvasManager.HideInventoryPage();
            }
        }
    }
}
