using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using UnityEngine;

public class Obstacle : MonoBehaviour, IRecycle
{
    public Sprite[] sprites;

    // Because of the size -height- of tv is heigher than the other stuff
    // like desk, we're going to create a colliderOffset.
    public Vector2 colliderOffset = Vector2.zero;

    public void Restart()
    {
        // The sprite renderer is what actually displays sprites on a game object.
        var renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = sprites[Random.Range(0, sprites.Length)];

        // This will give us the correct size based on the current sprite in the renderer. 
        var collider = GetComponent<BoxCollider2D>();
        var size = renderer.bounds.size;

        size.y += colliderOffset.y;

        collider.size = size;
        collider.offset = new Vector2(-colliderOffset.x, collider.size.y / 2 - colliderOffset.y);
    }

    public void ShutDown()
    {

    }
}
