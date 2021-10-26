using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInput))]
public class PlayerBehaviour : MonoBehaviour {
    // The speed of the players movement
    public float speed = 200.0f;

    // The inventory UI
    public InventoryUIBehaviour inventoryUI;

    public AudioSource walkingLSound;
    public AudioSource walkingRSound;

    // The players inventory
    public Inventory inventory;

    // NOTE: This is just a placeholder for having an inventory UI where this is the selected item
    public ItemData item;

    // GameObject that previews where to place tiles
    public GameObject previewGameObject;

    // Components
    private Rigidbody2D body;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private Animator playerAnimation;

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
        walkingRSound = GetComponent<AudioSource>();
        
        // Test var for capturing movement for animations

        // Grab a ref to move action, so we can read it later
        moveAction = actions["Move"];

        // Setup action handlers
        actions["Player/Inventory"].performed += OnActionInventory;
        actions["Player/Interact"].performed += OnActionInteract;
        actions["Player/Place"].performed += OnActionPlace;
        actions["Player/Cancel"].performed += OnActionCancel;
        actions["Player/Destroy"].performed += OnActionDestroy;
        actions["UI/Cancel"].performed += inventoryUI.OnClose;

        // Grab global objects
        mouse = Mouse.current;
        currentCamera = Camera.main;

        // Assert that the scene is setup to support player behaviour
        Assert.IsNotNull(mouse, "No mouse found");
        Assert.IsNotNull(currentCamera, "No main camera set");

        inventory.AddItem(item, 2);

        // We need to make a new instance of the game object, so that we can use it.
        previewGameObject = Instantiate(previewGameObject);
        // But it should still be disabled
        previewGameObject.SetActive(false);
    }

    private void Update() {
        // Move the preview object to under the mouse
        if (previewGameObject.activeSelf) {
            previewGameObject.transform.position = GetMousePosition();
            previewGameObject.transform.position = new Vector3(
                Mathf.Floor(previewGameObject.transform.position.x) + 0.5f,
                Mathf.Floor(previewGameObject.transform.position.y) + 0.5f,
                previewGameObject.transform.position.z);
        }
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

    // Create a placement preview based on prefab object
    private void CreatePreview(in ItemData item) {
        if (previewGameObject.activeSelf) return;

        if (!inventory.HasItem(item)) return;

        previewGameObject.SetActive(true);
        var sprite = previewGameObject.GetComponent<SpriteRenderer>();
        sprite.sprite = item.preview;
    }

    // Place object into the scene, based on the location of the preview
    private void PlaceObject(in ItemData item) {
        // Only place item, if preview was active
        if (!previewGameObject.activeSelf) return;

        // Remove item from inventory
        if (!inventory.RemoveItem(item)) return;

        // Create final object
        Instantiate(item.manifestation, previewGameObject.transform.position, Quaternion.identity);

        // Deactivate the preview
        previewGameObject.SetActive(false);
    }

    // Get the word space position of the mouse
    private Vector3 GetMousePosition() {
        // Grab the position of the mouse in screen space
        Vector3 mousePos = mouse.position.ReadValue();
        mousePos.z = 1.0f;

        // Convert to world space coordinates
        return currentCamera.ScreenToWorldPoint(mousePos);
    }

    // Called when the inventory UI closes
    private void OnCloseInventory(in ItemData item) {
        playerInput.SwitchCurrentActionMap("Player");

        if (item != null) {
            CreatePreview(item);
        }
    }

    // Called when inventory action is triggered
    private void OnActionInventory(InputAction.CallbackContext ctx) {
        playerInput.SwitchCurrentActionMap("UI");
        inventoryUI.OnActivate(inventory, OnCloseInventory);
    }

    // Called when interact action is triggered
    private void OnActionInteract(InputAction.CallbackContext ctx) {
    }

    // Called when place action is triggered
    private void OnActionPlace(InputAction.CallbackContext ctx) {
        // Destroy the preview object when real object is placed
        PlaceObject(item);
    }

    // Called when cancel action is triggered
    private void OnActionCancel(InputAction.CallbackContext ctx) {
        // Destroy the preview if it exists
        if (previewGameObject.activeSelf) {
            previewGameObject.SetActive(false);
        }
    }

    // Called when destroy action is triggered
    private void OnActionDestroy(InputAction.CallbackContext ctx) {
        // Look for a unit that is close to the mouse pointer
        var units = GameObject.FindGameObjectsWithTag("Unit");
        foreach (var unit in units) {
            var pos = GetMousePosition();
            if (unit.GetComponent<Collider2D>().OverlapPoint(pos)) {
                var bb = unit.GetComponent<BaseUnit>();
                if (bb) {
                    bb.Damage(50);
                }
                return;
            }
        }
    }
}
