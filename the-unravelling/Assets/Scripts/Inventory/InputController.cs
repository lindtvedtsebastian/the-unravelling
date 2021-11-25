using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour {
    Theunravelling controls;

    private void Awake() {
        controls = new Theunravelling();
    }

    private void OnEnable() {
        controls.Player.Enable();
        controls.Player.Move.performed += OnMove;
    }

    private void OnMove(InputAction.CallbackContext ctx) {
        Vector2 moveInput = ctx.ReadValue<Vector2>();
        Debug.Log($"Move input: {moveInput}");
    }
}
