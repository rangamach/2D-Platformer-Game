using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private static int total_scenes;
    private int checkpoint_count = 1;
    private PlayerController player;
    [SerializeField] Transform[] checkpoints;
    [SerializeField] GameOverController game_over_controller;
    [SerializeField] LevelCompleteController level_complete_controller;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerController>();
        //number of scenes in build.
        total_scenes = SceneManager.sceneCountInBuildSettings;
        //spawns player at beginning when loading scene.
        transform.position = checkpoints[checkpoint_count].transform.position;
    }

    //manages thw checkpoints in level.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            if (checkpoint_count < checkpoints.Length - 1)
            {
                transform.position = checkpoints[++checkpoint_count].transform.position;
            }
            else if (checkpoint_count == checkpoints.Length - 1)
            {
                player.GetComponent<Animator>().SetFloat("HorizontalSpeed", 0f);
                player.GetComponent<PlayerController>().enabled = false;
                int scene_ind = SceneManager.GetActiveScene().buildIndex;
                Debug.Log("scene ind - " + scene_ind);
                if(scene_ind == total_scenes - 1)
                {
                    SoundManager.Instance.PlaySoundEffect(SoundTypes.LevelComplete);
                    ParticleEffectManager.Instance.PlayParticleEffect(ParticleEffectTypes.GameComplete, player.transform);
                    game_over_controller.PlayerDied();
                }
                else if (scene_ind < total_scenes - 1)
                {
                    LevelManager.Instance.MarkCurrentLevelComplete();
                    SoundManager.Instance.PlaySoundEffect(SoundTypes.LevelComplete);
                    ParticleEffectManager.Instance.PlayParticleEffect(ParticleEffectTypes.LevelComplete, player.transform);
                    level_complete_controller.LevelComplete();
                }
            }
        }
    }

    public int GetCheckpointCount()
    {
        return checkpoint_count;
    }

    public Transform[] GetCheckpoints()
    {
        return checkpoints;
    }
}
