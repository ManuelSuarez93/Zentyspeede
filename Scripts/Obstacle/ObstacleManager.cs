using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZentySpeede.Audio;
using UnityEngine.Events;

namespace ZentySpeede.Obstacle
{
    public class ObstacleManager : MonoBehaviour
    {
        [SerializeField] ObstacleSpawner[] spawners;
        [SerializeField] Conductor conductor;
        [SerializeField] List<float> multiplier;
        [SerializeField] float consumableRate;
        [SerializeField] UnityEvent spawnEvent;
        [SerializeField] UnityEvent consumableEvent;
        float nextBeat;
        float nextBeatForConsumable;


        private void Start()
        {
            nextBeat = nextBeat + 8;
        }
        private void Update()
        {
            Timer();
            TimerConsumable();
        }

        private void Timer()
        {
            if (conductor.SongPositionInBeats >= nextBeat)
            {
                NextBeat(SelectMultiplier());
                spawnEvent.Invoke();
            }
        }

        private void TimerConsumable()
        {
            if(conductor.SongPositionInBeats >= nextBeatForConsumable)
            {
                NextBeatForConsumable();
                consumableEvent.Invoke();
            }
        }
        private float NextBeat(float multiplier) => nextBeat = conductor.SongPositionInBeats + multiplier;
        private float NextBeatForConsumable() => nextBeatForConsumable = conductor.SongPositionInBeats + consumableRate;
        private float SelectMultiplier() => multiplier[Random.Range(0, multiplier.Count - 1)];
  

    }

    



}
