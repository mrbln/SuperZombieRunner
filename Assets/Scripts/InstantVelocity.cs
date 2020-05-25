using UnityEngine;

public class InstantVelocity : MonoBehaviour
{
    public Vector2 velocity = Vector2.zero;

    private Rigidbody2D body2d;

    private void Awake()
    {
        body2d = GetComponent<Rigidbody2D>();
    }

    // Limited number of times per frame
    // more efficient for doing any of physics calculations
    private void FixedUpdate()
    {
        body2d.velocity = velocity;
    }
}
