using UnityEngine;

namespace Sheep
{
	public class TransitionView : BaseView
	{
		[SerializeField] RectTransform rectTransform;

		public RectTransform RectTransform => rectTransform;
	}
}


