using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Rigidbody2D rb;
    SpriteRenderer playerSprite;
    Vector2 moveDir;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        InputManagment();
    }
    private void FixedUpdate()
    {
        Move();
    }

    void InputManagment()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY).normalized;
        if (moveX < 0) playerSprite.flipX = true;
        else if (moveX > 0) playerSprite.flipX = false;
    }
    void Move()
    {
        rb.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
    }
}
