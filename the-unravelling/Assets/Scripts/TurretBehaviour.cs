using UnityEngine;

public class TurretBehaviour: MonoBehaviour {
    public float speed = 200.0f;
    
    private Rigidbody2D body;

    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        body.transform.Rotate(Vector3.back * (speed * Time.deltaTime));
    }
}
