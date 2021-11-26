using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private PlayerInput playerInput;

    private void Awake() {
        controls = new Theunravelling();

        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable() {
        //controls.Player.Enable();
        //controls.Player.Move.performed += OnMove;
        //controls.Player.Inventory.performed += OnOpenInventory;
        //controls.Player.Place.performed += OnActionPlace;
        ////controls.Player.Cancel.performed += OnActionCancel;

        //controls.UI.Cancel.performed += OnCloseInventory;

        playerInput.actions["Player/Move"].performed += OnMove;
        playerInput.actions["Player/Inventory"].performed += OnOpenInventory;
        playerInput.actions["Player/Place"].performed += OnActionPlace;
        playerInput.actions["Player/Cancel"].performed += OnActionCancel;
        playerInput.actions["Player/Destroy"].performed += OnActionDamage;

        playerInput.actions["UI/Cancel"].performed += OnCloseInventory;
    }

    private void OnMove(InputAction.CallbackContext ctx) {
        Vector2 moveInput = ctx.ReadValue<Vector2>();
        Debug.Log($"Move input: {moveInput}");
    }

    public void publicOpenInventory() {
        playerInput.SwitchCurrentActionMap("UI");
        Debug.Log("Current actionmap : " + playerInput.currentActionMap);
        playerInventory.ActivateInventory();
    }
    
    private void OnOpenInventory(InputAction.CallbackContext ctx) {
        Debug.Log("This will open the inventory");
        Debug.Log("Current actionmap : " + playerInput.currentActionMap);
        publicOpenInventory();        
    }

    public void publicCloseInventory() {
        playerInput.SwitchCurrentActionMap("Player");
        Debug.Log("Current actionmap : " + playerInput.currentActionMap);
        playerInventory.DeActivateInventory();
    }

    private void OnCloseInventory(InputAction.CallbackContext ctx) {
        //Debug.Log("This will close the inventory");
        //Debug.Log("Current actionmap : " + playerInput.currentActionMap);
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
		RaycastHit2D[] hits = Physics2D.RaycastAll(GetComponent<PlayerBehaviour>().GetMousePosition2D(),Vector2.zero);
		foreach (RaycastHit2D hit in hits)
		if (hit.collider != null) {
            hit.collider.GetComponent<IClickable>()?.OnDamage(50);
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
}
