using QFramework;
using System.Collections.Generic;
using UnityEngine;

namespace SheepSheep
{
    public class AudioSystem : AbstractSystem
    {
        Dictionary<string, AudioClip> musicClipMaps = new Dictionary<string, AudioClip>();
        Dictionary<string, AudioClip> uiClipMaps = new Dictionary<string, AudioClip>();
        Dictionary<string, AudioClip> sfxClipMaps = new Dictionary<string, AudioClip>();


        AudioSource musicSource;
        AudioSource uiSource;



        PoolSystem poolSystem;


        protected override void OnInit()
        {
            Transform audioManager = new GameObject("AudioManager").transform;
            poolSystem = this.GetSystem<PoolSystem>();
            musicSource = new GameObject("MusicSource").AddComponent<AudioSource>();
            musicSource.transform.SetParent(audioManager);
            musicSource.loop = true;
            uiSource = new GameObject("UISource").AddComponent<AudioSource>();
            uiSource.transform.SetParent(audioManager);
        }


        public void Play(string audioName, float pithMultiplier = 1f)
        {

        }

        private void PlayMusic(AudioClip audioClip)
        {
            musicSource.clip = audioClip;
            musicSource.Play();
        }

        private void PlayUI(AudioClip audioClip)
        {
            uiSource.clip = audioClip;
            uiSource.Play();
        }

        private void PlaySFX(AudioClip audioClip)
        {

        }
    }
}

