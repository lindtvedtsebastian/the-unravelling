using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInput))]
public class PlayerBehaviour : MonoBehaviour {
    // The speed of the players movement
    public float speed = 200.0f;

    // The inventory UI
    public InventoryUIBehaviour inventoryUI;

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

    // Initialize the components
    private void Awake() {
        body = GetComponent<Rigidbody2D>();

        playerInput = GetComponent<PlayerInput>();
        var actions = playerInput.actions;

        playerAnimation = GetComponent<Animator>();
        
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
        }
    }

    private void PlayerAnimations(Vector2 bodyMove)
    {
        // Animations for the different directions
        if (bodyMove.y > 0)
        {
            playerAnimation.SetBool("Up", true);
            playerAnimation.SetBool("Down", false);
            playerAnimation.SetBool("Right", false);
            playerAnimation.SetBool("Left", false);
            playerAnimation.SetBool("IdleFront", false);
            playerAnimation.SetFloat("Blend", bodyMove.y);
            playerAnimation.SetFloat("Velocity Y", bodyMove.y);

        }
        else if (bodyMove.y < 0)
        {
            playerAnimation.SetBool("Down", true);
            playerAnimation.SetBool("Up", false);
            playerAnimation.SetBool("Right", false);
            playerAnimation.SetBool("Left", false);
            playerAnimation.SetBool("IdleFront", false);
            playerAnimation.SetFloat("Blend", bodyMove.y);
            playerAnimation.SetFloat("Velocity Y", bodyMove.y);
        } 
        else if (bodyMove.x > 0)
        {
            playerAnimation.SetBool("Right", true);
            playerAnimation.SetBool("Left", false);
            playerAnimation.SetBool("Down", false);
            playerAnimation.SetBool("Up", false);
            playerAnimation.SetBool("IdleFront", false);
            playerAnimation.SetFloat("Blend", bodyMove.x);
            playerAnimation.SetFloat("Velocity X", bodyMove.x);
        } 
        else if (bodyMove.x < 0)
        {
            playerAnimation.SetBool("Left", true);
            playerAnimation.SetBool("Right", false);
            playerAnimation.SetBool("Down", false);
            playerAnimation.SetBool("Up", false);
            playerAnimation.SetBool("IdleFront", false);
            playerAnimation.SetFloat("Blend", bodyMove.x);
            playerAnimation.SetFloat("Velocity X", bodyMove.x);
        }
        else
        {
            playerAnimation.SetFloat("Blend", bodyMove.y);
        }
    }

    private void FixedUpdate() {
        var move = moveAction.ReadValue<Vector2>();

        body.velocity = move * (Time.deltaTime * speed);
        
        PlayerAnimations(body.velocity);
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
        Debug.Log("Open Inventory");

        playerInput.SwitchCurrentActionMap("UI");
        inventoryUI.OnActivate(inventory, OnCloseInventory);
    }

    // Called when interact action is triggered
    private void OnActionInteract(InputAction.CallbackContext ctx) {
        Debug.Log("Interact with...");
    }

    // Called when place action is triggered
    private void OnActionPlace(InputAction.CallbackContext ctx) {
        Debug.Log("Place tile/machine");

        // Destroy the preview object when real object is placed
        PlaceObject(item);
    }

    // Called when cancel action is triggered
    private void OnActionCancel(InputAction.CallbackContext ctx) {
        Debug.Log("Cancel current action");

        // Destroy the preview if it exists
        if (previewGameObject.activeSelf) {
            previewGameObject.SetActive(false);
        }
    }

    // Called when destroy action is triggered
    private void OnActionDestroy(InputAction.CallbackContext ctx) {
        Debug.Log("Destroy tile/machine");

        // Look for a unit that is close to the mouse pointer
        var units = GameObject.FindGameObjectsWithTag("Unit");
        foreach (var unit in units) {
            var pos = GetMousePosition();
            if (unit.GetComponent<Collider2D>().OverlapPoint(pos)) {
                Destroy(unit);
                return;
            }
        }
    }
}

/*playerAnimation.SetBool("RunBack", true);
            playerAnimation.SetBool("RunRight", false);
            playerAnimation.SetBool("RunLeft", false);
            playerAnimation.SetBool("RunFront", false);
            playerAnimation.SetBool("IdleFront", false);*/
            
//Debug.Log(body.position);
//Debug.Log("body velocity x : " + body.velocity.x);
//Debug.Log("body velocity y : " + body.velocity.y);

//Debug.Log("Player x position is : " + body.position.x);
//Debug.Log("Player y position is : " + body.position.y);

/*else if (bodyMove.x > 0)
        {
            /*playerAnimation.SetBool("RunRight", true);
            playerAnimation.SetBool("RunLeft", false);
            playerAnimation.SetBool("RunBack", false);
            playerAnimation.SetBool("RunFront", false);
            playerAnimation.SetBool("IdleFront", false);#1#
        } else if (bodyMove.x < 0)
        {
            /*playerAnimation.SetBool("RunLeft", true);
            playerAnimation.SetBool("RunRight", false);
            playerAnimation.SetBool("RunBack", false);
            playerAnimation.SetBool("RunFront", false);
            playerAnimation.SetBool("IdleFront", false);#1#
        }
        else
        {
            /*playerAnimation.SetBool("IdleFront", true);
            playerAnimation.SetBool("RunLeft", false);
            playerAnimation.SetBool("RunRight", false);
            playerAnimation.SetBool("RunBack", false);
            playerAnimation.SetBool("RunFront", false);#1#
        }*/


/*if (bodyPosition.y > 0)
{
    playerAnimation.SetBool("RunBack", true);
    playerAnimation.SetBool("RunFront", false);
}

if (bodyPosition.y < 0)
{
    playerAnimation.SetBool("RunFront", true);
    playerAnimation.SetBool("RunBack", false);
}*/ 
        
/*if (bodyPosition.y < 0)
{
    playerAnimation.SetBool("RunBack", true);
    playerAnimation.SetBool("IdleFront", false);
} else if (bodyPosition.y > -1)
{
    playerAnimation.SetBool("RunBack", false);
    playerAnimation.SetBool("IdleFront", true);
}*/

/*private void PlayUpAnimation(float val)
{
    Debug.Log("Value up is : " + val);
    if (val > 0)
    {
        playerAnimation.SetBool("RunBack", true);
        playerAnimation.SetBool("RunFront", false);
        playerAnimation.SetBool("IdleFront", false);
        playerAnimation.SetBool("IdleBack", false);
    }
    else
    {
        playerAnimation.SetBool("IdleBack", true);
        playerAnimation.SetBool("IdleFront", false);
        playerAnimation.SetBool("RunFront", false);
        playerAnimation.SetBool("RunBack", false);
    }
}*/

/*private void PlayDownAnimation(float val)
{
    Debug.Log("Value down is : " + val);
    
    if (val < 0)
    {
        playerAnimation.SetBool("RunFront", true);
        playerAnimation.SetBool("RunBack", false);
        playerAnimation.SetBool("IdleFront", false);
        playerAnimation.SetBool("IdleBack", false);
    }
    else
    {
        playerAnimation.SetBool("IdleFront", true);
        playerAnimation.SetBool("IdleBack", false);
        playerAnimation.SetBool("RunFront", false);
        playerAnimation.SetBool("RunBack", false);
    }
}*/