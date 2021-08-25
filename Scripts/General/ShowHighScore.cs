using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHighScore : MonoBehaviour
{
    [SerializeField] Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        scoreText.text = $"{Mathf.CeilToInt(PlayerPrefs.GetFloat("HighScore"))}";
    }

    public void ResetHighScore()
    {
        PlayerPrefs.SetFloat("HighScore", 0);
        PlayerPrefs.Save();
    }
}
