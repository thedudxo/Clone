using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSlider : MonoBehaviour {
        private AudioSource audioSrc;
        private float soundVolume;
        private float midVol;

        void Start()
        {
            audioSrc = GetComponent<AudioSource>();
        //set audiosource volume to start with at a balanced volume, and set the slider to the middle
            midVol = audioSrc.volume;
            soundVolume = midVol;
        }

        void Update()
        {
            // Setting volume option of Audio Source to be equal to soundVolume
            audioSrc.volume = soundVolume;
        }

        // Method that is called by slider game object
        // This method takes vol value on slider and sets it as soundValue
        public void SetVolume(float vol)
        {
            soundVolume = vol * midVol*2;
        }
    }
