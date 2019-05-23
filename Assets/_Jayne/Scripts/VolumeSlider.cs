using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSlider : MonoBehaviour {
        private AudioSource audioSrc;
        private float currentVolume;
        private float startVol;

        void Start()
        {
            audioSrc = GetComponent<AudioSource>();
        //set audiosource volume to start with at a balanced volume, and set the slider to the middle
            startVol = audioSrc.volume;
            currentVolume = startVol;
        }

        void Update()
        {
            // Setting volume option of Audio Source to be equal to currentVolume
            audioSrc.volume = currentVolume;
        }

        // Method that is called by slider game object
        // This method takes volSliderValue on slider and sets it as soundValue
        public void SetVolume(float vol)
        {
            currentVolume = vol;
        }
    }
