using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    // The speed of the players movement
    public float speed = 200.0f;

    private Rigidbody2D body;
    private InputAction moveAction;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        moveAction = GetComponent<PlayerInput>().actions["Move"];
        moveAction.Enable();
    }

    private void FixedUpdate()
    {
        var move = moveAction.ReadValue<Vector2>();

        body.velocity = move * (Time.deltaTime * speed);
    }
}