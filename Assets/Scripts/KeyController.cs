using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class KeyController : MonoBehaviour
{
    [SerializeField] float fade_duration;
    private SpriteRenderer sprite_renderer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>())
        {
            PlayerController player_controller = collision.gameObject.GetComponent<PlayerController>();
            player_controller.PickUpKey();
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeOut()
    {
        SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();
        Color initial_key_color = sprite_renderer.color;
        float elapsed_time = 0f;
        while(elapsed_time < fade_duration)
        {
            elapsed_time += Time.deltaTime;
            float alpha = Mathf.Lerp(initial_key_color.a,0,elapsed_time/fade_duration);
            sprite_renderer.color = new Color(initial_key_color.r, initial_key_color.g, initial_key_color.b, alpha);
            yield return null;
        }
        sprite_renderer.color = new Color(initial_key_color.r, initial_key_color.g, initial_key_color.b, 0f);
        Destroy(gameObject);
    }
}
