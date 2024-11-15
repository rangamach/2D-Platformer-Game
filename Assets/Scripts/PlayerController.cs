using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class PlayerController : MonoBehaviour
{
    private float horizontal_input_speed;
    private Rigidbody2D rb2d;
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
        float vertical = Input.GetAxisRaw("Jump");
        //player vertical movement (jump)
        PlayerVerticalMovement(vertical);
        if(vertical > 0)
        {
            animator.SetBool("Jump", true);
        }
        else if(Time.time - 0 > 0.5f)
        {
            animator.SetBool("Jump", false);
        }
    }
    
    void PlayerVerticalMovement(float vertical)
    {
        if(vertical > 0)
        {
            rb2d.AddForce(new Vector2(0f,jump), ForceMode2D.Impulse);
        }
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