using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class LobbyController : MonoBehaviour
{
    [SerializeField] Button play_button;
    [SerializeField] Button exit_button;
    [SerializeField] Button controls_button;
    [SerializeField] Button back_button;
    [SerializeField] Button[] levels;

    private void Awake()
    {
        if (play_button && exit_button && controls_button)
        {
            play_button.onClick.AddListener(LevelSelection);
            exit_button.onClick.AddListener(ExitGame);
            controls_button.onClick.AddListener(Controls);
        }
        else if(levels.Length > 0 && back_button)
        {
            back_button.onClick.AddListener(MainMenu);
            for(int i = 0;i< levels.Length;i++)
            {
                TextMeshProUGUI text_mesh_pro = levels[i].GetComponentInChildren<TextMeshProUGUI>();
                string text = text_mesh_pro.text;
                levels[i].onClick.AddListener(() => LoadLevel(text));
            }
        }
        else if(back_button)
        {
            back_button.onClick.AddListener(MainMenu);
        }
    }

    private void Controls()
    {
        SoundManager.Instance.PlaySoundEffect(SoundTypes.ButtonClick);
        SceneManager.LoadScene(2);
    }

    private void LevelSelection()
    {
        SoundManager.Instance.PlaySoundEffect(SoundTypes.ButtonClick);
        SceneManager.LoadScene(1);
    }

    private void MainMenu()
    {
        SoundManager.Instance.PlaySoundEffect(SoundTypes.ButtonClick);
        SceneManager.LoadScene(0);
    }
    
    private void LoadLevel(string level_name)
    {
        LevelStatus level_status = LevelManager.Instance.GetLevelStatus(level_name);
        switch(level_status)
        {
            case LevelStatus.Locked:
                SoundManager.Instance.PlaySoundEffect(SoundTypes.LockedLevel);
                break;
            case LevelStatus.Unlocked:
                SoundManager.Instance.PlaySoundEffect(SoundTypes.ButtonClick);
                SceneManager.LoadScene(level_name);
                break;
            case LevelStatus.Completed:
                SoundManager.Instance.PlaySoundEffect(SoundTypes.ButtonClick);
                SceneManager.LoadScene(level_name);
                break;
        }
    }

    private void ExitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
    }
}
