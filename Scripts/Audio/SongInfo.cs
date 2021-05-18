using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZentySpeede.Audio
{
    public class SongInfo : MonoBehaviour
    {
        [SerializeField] float bpm;
        [SerializeField] List<float> notes1;
        [SerializeField] List<float> notes2;
        [SerializeField] List<float> notes3;
        [SerializeField] List<float> notes4;
        [SerializeField] List<float> notes5;
        [SerializeField] List<float> notes6;
        [SerializeField] int nextNote;
    }

}