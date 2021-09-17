using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour {
    // The speed of the players movement
    public float speed = 200.0f;

    // Components
    private Rigidbody2D body;
    private InputAction moveAction;

    // Initialize the components
    private void Awake() {
        body = GetComponent<Rigidbody2D>();

        var actions = GetComponent<PlayerInput>().actions;

        // Grab a ref to move action, so we can read it later
        moveAction = actions["Move"];

        // Setup action handlers
        actions["Inventory"].performed += OnActionInventory;
        actions["Interact"].performed += OnActionInteract;
        actions["Place"].performed += OnActionPlace;
        actions["Cancel"].performed += OnActionCancel;
        actions["Destroy"].performed += OnActionDestroy;
    }

    private void FixedUpdate() {
        var move = moveAction.ReadValue<Vector2>();

        body.velocity = move * (Time.deltaTime * speed);
    }

    // Called when inventory action is triggered
    private void OnActionInventory(InputAction.CallbackContext ctx) {
        Debug.Log("Open Inventory");
    }

    // Called when interact action is triggered
    private void OnActionInteract(InputAction.CallbackContext ctx) {
        Debug.Log("Interact with...");
    }

    // Called when place action is triggered
    private void OnActionPlace(InputAction.CallbackContext ctx) {
        Debug.Log("Place tile/machine");
    }

    // Called when cancel action is triggered
    private void OnActionCancel(InputAction.CallbackContext ctx) {
        Debug.Log("Cancel current action");
    }

    // Called when destroy action is triggered
    private void OnActionDestroy(InputAction.CallbackContext ctx) {
        Debug.Log("Destroy tile/machine");
    }
}