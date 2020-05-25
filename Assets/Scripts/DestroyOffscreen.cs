using UnityEngine;

public class DestroyOffscreen : MonoBehaviour
{
    // represents the value of how far off of the screen
    // it needs to be before it gets destroyed.
    public float offset = 16f;

    public delegate void OnDestroy();
    public event OnDestroy DestroyCallback;

    private bool offscreen;
    private float offscreenX = 0;

    private Rigidbody2D body2d;

    private void Awake()
    {
        body2d = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // actual width / 2

        // even though we find the halfway position of the screen
        // we're still going to take a little bit extra off in order to
        // account for how far off the object needs to be from the screen.
        offscreenX = (Screen.width / PixelPerfectCamera.pixelsToUnits) / 2 + offset;
    }

    // Update is called once per frame
    void Update()
    {
        // store the position x
        var posX = transform.position.x;

        // we'll need to keep track of the actual velocity,
        // so that we know which direction the object is facing
        var dirX = body2d.velocity.x;

        if (Mathf.Abs(posX) > offscreenX)
        {
            if (dirX < 0 && posX < -offscreenX) {
                offscreen = true;
            } else if (dirX > 0 && posX > offscreenX)
            {
                offscreen = true;
            } else
            {
                offscreen = false;
            }
        }
        
        if (offscreen)
        {
            OnOutOfBounds();
        }
    }

    public void OnOutOfBounds()
    {
        offscreen = false;
        GameObjectUtil.Destroy(gameObject);

        if (DestroyCallback != null)
        {
            DestroyCallback();
        }
    }
}
