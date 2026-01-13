using UnityEngine;
using UnityEngine.EventSystems;

namespace Sheep
{
	public class BlockController : MonoController,IPointerClickHandler
	{
		#region UI
		[SerializeField] SpriteRenderer bgRenderer;
		[SerializeField] SpriteRenderer iconRenderer;
		[SerializeField] SpriteRenderer maskRenderer;
		#endregion

		#region  Ù–‘
		private bool interactable = true;

		public bool Interactable
		{
			get
			{
				return interactable;
			}
			set
			{
				interactable = value;
				maskRenderer.enabled = !value;
			}
		}

		public string Theme { get; set; }
		public string Content { get; set; }

		public Vector2Int[] OccupiedCells { get; set; }
		#endregion

		public override void Init()
		{
			
		}

        public void OnPointerClick(PointerEventData eventData)
        {
            
        }
    }

	public static class BlockControllerExtension
	{
		public static bool TypeEquals(this BlockController self, BlockController other)
		{
			return self.Theme == other.Theme && self.Content == other.Content;
		}
	}
}

