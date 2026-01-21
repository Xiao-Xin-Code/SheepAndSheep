using QMVC;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sheep
{
	public class BlockController : MonoController,IPointerClickHandler
	{
		[SerializeField] BlockView _view;
		BlockEntity _entity;
		AssetSystem _assetSystem;
		AudioSystem _audioSystem;

		#region ÊôÐÔ
		public bool interactable { get => _entity.Interactable.Value; set => _entity.Interactable.Value = value; }

		public string theme { get => _entity.theme; set => _entity.theme = value; }

		public string content { get => _entity.content; set => _entity.content = value; }

		public Vector2Int[] OccupiedCells { get => _entity.occupiedCells; set => _entity.occupiedCells = value; }

		public int ID { get => _entity.ID; }
		#endregion


		private void InteractableChanged(bool isOn)
		{
			_view.MaskRenderer.enabled = !isOn;
		}

		public void UpdateIcon()
		{
			_view.BgRenderer.sprite = _assetSystem.BgSprite;
			_view.MaskRenderer.sprite = _assetSystem.MaskSprite;
			_view.IconRenderer.sprite = _assetSystem.GetIcon(_entity.content);
		}


		public override void Init()
		{
			_entity = new BlockEntity();
			_entity.Interactable.RegisterWithInitValue(InteractableChanged);
			_assetSystem = this.GetSystem<AssetSystem>();
			_audioSystem = this.GetSystem<AudioSystem>();
		}

        public void OnPointerClick(PointerEventData eventData)
        {
			Debug.Log("µã»÷");
			if (interactable)
			{
				_entity.Interactable.SetValueWithoutEvent(false);
				this.SendCommand(new RemoveInGridsCommand(this));
				_audioSystem.PlaySFX("Click");
				this.SendCommand(new PlaceToContainerCommand(this));
			}
        }
    }

	public static class BlockControllerExtension
	{
		public static bool TypeEquals(this BlockController self, BlockController other)
		{
			return self.theme == other.theme && self.content == other.content;
		}
	}
}

