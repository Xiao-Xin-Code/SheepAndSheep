using System.Collections;
using Frame;
using QMVC;
using UnityEngine;

namespace Sheep
{


    public class AudioSystem : AbstractSystem
    {
        PoolSystem _poolSystem;
        AssetSystem _assetSystem;
        DataModel _dataModel;

        AudioSource _bgmSource;
        float _bgmVolume = 1;
        float _bgmFadeTime = 0.5f;
        private Coroutine _bgmFadCoroutine;

        protected override void OnInit()
        {
            _poolSystem = this.GetSystem<PoolSystem>();
            _assetSystem = this.GetSystem<AssetSystem>();
            _dataModel = this.GetModel<DataModel>();
			_bgmSource = new GameObject("BGM").AddComponent<AudioSource>();

            //_poolSystem.GetBGM().
        }



		#region BGM

        public void LaunchBGM()
        {
            _bgmSource.Play();
        }

		public void PlayBGM(string clip, bool isLoop = true)
        {
            AudioClip audioClip = _assetSystem.GetBGM(clip);
            PlayBGM(audioClip, isLoop);
		}

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
                if (_dataModel.MusicIsOn.Value)
                {
					_bgmSource.Play();
					_bgmFadCoroutine = MonoService.Instance.StartCoroutine(FadeInBGM());
				}
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


		public void PlaySFX(string clip, float volume = 1f)
		{
            if (_dataModel.SfxIsOn.Value)
            {
				AudioClip audioClip = _assetSystem.GetSFX(clip);
				PlaySFX(audioClip, volume);
			}
		}

		public void PlaySFX(AudioClip clip, float volume = 1f)
        {
            if (clip == null) return;
            SFXController sfx = _poolSystem.GetSFX();
            sfx.Play(clip, volume);
        }
    }

}


