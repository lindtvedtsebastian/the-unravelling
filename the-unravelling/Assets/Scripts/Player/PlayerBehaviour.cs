using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInput))]
public class PlayerBehaviour : MonoBehaviour {
    // The speed of the players movement
    public float speed = 200.0f;
    
    // Components
    private Rigidbody2D body;
    private Animator playerAnimation;

    // Audio
    public AudioSource walkingLSound;
    public AudioSource walkingRSound;
    
    // Animation 
    private static readonly int VelocityY = Animator.StringToHash("Velocity Y");
    private static readonly int VelocityX = Animator.StringToHash("Velocity X");

    // Initialize the components
    private void Awake() {
        body = GetComponent<Rigidbody2D>();

        playerAnimation = GetComponent<Animator>();
        
        walkingLSound = GetComponent<AudioSource>();
        walkingLSound.volume = 0.2f;
        walkingRSound = GetComponent<AudioSource>();
        walkingRSound.volume = 0.2f;
    }

    private void FixedUpdate() {
        Vector2 move = GetComponent<PlayerInput>().actions["Player/Move"].ReadValue<Vector2>();
        
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
}
