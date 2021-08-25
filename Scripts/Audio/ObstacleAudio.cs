using System.Collections.Generic;
using UnityEngine;

namespace ZentySpeede.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class ObstacleAudio : MonoBehaviour
    {
        #region Variables
        [SerializeField] List<AudioClip> spawnClips;
        [SerializeField] List<AudioClip> endClip;
        [SerializeField] List<AudioClip> crashClip;
        [SerializeField] AudioSource source;
        [SerializeField] bool playSoundOnSpawn = true;
        #endregion

        #region Methods

        private void Start()
        {
            if (AudioController.Instance.Sounds != null) 
            {
                Debug.Log("LO QUEEEEE");
                AudioController.Instance.Sounds.Add(source); 
            }
        }
        private void OnEnable()
        {
            PlaySpawnClip();
        }
        public void PlaySpawnClip()
        {
            if(playSoundOnSpawn)
            {
                source.clip = spawnClips[RandomNumber(spawnClips)];
                source.Play();
                source.volume = 0.1f;
            }
        }

        public void PlayEndClip()
        {
            source.clip = endClip[RandomNumber(endClip)];
            source.Play();
            source.volume = 0.75f;
        }
        
        public void PlayCrashClip()
        {
            source.clip = crashClip[RandomNumber(crashClip)];
            source.Play();
            source.volume = 0.75f;
        }
        public int RandomNumber(List<AudioClip> t) => Random.Range(0, t.Count - 1);
        #endregion
    }
}

