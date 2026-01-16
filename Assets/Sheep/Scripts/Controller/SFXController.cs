using System;
using System.Collections;
using UnityEngine;


namespace Sheep
{
	public class SFXController : MonoController
	{
        private static WaitForSeconds _waitForInterval = new WaitForSeconds(0.05f);
        private Action<SFXController> _onComplete;
        private static IEnumerator _audioCompleteCoroutine;

        [SerializeField] SFXView _view;

        public override void Init()
        {
            if(_audioCompleteCoroutine == null)
            {
                _audioCompleteCoroutine = AudioCompleteCoroutine();
            }
        }



        public void Play(AudioClip clip, float volume, Action<SFXController> onComplete)
        {
			_view.AudioSource.clip = clip;
			_view.AudioSource.volume = volume;

			_view.AudioSource.Play();

			StopCoroutine(_audioCompleteCoroutine);
			StartCoroutine(_audioCompleteCoroutine);
		}


        private IEnumerator AudioCompleteCoroutine()
        {
			while (!_view.AudioSource.isPlaying)
			{
				yield return null;
			}

			while (_view.AudioSource.isPlaying)
            {
                yield return null;
            }

            //µÈ´ý¼ä¸ô
            yield return _waitForInterval;

            _onComplete?.Invoke(this);
            _onComplete = null;
        }
	}
}


