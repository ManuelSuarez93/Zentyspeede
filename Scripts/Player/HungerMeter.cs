using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ZentySpeede.Player
{
    public class HungerMeter : MonoBehaviour
    {
        #region Variables

        [Header("Hunger settings")]
        [SerializeField] float currentHunger;
        [SerializeField] float maxHunger;
        [SerializeField] float hungerRate;
        [SerializeField] float minHunger;
        [Header("Timer start")]
        [SerializeField] float startTimer;
        [Header("UI Images")]
        [SerializeField] float hungerAdviceRate;
        [SerializeField] Image hungerBar;
        [SerializeField] Image hungerAdvice;
        [Header("Events")]
        [SerializeField] UnityEvent noHungerStartEvent;
        [SerializeField] UnityEvent noHungerStopEvent;
        [SerializeField] UnityEvent hungerEvent;

        float timer;
        bool enableHunger;
        bool hasMinHunger;

        Vector3 hungerAdviceColorValues;
        #endregion

        #region Unity Methods
        private void Start()
        {
            currentHunger = maxHunger;
            hasMinHunger = false;
            StartCoroutine(StartTimerRoutine());
            
        }
        void Update()
        {
            ImageFill();
            HungerCheck();
            HungerOverpassControl();
            HungerEventCheck();
            WhenMinHunger();
            ChangeImageInHunger();
        }
        #endregion

        #region Methods

        IEnumerator StartTimerRoutine()
        {
            timer = 0;
            enableHunger = false;
            while(timer < startTimer)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            noHungerStopEvent.Invoke();
            enableHunger = true;
        }
        private void HungerEventCheck()
        {
            if (currentHunger <= 0)
            {
                hungerEvent.Invoke();
            }
        }
        private void HungerOverpassControl()
        {
            if (currentHunger > maxHunger)
            {
                currentHunger = maxHunger;
            }
        }
        private void WhenMinHunger() 
        { 
            if (currentHunger < minHunger)
            {
                hasMinHunger = true;
            }
            else if (hasMinHunger && currentHunger >= minHunger)
            {
                hasMinHunger = false;
            }
        }
        private void ChangeImageInHunger()
        {
            Color alphaChange = hungerAdvice.color;
            if (currentHunger <= minHunger)
            {
                alphaChange.a = currentHunger > 0 ? ((maxHunger / currentHunger) * hungerAdviceRate) : 1;
            }
            else
            {
                alphaChange.a = 0;
            }
            hungerAdvice.color = alphaChange;
        }
        private void HungerCheck() => currentHunger -= enableHunger ? Time.deltaTime * hungerRate : 0;
        private float HungerPercent => currentHunger / maxHunger;
        private float ImageFill() => hungerBar.fillAmount = HungerPercent;
        public void AddHunger(float i) => currentHunger += i;
        public void RemoveHunger(float i) => currentHunger -= i;
        public void SetHunger(float i) => currentHunger = i;
        public void SetTimer(float i)
        {
            startTimer = i;
            StopAllCoroutines();
            noHungerStartEvent.Invoke();
            StartCoroutine(StartTimerRoutine());
        }

        #endregion
    }
}

