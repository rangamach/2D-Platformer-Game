using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class PlayerController : MonoBehaviour
{
    private float horizontal_input_speed;
    private Rigidbody2D rb2d;
    private bool isGrounded = true;
    [SerializeField] float running_speed;
    [SerializeField] float jump;
    public Animator animator;

    private void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //running animation both left or right direction.
        HorizontalInputSpeed();
        //jumping and crouching animations.
        VerticalInputSpeed();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            if (!isGrounded)
                isGrounded = true;
        }
    }

    private void VerticalInputSpeed()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            animator.SetBool("Crouch", true);
        }
        else
        {
            animator.SetBool("Crouch", false);
        }
        PlayerVerticalMovement();
    }
    
    void PlayerVerticalMovement()
    {
        float vertical = Input.GetAxisRaw("Jump");
        if (vertical > 0)
        {
            if (isGrounded)
            {
                isGrounded = false;
                animator.SetBool("Jump", true);
                rb2d.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
            }
        }
        else
            animator.SetBool("Jump", false);
    }

    //play animation by trigger.
    public void PlayAnimation(string trigger)
    {
        animator.SetTrigger(trigger);
    }

    private void HorizontalInputSpeed()
    {
        horizontal_input_speed = Input.GetAxisRaw("Horizontal");
        //player movement either left or right.
        PlayerHorizontalMovement(horizontal_input_speed);
        animator.SetFloat("HorizontalSpeed", Mathf.Abs(horizontal_input_speed));
        Vector3 scale = transform.localScale;
        if (horizontal_input_speed < 0)
        {
            scale.x = Mathf.Abs(scale.x) * -1;
        }
        else if (horizontal_input_speed > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
    }

    private void PlayerHorizontalMovement(float horizontal_input_speed)
    {
        Vector3 position = transform.position;
        position.x += horizontal_input_speed * running_speed * Time.deltaTime;
        transform.position = position;
    }
}