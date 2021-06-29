using UnityEngine;
using UnityEngine.Events;
using ZentySpeede.Player;

public class PlayerDeath : MonoBehaviour
{
    #region Variabels
    [SerializeField] ObstacleDetection obstacle;
    [SerializeField] HungerMeter hunger;
    [SerializeField] GameObject deathText;
    [SerializeField] float maxTime;
    [SerializeField] float currentTimer;
    [SerializeField] UnityEvent deathEvent;
    [SerializeField] bool death;
    #endregion
    #region Unity Methods
    private void Awake() => Time.timeScale = 1;
    void Start() => currentTimer = 0;
    void Update()
    {
        if(death) Timer();
    }
    #endregion
    #region Methods
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
    #endregion
}
