using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class PlayerController : MonoBehaviour
{
    private bool can_jump = false;
    private bool can_crouch = false;
    public Animator animator;

    private void Update()
    {
        //running left or right
        HoriontalMovement();
        //jumping or crouching
        VerticalMovement();
    }

    private void VerticalMovement()
    {
        float speed = Input.GetAxisRaw("Vertical");
        animator.SetBool("Jump", can_jump);
        animator.SetBool("Crouch", can_crouch);
        if (speed > 0 && !can_jump)
        {
            can_jump = true;
        }
        else if (speed < 0 && !can_crouch)
        {
            can_crouch= true;
        }
        else if(speed == 0)
        {
            if(can_jump)
                can_jump = false;
            if(can_crouch)
                can_crouch = false;
        }
    }

    private void HoriontalMovement()
    {
        float speed = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("HorizontalSpeed", Mathf.Abs(speed));
        Vector3 scale = transform.localScale;
        if (speed < 0)
        {
            scale.x = Mathf.Abs(scale.x) * -1;
        }
        else if (speed > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
    }
}