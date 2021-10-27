using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class VisualizeInventory : MonoBehaviour {
    [SerializeField]
    private GameObject inventoryCanvas;

    public Item stone;
    public Item wood;

    public void ActivateInventory()
    {
        Debug.Log("Visualize inventory activate");
        inventoryCanvas.SetActive(true);
    }

    public void DeActivateInventory()
    {
        Debug.Log("Visualize inventory deactivate");
        inventoryCanvas.SetActive(false);
    }

    /*public void OnClose(InputAction.CallbackContext ctx)
    {
        DeActivateInventory();
    }*/
}
