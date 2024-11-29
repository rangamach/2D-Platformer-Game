using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float horizontal_input_speed;
    private Rigidbody2D rb2d;
    private bool isGrounded = true;
    private LevelController level_controller;
    private int health_left;
    private bool player_death_sound;

    [SerializeField] ScoreController score_controller;
    [SerializeField] GameOverController game_over_controller;
    [SerializeField] Image[] hearts; 
    [SerializeField] float running_speed;
    [SerializeField] float jump;

    public Animator animator;

    private void Awake()
    {
        ParticleEffectManager.Instance.PlayParticleEffect(ParticleEffectTypes.PlayerSpawn, gameObject.transform);
        SoundManager.Instance.PlaySoundEffect(SoundTypes.PlayerSpawn);
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        level_controller = FindObjectOfType<LevelController>();
        //initializing to total health.
        health_left = hearts.Length;
        player_death_sound = false;
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
            {
                isGrounded = true;
                PlayAnimation("Idle");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            Transform trans = level_controller.GetCheckpoints()[level_controller.GetCheckpointCount() - 1];
            transform.position = trans.position;
            ParticleEffectManager.Instance.PlayParticleEffect(ParticleEffectTypes.PlayerSpawn, trans);
            SoundManager.Instance.PlaySoundEffect(SoundTypes.PlayerSpawn);
        }
    }

    private void VerticalInputSpeed()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && isGrounded)
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
                SoundManager.Instance.PlaySoundEffect(SoundTypes.PlayerJump);
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
        //if (Mathf.Abs(horizontal_input_speed) > 0 && isGrounded)
        //    SoundManager.Instance.PlaySoundEffect(SoundTypes.PlayerMove);
        transform.position = position;
    }

    public void PlayerTransform(Transform transform)
    {
        this.transform.position = transform.position;
    }

    public void PickUpKey() 
    {
        score_controller.IncreaseScore(10);
    }

    public void LoseOneHeart()
    {
        ParticleEffectManager.Instance.PlayParticleEffect(ParticleEffectTypes.EnemyHit, transform);
        SoundManager.Instance.PlaySoundEffect(SoundTypes.EnemyHit);
        if(health_left > 0)
        {
            hearts[--health_left].enabled = false;
        }
        if(health_left == 0)
        {
            if (!player_death_sound)
            {
                player_death_sound = true;
                SoundManager.Instance.PlaySoundEffect(SoundTypes.PlayerDeath);
            }
            animator.Play("Ellen_Death");
        }
    }

    private void KillPlayer()
    {
        game_over_controller.PlayerDied();
        this.enabled = false;
    }
} 