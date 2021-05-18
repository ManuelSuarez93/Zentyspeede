using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ZentySpeede.Player
{
    public class HungerMeter : MonoBehaviour
    {
        [SerializeField] float currentHunger;
        [SerializeField] float maxHunger;
        [SerializeField] float hungerRate;
        [SerializeField] Image hungerBar;
        [SerializeField] UnityEvent hungerEvent;

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

        private void EventCheck()
        {
            if(currentHunger <= 0)
            {
                hungerEvent.Invoke();
            }
        }

        private void HungerCheck() => currentHunger -= Time.deltaTime * hungerRate;
        private float HungerPercent => currentHunger / maxHunger;
        private float ImageFill() => hungerBar.fillAmount = HungerPercent;

        private void HungerOverpassControl()
        {
            if (currentHunger > maxHunger)
            {
                currentHunger = maxHunger; 
            }
        }
        public void AddHunger(float i) => currentHunger += i;

        public void RemoveHunger(float i) => currentHunger -= i;

        public void SetHunger(float i) => currentHunger = i;

    }
}

