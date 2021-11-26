using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour {
    Theunravelling controls;

    [SerializeField]
    private PlayerInventory playerInventory;

    [SerializeField]
    private GameObject inGameMenu;

    [SerializeField]
    private GameObject HUD;

    // Global objects
    private Mouse mouse;
    private Camera currentCamera;

    private PlayerInput playerInput;

    private void Awake() {
        controls = new Theunravelling();

        playerInput = GetComponent<PlayerInput>();
 
        // Grab global objects
        mouse = Mouse.current;
        currentCamera = Camera.main;

        Assert.IsNotNull(mouse, "No mouse found");
        Assert.IsNotNull(currentCamera, "No main camera set");  
    }

    private void OnEnable() {
        playerInput.actions["Player/Inventory"].performed += OnOpenInventory;
        playerInput.actions["Player/Place"].performed += OnActionPlace;
        playerInput.actions["Player/Cancel"].performed += OnActionCancel;
        playerInput.actions["Player/Destroy"].performed += OnActionDamage;

        playerInput.actions["UI/Cancel"].performed += OnCloseInventory;
    }

    private void OnDisable() {
        playerInput.actions["Player/Inventory"].performed -= OnOpenInventory;
        playerInput.actions["Player/Place"].performed -= OnActionPlace;
        playerInput.actions["Player/Cancel"].performed -= OnActionCancel;
        playerInput.actions["Player/Destroy"].performed -= OnActionDamage;

        playerInput.actions["UI/Cancel"].performed -= OnCloseInventory;
    }

    public void publicOpenInventory() {
        playerInput.SwitchCurrentActionMap("UI");
        playerInventory.ActivateInventory();
    }
    
    private void OnOpenInventory(InputAction.CallbackContext ctx) {
        publicOpenInventory();        
    }

    public void publicCloseInventory() {
        playerInput.SwitchCurrentActionMap("Player");
        playerInventory.DeactivateInventory();
    }

    private void OnCloseInventory(InputAction.CallbackContext ctx) {
        publicCloseInventory();
        inGameMenu.SetActive(false);
    }

    private void OnActionPlace(InputAction.CallbackContext ctx) {
        Debug.Log("This will place an object");
        playerInventory.PlaceObject();
    }

    private void OnActionCancel(InputAction.CallbackContext ctx) {
        Debug.Log("This will cancel an action");
        if (playerInventory.previewCraft.activeSelf) {
            playerInput.SwitchCurrentActionMap("Player");
            playerInventory.CancelInventoryAction();
        } else {
            inGameMenu.SetActive(true);
        }
    }

    private void OnActionDamage(InputAction.CallbackContext ctx) {
		RaycastHit2D[] hits = Physics2D.RaycastAll(GetMousePosition(),Vector2.zero);
		foreach (RaycastHit2D hit in hits)
		if (hit.collider != null) {
            hit.collider.GetComponent<IClickable>()?.OnDamage(playerInventory.player.entityDamage);
        }
	}

    public void SaveGameAndExitButtonClick() {
        inGameMenu.SetActive(false);
        HUD.SetActive(false);
        GameData.Get.SaveWorld();
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
    
    public void ResumeButtonClick() {
        inGameMenu.SetActive(false);
        playerInput.SwitchCurrentActionMap("Player");
    }

    /// <summary>
    /// Function to get mouse position
    /// </summary>
    public Vector3 GetMousePosition() {
        // Grab the position of the mouse in screen space
        Vector3 mousePos = mouse.position.ReadValue();
        mousePos.z = 1.0f;

        // Convert to world space coordinates
        return currentCamera.ScreenToWorldPoint(mousePos);
    }
 

}
