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
        [SerializeField] float currentHunger;
        [SerializeField] float maxHunger;
        [SerializeField] float hungerRate;
        [SerializeField] Image hungerBar;
        [SerializeField] UnityEvent hungerEvent;
        #endregion

        #region Unity Methods
        private void Start()
        {
            currentHunger = maxHunger;
        }
        void Update()
        {
            ImageFill();
            HungerCheck();
            HungerOverpassControl();
            EventCheck();
        }
        #endregion

        #region Methods
        private void EventCheck()
        {
            if(currentHunger <= 0)
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
        private void HungerCheck() => currentHunger -= Time.deltaTime * hungerRate;
        private float HungerPercent => currentHunger / maxHunger;
        private float ImageFill() => hungerBar.fillAmount = HungerPercent;
        public void AddHunger(float i) => currentHunger += i;
        public void RemoveHunger(float i) => currentHunger -= i;
        public void SetHunger(float i) => currentHunger = i;
        #endregion
    }
}

