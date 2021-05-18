using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZentySpeede.Audio
{
    public class Conductor : MonoBehaviour
    {
        [SerializeField] float songBPM;
        [SerializeField] float secPerBeat;
        [SerializeField] float songPosition;
        [SerializeField] float songPositionInBeats;
        [SerializeField] float dspSongTime;
        [SerializeField] AudioSource audioSource;

        public float SongPositionInBeats { get => songPositionInBeats; set => songPositionInBeats = value; }

        private void Awake()
        {
            TryGetComponent(out audioSource);
        }
        private void Start()
        {
            SetDsp();
            SetSecPerBeat();
        }

        void Update()
        {
            SetSongPos();
            SongPosInBeats();
        }
        
        private void SetDsp() => dspSongTime = (float)AudioSettings.dspTime;
        private void SetSecPerBeat() => secPerBeat = 60f / songBPM;
        private void SetSongPos() => songPosition = (float)(AudioSettings.dspTime - dspSongTime);
        private void SongPosInBeats() => songPositionInBeats = songPosition / secPerBeat;
    
    }
}

