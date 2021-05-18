using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetScore : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] float score;
    [SerializeField] float multiplier = 1;
    
    public float HighScore 
    { 
        get => PlayerPrefs.GetFloat("HighScore", 0);
        set
        {
            PlayerPrefs.SetFloat("HighScore", value);
            PlayerPrefs.Save();
        }
    }

    private void Awake()
    {
        score = 0;
    }

    void Update()
    {
        ScoreTime();
        SetScoreUI();
    }


    private void SetMultiplier(float m)
    {
        multiplier = m;
    }

    private void ScoreTime() => score += Time.deltaTime * multiplier;
    private int ScoreToInt() => Mathf.CeilToInt(score);
    private void SetScoreUI() => scoreText.text = $"{ScoreToInt()}";
    public void AddScore(int i) => score += i;

}
