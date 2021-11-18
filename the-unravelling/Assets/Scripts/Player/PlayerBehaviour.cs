using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInput))]
public class PlayerBehaviour : MonoBehaviour {
    // The speed of the players movement
    public float speed = 200.0f;

    public PlayerInventory playerInventory;
    [SerializeField]
    public GameObject InGameMenu;
    [SerializeField]
    public GameObject HUD;
    
    // Components
    private Rigidbody2D body;
    public PlayerInput playerInput;
    private InputAction moveAction;
    private Animator playerAnimation;

    public AudioSource walkingSound;

    // Global objects
    private Mouse mouse;
    private Camera currentCamera;
    
    private static readonly int VelocityY = Animator.StringToHash("Velocity Y");
    private static readonly int VelocityX = Animator.StringToHash("Velocity X");

    // Initialize the components
    private void Awake() {
        body = GetComponent<Rigidbody2D>();

        playerInput = GetComponent<PlayerInput>();
        var actions = playerInput.actions;

        playerAnimation = GetComponent<Animator>();
        
        walkingSound = GetComponent<AudioSource>();

        // Grab a ref to move action, so we can read it later
        moveAction = actions["Move"];

        // Setup action handlers
        actions["Player/Inventory"].performed += OnOpenInventory;
        /*actions["Player/Interact"].performed += OnActionInteract;*/
        actions["Player/Place"].performed += OnActionPlace;
        actions["Player/Cancel"].performed += OnActionCancel;
        //actions["Player/Destroy"].performed += OnActionDestroy;
        actions["Player/Destroy"].performed += OnActionDamage;
        actions["UI/Cancel"].performed += OnCloseInventory;

        // Grab global objects
        mouse = Mouse.current;
        currentCamera = Camera.main;
        
        Assert.IsNotNull(mouse, "No mouse found");
        Assert.IsNotNull(currentCamera, "No main camera set");
        
    }

    private void FixedUpdate() {
        var move = moveAction.ReadValue<Vector2>();

        body.velocity = move * (Time.deltaTime * speed);

        // int x =  Mathf.FloorToInt(body.transform.position.x);
        // int y = Mathf.CeilToInt(body.transform.position.y -0.5f) - 1;

        // Debug.Log(GameData.Get.world.pathfindingMap[256 - y, x]);

        if (move.x != 0) {
			playerAnimation.SetFloat(VelocityX, move.x);
			playerAnimation.SetFloat(VelocityY, 0);	
		} else if (move.y != 0) {
			playerAnimation.SetFloat(VelocityX, 0);
			playerAnimation.SetFloat(VelocityY, move.y);	
		} else {
			playerAnimation.SetFloat(VelocityX, 0);
			playerAnimation.SetFloat(VelocityY, 0);	
		}
	}
    
    private void PlayWalkingSound() {
        walkingSound.Play();
    }

    public void OnOpenInventory(InputAction.CallbackContext ctx) {
        //Debug.Log("Activate UI");
        OpenInventory();
    }

    public void OpenInventory() {
        playerInput.SwitchCurrentActionMap("UI");
        playerInventory.ActivateInventory();
    }

    public void SaveGameAndExitButtonClick() {
        InGameMenu.SetActive(false);
        HUD.SetActive(false);
        GameData.Get.SaveWorld();
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
    
    public void ResumeButtonClick() {
        InGameMenu.SetActive(false);
        playerInput.SwitchCurrentActionMap("Player");
    }

    public void CloseInventory()
    {
        playerInput.SwitchCurrentActionMap("Player");
        playerInventory.DeActivateInventory();
    }

    // Called when the inventory UI closes
    public void OnCloseInventory(InputAction.CallbackContext ctx) {
        //Debug.Log("Deactivate UI");
        CloseInventory();
        InGameMenu.SetActive(false);
    }

    // Called when place action is triggered
    public void OnActionPlace(InputAction.CallbackContext ctx) {
        // Destroy the preview object when real object is placed
        playerInventory.PlaceObject();
    }

    // Called when cancel action is triggered
    public void OnActionCancel(InputAction.CallbackContext ctx) {
        // Destroy the preview if it exists
        if (playerInventory.previewCraft.activeSelf) {
            playerInput.SwitchCurrentActionMap("Player");
            playerInventory.CancelInventoryAction();
        } else {
            InGameMenu.SetActive(true);
        }
    }

	private void OnActionDamage(InputAction.CallbackContext ctx) {
		RaycastHit2D[] hits = Physics2D.RaycastAll(GetMousePosition2D(),Vector2.zero);
		foreach (RaycastHit2D hit in hits)
		if (hit.collider != null) {
            hit.collider.GetComponent<IClickable>()?.OnDamage(50);
        }
	}
	
    /// <summary>
    /// Function to get mouse position
    /// </summary>
    private Vector2 GetMousePosition2D() {
        // Grab the position of the mouse in screen space
        Vector3 mousePos = mouse.position.ReadValue();

        // Convert to world space coordinates
        return currentCamera.ScreenToWorldPoint(mousePos);
    }

}
