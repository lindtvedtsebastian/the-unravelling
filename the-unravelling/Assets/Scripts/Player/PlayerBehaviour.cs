using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInput))]
public class PlayerBehaviour : MonoBehaviour {
    // The speed of the players movement
    public float speed = 200.0f;

    public PlayerInventory playerInventory;

    // Components
    private Rigidbody2D body;
    public PlayerInput playerInput;
    private InputAction moveAction;
    private Animator playerAnimation;
    
    public AudioSource walkingLSound;
    public AudioSource walkingRSound;

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
        
        walkingLSound = GetComponent<AudioSource>();
        walkingLSound.volume = 0.2f;
        walkingRSound = GetComponent<AudioSource>();
        walkingRSound.volume = 0.2f;
        
        // Grab a ref to move action, so we can read it later
        moveAction = actions["Move"];

        // Setup action handlers
        actions["Player/Inventory"].performed += OnOpenInventory;
        /*actions["Player/Interact"].performed += OnActionInteract;*/
        actions["Player/Place"].performed += OnActionPlace;
        actions["Player/Cancel"].performed += OnActionCancel;
        //actions["Player/Destroy"].performed += OnActionDestroy;
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
    
    private void PlayRightWalkingSound() {
        walkingRSound.Play();
    }

    private void PlayLeftWalkingSound() {
        walkingLSound.Play();
    }

    public void OnOpenInventory(InputAction.CallbackContext ctx) {
        //Debug.Log("Activate UI");
        playerInput.SwitchCurrentActionMap("UI");
        playerInventory.ActivateInventory();
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
    }

    // Called when place action is triggered
    public void OnActionPlace(InputAction.CallbackContext ctx) {
        // Destroy the preview object when real object is placed
        playerInventory.PlaceObject();
    }

    // Called when cancel action is triggered
    public void OnActionCancel(InputAction.CallbackContext ctx) {
        // Destroy the preview if it exists
        playerInventory.CancelInventoryAction();
    }
    
    private void OnActionDestroy(InputAction.CallbackContext ctx) {
        // Look for a unit that is close to the mouse pointer
        /*var units = GameObject.FindGameObjectsWithTag("Unit");
        foreach (var unit in units) {
            var pos = GetMousePosition();
            if (unit.GetComponent<Collider2D>().OverlapPoint(pos)) {
                var bb = unit.GetComponent<BaseUnit>();
                if (bb) {
                    bb.Damage(50);
                }
                return;
            }
        }*/
    }
    
}