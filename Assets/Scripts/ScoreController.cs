using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreController : MonoBehaviour
{
    private TextMeshProUGUI score_text;
    private int score = 0;

    private void Awake()
    {
        score_text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        RefreshUI();
    }

    public void IncreaseScore(int increment)
    {
        SoundManager.Instance.PlaySoundEffect(SoundTypes.Pickup);
        score += increment;
        RefreshUI();
    }

    private void RefreshUI()
    {
        score_text.text = "Score: " + score;
    }
}
