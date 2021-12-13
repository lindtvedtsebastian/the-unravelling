using UnityEngine;

public class WorldStateManager : MonoBehaviour {
    private WorldManager _worldManager;
    public GameObject NightEffect;

    private void Start() {
        _worldManager = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldManager>();
        InvokeRepeating(nameof(IncrementGameTimeAndDay), 0.0f, 1.0f);
		NightEffect = GameObject.FindWithTag("NightEffect");
    }

    /// <summary>
    /// Increments a specific world's time and day.
    /// <see cref="WorldState.TickTime()"/>
    /// </summary>
    public void IncrementGameTimeAndDay() {
        _worldManager.world.state.TickTime();
        NightEffect.SetActive(_worldManager.world.state.IsNight());
        if (_worldManager.world.state.regenerateResource) {
            _worldManager.regenerateResources();
            _worldManager.world.state.regenerateResource = false;
        }
    }
    


    public World getWorld() {
        return _worldManager.world;
    }
} 
