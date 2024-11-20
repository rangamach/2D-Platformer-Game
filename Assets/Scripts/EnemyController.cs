using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{

    [SerializeField] float patrol_distance;
    [SerializeField] float patrol_speed; 
    private Transform initial_transform;
    private float mid_point;
    //If true move left to right.
    private bool forward;
    //If false input speed.
    private int look;
    //If false will pause for 5 seconds.
    private bool moving;

    private void Awake()
    {
        patrol_distance /= 2;
        initial_transform = transform;
        mid_point = initial_transform.position.x + (patrol_distance);
        forward = true;
        moving = true;
        look = 1;
    }

    private void Update()
    {
        Vector3 position = transform.position;
        if (moving)
        {
            if (forward)
                position.x += look * patrol_speed * Time.deltaTime;
            else
                position.x -= look * patrol_speed * Time.deltaTime;
        }
        else
        {
            look = 0;
            StartCoroutine(LookForFiveSeconds());
        }
        if (Mathf.Abs(position.x - mid_point) > patrol_distance)
        {
            moving = false;
            if (forward)
                forward = false;
            else
                forward = true;
        }
        transform.position = position;
    }

    private IEnumerator LookForFiveSeconds()
    {
        yield return new WaitForSeconds(5);
        look = 1;
        moving = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>())
        {
            Animator animator = collision.gameObject.GetComponent<Animator>();
            animator.Play("Ellen_Death");
        }
    }
}
