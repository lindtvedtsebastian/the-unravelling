using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour {
    // The speed of the players movement
    public float speed = 200.0f;

    // Prefab to base the preview object upon
    public GameObject previewPrefab;

    // GameObject that previews where to place tiles
    // NOTE: We could reuse the same object instead of instantiating and destroying objects
    private GameObject previewGameObject;

    // TODO: Replace this with prefab chosen from inventory
    public GameObject machine;

    // Components
    private Rigidbody2D body;
    private InputAction moveAction;

    // Global objects
    private Mouse mouse;
    private Camera currentCamera;

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

        // Grab global objects
        mouse = Mouse.current;
        currentCamera = Camera.main;

        // Assert that the scene is setup to support player behaviour
        Assert.IsNotNull(mouse, "No mouse found");
        Assert.IsNotNull(currentCamera, "No main camera set");
    }

    private void Update() {
        // Move the preview object to under the mouse
        if (previewGameObject) {
            previewGameObject.transform.position = GetMousePosition();
        }
    }

    private void FixedUpdate() {
        var move = moveAction.ReadValue<Vector2>();

        body.velocity = move * (Time.deltaTime * speed);
    }

    // Create a placement preview based on prefab object
    private void CreatePreview(GameObject prefab) {
        if (previewGameObject == null) {
            Assert.IsNotNull(prefab.GetComponent<SpriteRenderer>(),
                "Prefab to be placed must have a sprite renderer component");

            // Create the new object
            // The position 
            previewGameObject = Instantiate(previewPrefab);

            // Set the opacity of the object to 50%
            var sprite = previewGameObject.GetComponent<SpriteRenderer>();
            sprite.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
            var spriteColor = sprite.color;
            spriteColor.a = 0.5f;
            sprite.color = spriteColor;
        }
    }

    // Place object into the scene, based on the location of the preview
    private void PlaceObject(GameObject prefab) {
        if (previewGameObject) {
            // Create final object
            Instantiate(prefab, previewGameObject.transform.position, Quaternion.identity);

            // Destroy the preview object
            Destroy(previewGameObject);
        }
    }

    // Get the word space position of the mouse
    private Vector3 GetMousePosition() {
        // Grab the position of the mouse in screen space
        Vector3 mousePos = mouse.position.ReadValue();
        mousePos.z = 1.0f;

        // Convert to world space coordinates
        return currentCamera.ScreenToWorldPoint(mousePos);
    }

    // Called when inventory action is triggered
    private void OnActionInventory(InputAction.CallbackContext ctx) {
        Debug.Log("Open Inventory");

        // Create a preview object for previewing placement
        CreatePreview(machine);
    }

    // Called when interact action is triggered
    private void OnActionInteract(InputAction.CallbackContext ctx) {
        Debug.Log("Interact with...");
    }

    // Called when place action is triggered
    private void OnActionPlace(InputAction.CallbackContext ctx) {
        Debug.Log("Place tile/machine");

        // Destroy the preview object when real object is placed
        PlaceObject(machine);
    }

    // Called when cancel action is triggered
    private void OnActionCancel(InputAction.CallbackContext ctx) {
        Debug.Log("Cancel current action");

        // Destroy the preview if it exists
        if (previewGameObject) {
            Destroy(previewGameObject);
        }
    }

    // Called when destroy action is triggered
    private void OnActionDestroy(InputAction.CallbackContext ctx) {
        Debug.Log("Destroy tile/machine");

        // Look for a unit that is close to the mouse pointer
        var units = GameObject.FindGameObjectsWithTag("Unit");
        foreach (var unit in units) {
            var pos = GetMousePosition();
            var dist = Vector3.Distance(unit.transform.position, pos);
            if (dist < 1.0f) {
                Destroy(unit);
                return;
            }
        }
    }
}