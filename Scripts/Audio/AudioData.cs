using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ZentySpeede.Audio
{
	public class AudioData : MonoBehaviour
	{

		[SerializeField] AudioSource audioSource;
		[SerializeField] int sampleDataLength = 1024;

		[SerializeField] float clipLoudness;
		[SerializeField] float[] clipSampleData;
		[SerializeField] float sizeFactor;

		[SerializeField] float minLoudness = 0;
		[SerializeField] float maxLoudness = 500;
		[SerializeField] float updateStep = 0.1f;
		float currentUpdateTime;

        public float ClipLoudness { get => clipLoudness; set => clipLoudness = value; }


        // Use this for initialization
        private void Awake()
		{
			clipSampleData = new float[sampleDataLength];
		}
		private void FillSampleData()
        {
			audioSource.clip.GetData(clipSampleData, audioSource.timeSamples);
		}

        private void Update()
        {
			GetCurrentLoudness();
		}
        public void GetCurrentLoudness()
		{
			currentUpdateTime += Time.deltaTime;
			if (currentUpdateTime >= updateStep)
			{
				currentUpdateTime = 0f;
				audioSource.clip.GetData(clipSampleData, audioSource.timeSamples); //I read 1024 samples, which is about 80 ms on a 44khz stereo clip, beginning at the current sample position of the clip.
				clipLoudness = 0f;
				foreach (var sample in clipSampleData)
				{
					clipLoudness += Mathf.Abs(sample);
				}
				clipLoudness /= sampleDataLength;
				clipLoudness *= sizeFactor;
			}
		}

		public void GetClipPeak()
		{

		}


		
	}
}

