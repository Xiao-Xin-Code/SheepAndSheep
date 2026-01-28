using QMVC;
using UnityEngine;


namespace Sheep
{
	public class SFXController : MonoController
	{
        static PoolSystem _poolSystem;

        [SerializeField] SFXView _view;

        public override void Init()
        {
            if(_poolSystem== null)
            {
                _poolSystem = this.GetSystem<PoolSystem>();
            }
        }


		public void Play(AudioClip clip, float volume)
        {
            Stop();

			_view.AudioSource.clip = clip;
			_view.AudioSource.volume = volume;
			_view.AudioSource.Play();
            Invoke(nameof(OnAudioComplete), clip.length);
		}

        public void Stop()
        {
            _view.AudioSource.Stop();
            CancelInvoke(nameof(OnAudioComplete));
        }

        private void OnAudioComplete()
        {
            _poolSystem.RecycleSFX(this);
        }
	}
}


