using UnityEngine;

namespace Sheep
{
	public class TransitionView : BaseView
	{
		[SerializeField] RectTransform rectTransform;
		[SerializeField] GameObject sheepGroup;
		[SerializeField] GameObject levelUp;

		public RectTransform RectTransform => rectTransform;
		public GameObject SheepGroup => sheepGroup;
		public GameObject LevelUp => levelUp;
	}
}


