using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZentySpeede.Audio
{
    public class ObstacleAudio : MonoBehaviour
    {
        [SerializeField] List<AudioClip> spawnClips;
        [SerializeField] List<AudioClip> endClip;
        [SerializeField] AudioSource source;

        private void Awake()
        {
            PlaySpawnClip();
        }
        public void PlaySpawnClip()
        {
            source.clip = spawnClips[RandomNumber(spawnClips)];
            source.Play();
            source.volume = 0.1f;
        }

        public void PlayEndClip()
        {
            source.clip = endClip[RandomNumber(endClip)];
            source.Play();
            source.volume = 0.75f;
        }

        public int RandomNumber(List<AudioClip> t) => Random.Range(0, t.Count - 1);

    }
}

