using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    [SerializeField] Button restart_button;
    [SerializeField] Button main_menu_button;
    private void Awake()
    {
        restart_button.onClick.AddListener(ReloadLevel);
        main_menu_button.onClick.AddListener(BackToMainMenu);
    }

    public void PlayerDied()
    {
        gameObject.SetActive(true);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void BackToMainMenu()
    {
        SceneManager.LoadScene(1);
    }
}
