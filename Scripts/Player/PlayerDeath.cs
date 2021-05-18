using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;
using ZentySpeede.Player;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] ObstacleDetection obstacle;
    [SerializeField] HungerMeter hunger;
    [SerializeField] Text deathText;
    [SerializeField] float maxTime;
    [SerializeField] float currentTimer;
    [SerializeField] UnityEvent deathEvent;
    [SerializeField] bool death;

    private void Awake()
    {
        Time.timeScale = 1;
    }

    void Start()
    {
        currentTimer = 0;
    }

    
    void Update()
    {
        if(death) Timer();
    }

    public void Death()
    {
        death = true;
        Time.timeScale = 0;
        deathText.gameObject.SetActive(true);
    }

    public void Timer()
    {
        if(currentTimer < maxTime)
        {
            currentTimer += Time.unscaledDeltaTime;
        }
        else
        {
            deathEvent.Invoke();
            Time.timeScale = 1;
        }
    }
}
