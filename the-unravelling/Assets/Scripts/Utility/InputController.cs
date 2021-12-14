using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour {
    Theunravelling controls;

    [SerializeField]
    private PlayerInventoryDisplay playerInventory;

    [SerializeField]
    private StorageInventoryDisplay storageInventoryDisplay;

    [SerializeField]
    private GameObject inGameMenu;

    [SerializeField]
    private GameObject HUD;

    // Global objects
    private Mouse mouse;
    private Camera currentCamera;

    private PlayerInput playerInput;

    private World _world;

    private void Awake() {
        _world = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldManager>().world;
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
        playerInput.actions["Player/Interact"].performed += OnActionInteract;
        playerInput.actions["Player/DialogueTrigger"].performed += OnActionDialogue;
        playerInput.actions["Dialogue/Submit"].performed += OnActionProgress;
        
        playerInput.actions["Dialogue/Cancel"].performed += OnCloseDialogue;
        playerInput.actions["UI/Cancel"].performed += OnCloseInventory;
    }

    private void OnDisable() {
        playerInput.actions["Player/Inventory"].performed -= OnOpenInventory;
        playerInput.actions["Player/Place"].performed -= OnActionPlace;
        playerInput.actions["Player/Cancel"].performed -= OnActionCancel;
        playerInput.actions["Player/Destroy"].performed -= OnActionDamage;
        playerInput.actions["Player/Interact"].performed -= OnActionInteract;
        playerInput.actions["Player/DialogueTrigger"].performed -= OnActionDialogue;
        playerInput.actions["Dialogue/Submit"].performed -= OnActionProgress;

        playerInput.actions["Dialogue/Cancel"].performed -= OnCloseDialogue;
        playerInput.actions["UI/Cancel"].performed -= OnCloseInventory;
    }

    /// <summary>
    /// Function that can be called outside this class to activate inventory
    /// </summary>
    public void publicOpenInventory() {
        playerInventory.ActivateInventory();
        playerInput.actions.Disable();
        playerInput.SwitchCurrentActionMap("UI");
        playerInput.actions.Enable();
    }
    
    /// <summary>
    /// Function to open the inventory subscribed to input action
    /// </summary>
    /// <param name="ctx">Input action callback for registering action</param>
    private void OnOpenInventory(InputAction.CallbackContext ctx) {
        publicOpenInventory();        
    }

    /// <summary>
    /// Function to get mouse position
    /// </summary>
    public void publicCloseInventory() {
        playerInventory.DeactivateInventory();
        storageInventoryDisplay.DeactivateStorageInventory();
        playerInput.actions.Disable();
        playerInput.SwitchCurrentActionMap("Player");
        playerInput.actions.Enable();
    }

    /// <summary>
    /// Function to get mouse position
    /// </summary>
    /// <param name="ctx">Input action callback for registering action</param>
    private void OnCloseInventory(InputAction.CallbackContext ctx) {
        publicCloseInventory();
        inGameMenu.SetActive(false);
    }

    /// <summary>
    /// Function to get mouse position
    /// </summary>
    /// <param name="ctx">Input action callback for registering action</param>
    private void OnActionInteract(InputAction.CallbackContext ctx) {
        RaycastHit2D[] hits = Physics2D.RaycastAll(GetMousePosition(),Vector2.zero);
		foreach (RaycastHit2D hit in hits)
		if (hit.collider != null && hit.collider.name == "Chest(Clone)") {
            playerInput.actions.Disable();
            playerInput.SwitchCurrentActionMap("UI");
            playerInput.actions.Enable();
            InventoryWithStorage storage = hit.collider.GetComponent<InventoryWithStorage>();
            storageInventoryDisplay.ActivateStorageInventory(storage);
        }
    }

    /// <summary>
    /// Function to get mouse position
    /// </summary>
    /// <param name="ctx">Input action callback for registering action</param>
    private void OnActionPlace(InputAction.CallbackContext ctx) {
        playerInventory.PlaceObject();
    }

    /// <summary>
    /// Function to get mouse position
    /// </summary>
    /// <param name="ctx">Input action callback for registering action</param>
    private void OnActionCancel(InputAction.CallbackContext ctx) {
        if (playerInventory.previewCraft.activeSelf) {
            playerInput.SwitchCurrentActionMap("Player");
            playerInventory.CancelInventoryAction();
        } else {
            inGameMenu.SetActive(true);
        }
    }

    /// <summary>
    /// Function to get mouse position
    /// </summary>
    /// <param name="ctx">Input action callback for registering action</param>
    private void OnActionDamage(InputAction.CallbackContext ctx) {
		RaycastHit2D[] hits = Physics2D.RaycastAll(GetMousePosition(),Vector2.zero);
		foreach (RaycastHit2D hit in hits)
		if (hit.collider != null) {
            hit.collider.GetComponent<IClickable>()?.OnDamage(playerInventory.player.entityDamage);
        }
	}

    /// <summary>
    /// Function for button to save and exit the game
    /// </summary>
    public void SaveGameAndExitButtonClick() {
        inGameMenu.SetActive(false);
        HUD.SetActive(false);
        WorldHandler.saveWorld(_world);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
    
    /// <summary>
    /// Function for button to resume the game
    /// </summary>   
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

    private void OnActionDialogue(InputAction.CallbackContext ctx) {
        playerInput.actions.Disable();
        playerInput.SwitchCurrentActionMap("Dialogue");
        playerInput.actions.Enable();
        DialogueManager.instance.EnterDialogueMode();
    }

    private void OnActionProgress(InputAction.CallbackContext ctx) {
        DialogueManager.instance.ContinueStory();
    }

    private void OnCloseDialogue(InputAction.CallbackContext ctx) {
        playerInput.actions.Disable();
        playerInput.SwitchCurrentActionMap("Player");
        playerInput.actions.Enable();
        DialogueManager.instance.ExitDialogueMode();
    }
}
