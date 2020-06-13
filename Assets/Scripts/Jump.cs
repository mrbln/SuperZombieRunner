﻿using UnityEngine;
using UnityEngine.EventSystems;

public class Jump : MonoBehaviour
{
    public float jumpSpeed = 240f;
    public float forwardSpeed = 20;

    private Rigidbody2D body2d;
    private InputState inputState;

    private void Awake()
    {
        body2d = GetComponent<Rigidbody2D>();
        inputState = GetComponent<InputState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputState.standing)
        {
            if (inputState.actionButton && !EventSystem.current.currentSelectedGameObject)
            {
                body2d.velocity = new Vector2(transform.position.x < 0 ? forwardSpeed : 0, jumpSpeed);
            }
        }
    }
}
