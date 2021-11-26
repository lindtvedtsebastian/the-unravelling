using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInput))]
public class PlayerBehaviour : MonoBehaviour {
    // The speed of the players movement
    public float speed = 200.0f;
    
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

        // Grab global objects
        mouse = Mouse.current;
        currentCamera = Camera.main;
        
        Assert.IsNotNull(mouse, "No mouse found");
        Assert.IsNotNull(currentCamera, "No main camera set");
        
    }

    private void FixedUpdate() {
        var move = GetComponent<PlayerInput>().actions["Move"].ReadValue<Vector2>();
        
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
	
    /// <summary>
    /// Function to get mouse position
    /// </summary>
    public Vector2 GetMousePosition2D() {
        // Grab the position of the mouse in screen space
        Vector3 mousePos = mouse.position.ReadValue();

        // Convert to world space coordinates
        return currentCamera.ScreenToWorldPoint(mousePos);
    }

}
