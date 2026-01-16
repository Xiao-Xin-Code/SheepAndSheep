using UnityEngine;

namespace Sheep
{
	public class SFXView : BaseView
	{
		[SerializeField] private AudioSource audioSource;

		public AudioSource AudioSource => audioSource;
	}

}



