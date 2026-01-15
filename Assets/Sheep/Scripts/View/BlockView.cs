
using UnityEngine;

namespace Sheep
{

	public class BlockView : BaseView
	{
		[SerializeField] SpriteRenderer bgRenderer;
		[SerializeField] SpriteRenderer iconRenderer;
		[SerializeField] SpriteRenderer maskRenderer;

		public SpriteRenderer BgRenderer => bgRenderer;
		public SpriteRenderer IconRenderer => iconRenderer;
		public SpriteRenderer MaskRenderer => maskRenderer;
	}

}


