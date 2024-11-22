using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    [SerializeField] Button play_button;

    private void Awake()
    {
        play_button.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
