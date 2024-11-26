using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;

    public string[] all_level_names;

    public static LevelManager Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (GetLevelStatus(all_level_names[0]) == LevelStatus.Locked)
            SetLevelStatus(all_level_names[0], LevelStatus.Unlocked);    
    }

    public LevelStatus GetLevelStatus(string level_name)
    {
        LevelStatus level_status = (LevelStatus)PlayerPrefs.GetInt(level_name, 0);
        return level_status;
    }

    public void SetLevelStatus(string level_name, LevelStatus level_status)
    {
        PlayerPrefs.SetInt(level_name, (int)level_status);
        Debug.Log(level_name + " - status - " + level_status);
    }

    public void MarkCurrentLevelComplete()
    {
        Scene current_scene = SceneManager.GetActiveScene();
        SetLevelStatus(current_scene.name, LevelStatus.Completed);
        int current_scene_index = Array.FindIndex(all_level_names, level_name => level_name == current_scene.name);
        int next_scene_index = current_scene_index + 1;
        if (next_scene_index < all_level_names.Length)
            SetLevelStatus(all_level_names[next_scene_index], LevelStatus.Unlocked);

    }
}
