using Krivodeling.UI.Effects;
using UnityEngine;
using UnityEngine.Events;
using ZentySpeede.Player;

namespace ZentySpeede.General
{
    public class Pause : MonoBehaviour
    {
        #region Variables
        [SerializeField] private bool isPaused;
        [SerializeField] UIBlur uiBlur;
        [SerializeField] UnityEvent pauseEvent;
        [SerializeField] UnityEvent unpauseEvent;
        [SerializeField] InputManager inputManager;
        #endregion

        #region Unity Methods
        void Start() => isPaused = false;
        void Update()
        {
            if (inputManager.IsPause)
            {
                Paused();
            }
        }
        #endregion

        #region Methods
        public void Paused()
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                Time.timeScale = 0;
                pauseEvent.Invoke();
            }
            else
            {
                Time.timeScale = 1;
                uiBlur.SetBlur(0);
                unpauseEvent.Invoke();
            }

        }

        public bool GameIsPaused()
        {
            return isPaused;
        }
        #endregion
    }

}
