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
        LevelStatus level_status = (LevelStatus)PlayerPrefs.GetInt(level_name);
        return level_status;
    }

    public void SetLevelStatus(string level_name, LevelStatus level_status)
    {
        PlayerPrefs.SetInt(level_name, (int)level_status);
    }

    public void MarkCurrentLevelComplete()
    {
        Scene current_scene = SceneManager.GetActiveScene();
        SetLevelStatus(current_scene.name, LevelStatus.Completed);
        string next_scene_name = NameFromIndex(SceneManager.GetActiveScene().buildIndex + 1);
        SetLevelStatus(next_scene_name, LevelStatus.Unlocked);
    }

    private string NameFromIndex(int index)
    {
        string path_to_name = SceneUtility.GetScenePathByBuildIndex(index);
        int slash = path_to_name.LastIndexOf('/');
        string name = path_to_name.Substring(slash + 1);
        int dot = name.LastIndexOf(".");
        return name.Substring(0, dot);
    }
}
