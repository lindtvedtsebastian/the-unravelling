using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour {

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


    private void Awake() {

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
        playerInput.actions["Player/RotateObject"].performed += OnActionRotateObject;
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
        playerInput.actions["Player/RotateObject"].performed += OnActionRotateObject;

        playerInput.actions["Dialogue/Cancel"].performed -= OnCloseDialogue;
        playerInput.actions["UI/Cancel"].performed -= OnCloseInventory;
    }

    /// <summary>
    /// Function that can be called outside this class to activate inventory
    /// </summary>
    public void publicOpenInventory() {
        playerInput.actions.Disable();
        playerInput.SwitchCurrentActionMap("UI");
        playerInput.actions.Enable(); 
        playerInventory.ActivateInventory();
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
        playerInput.actions.Disable();
        playerInput.SwitchCurrentActionMap("Player");
        playerInput.actions.Enable();
        playerInventory.DeactivateInventory();
        storageInventoryDisplay.DeactivateStorageInventory();
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
		if (hit.collider != null && Constants.CHESTS.Contains(hit.collider.GetComponent<BaseUnit>().GetObjectID())) {
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

    private void OnActionRotateObject(InputAction.CallbackContext ctx) {
        playerInventory.RotateSprite();
    }

    /// <summary>
    /// Function to get mouse position
    /// </summary>
    /// <param name="ctx">Input action callback for registering action</param>
    private void OnActionCancel(InputAction.CallbackContext ctx) {
        if (playerInventory.previewCraft.activeSelf) {
            playerInput.SwitchCurrentActionMap("Player");
            playerInventory.CancelPreviewAction();
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
            hit.collider.GetComponent<IClickable>()?.OnDamage(playerInventory.player.entityDamage,false);
        }
	}

    /// <summary>
    /// Function for button to save and exit the game
    /// </summary>
    public void SaveGameAndExitButtonClick() {
        WorldHandler.saveWorld(GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldManager>().world);
        inGameMenu.SetActive(false);
        HUD.SetActive(false);
        StartCoroutine(loadAfterDelay("MainMenu"));
    }

    IEnumerator loadAfterDelay(string sceneName) {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
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
        StartCoroutine(DialogueManager.instance.ContinueStory());
    }

    private void OnCloseDialogue(InputAction.CallbackContext ctx) {
        playerInput.actions.Disable();
        playerInput.SwitchCurrentActionMap("Player");
        playerInput.actions.Enable();
        StartCoroutine(DialogueManager.instance.ExitDialogueMode());
    }
}
