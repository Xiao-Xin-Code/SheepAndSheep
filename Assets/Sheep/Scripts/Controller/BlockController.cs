using QMVC;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sheep
{
	public class BlockController : MonoController,IPointerClickHandler
	{
		[SerializeField] BlockView _view;
		BlockEntity _entity;

		#region ÊôÐÔ
		public bool interactable { get => _entity.Interactable.Value; set => _entity.Interactable.Value = value; }

		public string Theme { get; set; }
		public string Content { get; set; }

		public Vector2Int[] OccupiedCells { get; set; }
		#endregion


		private void InteractableChanged(bool isOn)
		{
			_view.MaskRenderer.enabled = !isOn;
		}


		public override void Init()
		{
			_entity = new BlockEntity();
			_entity.Interactable.RegisterWithInitValue(InteractableChanged);
		}

        public void OnPointerClick(PointerEventData eventData)
        {
			Debug.Log("µã»÷");
			if (interactable)
			{
				this.SendCommand(new RemoveInGridsCommand(this));
				this.SendCommand(new PlaceToContainerCommand(this));
			}
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

