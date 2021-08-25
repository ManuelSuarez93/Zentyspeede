using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZentySpeede.Audio
{
    public class AudioController : MonoBehaviour
    {
            
        private static AudioController _instance;
        public static AudioController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<AudioController>();

                    if (_instance == null)
                    {
                        GameObject container = new GameObject("AudioController");
                        _instance = container.AddComponent<AudioController>();
                    }
                }

                return _instance;
            }
        }

        public List<AudioSource> Sounds;
        public AudioSource Music;

        void Awake()
        {
            if (Instance == null && _instance != this)
            {
                Destroy(this.gameObject);
            }

            else
            {
                _instance = this;
            }

            DontDestroyOnLoad(this.gameObject);
            Sounds = new List<AudioSource>();
        }
        private void Update()
        {
            GetSoundsVolume();
            GetMusicVolume();
        }
        void GetSoundsVolume()
        {
            foreach(AudioSource s in Sounds)
            {
                s.volume = PlayerPrefs.GetFloat("SoundVolume");
            }
        }

        void GetMusicVolume() => Music.volume = PlayerPrefs.GetFloat("MusicVolume");


    }
}

