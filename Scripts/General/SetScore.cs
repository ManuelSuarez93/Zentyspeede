using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetScore : MonoBehaviour
{
    #region Variables
    float score;
    float multiplier = 1;
    float comboTime;
    bool isInCombo = false;
    int counter;

    [SerializeField] List<int> comboThresholds;
    [SerializeField] float maxComboTime;

    [Header("UI settings")]
    [SerializeField] Image circle;
    [SerializeField] Text multiplierText;
    [SerializeField] Image comboTimerBar;
    [SerializeField] Text scoreText;
    public bool IsInCombo { get => isInCombo; set => isInCombo = value; }

    #endregion

    #region Unity Methods

    private void Awake()
    {
        score = 0;
        counter = 0;
    }

    void Update()
    {
        ScoreTime();
        SetScoreUI();
        SetMultiplierUI();
        if (isInCombo && comboTime > 0)
        {
            ComboTimer();
        }
        else
        {
            IsInCombo = false;
            counter = 0;
        }
    }
    #endregion

    #region Methods
    public void ComboTimer(){ comboTime -= Time.deltaTime; }
    public void AddCounter()
    {
        counter ++;
        comboTime = maxComboTime;
        CheckMultiplier();
    }
    public void CheckMultiplier()
    {
        for(int i = 0; i < comboThresholds.Count; i++)
        {
            if(counter >= comboThresholds[i])
            {
                multiplier = (i+1)*2;
            }
        }
    }
    void SetMultiplierUI()
    {
        circle.fillAmount = counter/15f;
        multiplierText.text = $"X{multiplier}";
        comboTimerBar.fillAmount = comboTime / maxComboTime;
    }
    public float HighScore
    {
        get => PlayerPrefs.GetFloat("HighScore", 0);
        set
        {
            PlayerPrefs.SetFloat("HighScore", value);
            PlayerPrefs.Save();
        }
    }


    #endregion

    #region OneLine Methods
    public void SetMultiplier(int m) => multiplier = m;
    public void SetComboTimer(int i) => comboTime = i;
    private void ScoreTime() => score += Time.deltaTime * multiplier;
    private int ScoreToInt() => Mathf.CeilToInt(score);
    private void SetScoreUI() => scoreText.text = $"{ScoreToInt()}";
    public void AddScore(int i) => score += i;
    #endregion

}


