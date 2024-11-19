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

    private void Awake()
    {
        //number of scenes in build.
        total_scenes = SceneManager.sceneCount;
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
                int scene_ind = SceneManager.GetActiveScene().buildIndex;
                if(scene_ind == total_scenes)
                {
                    Debug.Log("Game Complete!!!");
                }
                else if (scene_ind < total_scenes)
                {
                    SceneManager.LoadScene(scene_ind + 1);
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
