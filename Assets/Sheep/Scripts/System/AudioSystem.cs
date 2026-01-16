using System.Collections;
using Frame;
using QMVC;
using UnityEngine;

namespace Sheep
{


    public class AudioSystem : AbstractSystem
    {
        PoolSystem _poolSystem;

        AudioSource _bgmSource;
        float _bgmVolume;
        float _bgmFadeTime;
        private Coroutine _bgmFadCoroutine;

        protected override void OnInit()
        {
            _poolSystem = this.GetSystem<PoolSystem>();


            //_poolSystem.GetBGM().
        }




        public void PlaySFX(AudioClip clip, float volume = 1f, bool isLoop = false)
        {
            if (clip == null) return;
            _poolSystem.GetSFX();
        }


        #region BGM

        public void PlayBGM(AudioClip clip,bool isLoop = true)
        {
            if (clip == null)
            {
                return;
            }

            if (_bgmFadCoroutine != null)
            {
                MonoService.Instance.StopCoroutine(_bgmFadCoroutine);
            }

            if (_bgmSource.isPlaying)
            {
                _bgmFadCoroutine = MonoService.Instance.StartCoroutine(FadeOutAndSwitchBGM(clip, isLoop));
            }
            else
            {
                _bgmSource.clip = clip;
                _bgmSource.loop = isLoop;
                _bgmSource.volume = 0;
                _bgmSource.Play();
                _bgmFadCoroutine = MonoService.Instance.StartCoroutine(FadeInBGM());
            }
        }

        public void StopBGM()
        {
            if (_bgmFadCoroutine != null)
            {
                MonoService.Instance.StopCoroutine(_bgmFadCoroutine);
            }
            _bgmFadCoroutine = MonoService.Instance.StartCoroutine(FadeOutBGM());

        }

        #endregion

        /// <summary>
        /// 淡入BGM
        /// </summary>
        /// <returns></returns>
        private IEnumerator FadeInBGM()
        {
            float currentVolume = 0;
            while(currentVolume< _bgmVolume)
            {
                currentVolume += Time.deltaTime / _bgmFadeTime;
                currentVolume = Mathf.Min(currentVolume, _bgmVolume);
                _bgmSource.volume = currentVolume;
                yield return null;
            }
            _bgmFadCoroutine = null;
        }

        /// <summary>
        /// 淡出BGM
        /// </summary>
        /// <returns></returns>
        private IEnumerator FadeOutBGM()
        {
            float currentVolume = _bgmSource.volume;
            while (currentVolume > 0)
            {
                currentVolume -= Time.deltaTime / _bgmFadeTime;
                currentVolume = Mathf.Max(currentVolume, 0);
                _bgmSource.volume = currentVolume;
                yield return null;
            }
            _bgmSource.Stop();
            _bgmSource.clip = null;
            _bgmFadCoroutine = null;

        }

		/// <summary>
		/// 淡出当前BGM并切换新BGM
		/// </summary>
		private IEnumerator FadeOutAndSwitchBGM(AudioClip newClip, bool isLoop)
		{
			yield return FadeOutBGM();
			_bgmSource.clip = newClip;
			_bgmSource.loop = isLoop;
			_bgmSource.volume = 0;
			_bgmSource.Play();
			yield return FadeInBGM();
		}



        public void PlaySFX(AudioClip clip, float volume = 1f)
        {
            if (clip == null) return;
            SFXController sfx = _poolSystem.GetSFX();
            sfx.Play(clip, volume,AudioSourceCompleted);
        }



		private void AudioSourceCompleted(SFXController sfx)
        {
            _poolSystem.RecycleSFX(sfx);
        }
    }

}


