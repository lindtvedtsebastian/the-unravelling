using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// A class representing the player behaviour
/// </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInput))]
public class PlayerBehaviour : MonoBehaviour {
    // The speed of the players movement
    public float speed = 200.0f;

    // The players health
    [SerializeField] private int health = 100;
    [SerializeField] private GameObject healthBar;

    // Damage the player inflicts on an entity
    public int entityDamage = 50;
    
    // Components
    private Rigidbody2D body;
    private Animator playerAnimation;

    // Audio
    public AudioSource walkingLSound;
    public AudioSource walkingRSound;
    
    // Animation 
    private static readonly int VelocityY = Animator.StringToHash("Velocity Y");
    private static readonly int VelocityX = Animator.StringToHash("Velocity X");

    // Game over screen
    [SerializeField]
    private GameOverScreen gameOverScreen;
    
    // Initialize the components
    private void Awake() {
        body = GetComponent<Rigidbody2D>();

        playerAnimation = GetComponent<Animator>();
        
        walkingLSound = GetComponent<AudioSource>();
        walkingLSound.volume = 0.2f;
        walkingRSound = GetComponent<AudioSource>();
        walkingRSound.volume = 0.2f;
        
        var bar = Instantiate(healthBar, this.transform);
        var data = bar.GetComponent<HealthBar>();
        data.Health += () => health / 100f;
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

    public void PlayRightWalking() {
	    walkingRSound.Play();
    }

    public void PlayLeftWalking() {
	    walkingLSound.Play();
    }

    public bool OnDamage(int damage) {
	    health -= damage;
	    
	    // Player is dead
	    if (health <= 0) {
		    gameOverScreen.Setup();
		    Time.timeScale = 0.0f;
		    return true;
	    }

	    return false;
    }
}
