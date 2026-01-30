using UnityEngine;
using UnityEngine.UI;

namespace Sheep
{

	public class BlockView : BaseView
	{
		[SerializeField] Image bgImage;
		[SerializeField] Image iconImage;
		[SerializeField] Image maskImage;
		[SerializeField] RectTransform rectTransform;


		public Image BgImage => bgImage;
		public Image IconImage => iconImage;
		public Image MaskImage => maskImage;

		public RectTransform RectTransform => rectTransform;
	}

}


