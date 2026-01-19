using QMVC;
using UnityEngine;


namespace Sheep
{
	public class BlockEntity : BaseEntity
	{
		public BindableProperty<bool> Interactable = new BindableProperty<bool>(true);
		public string theme;
		public string content;

		public Vector2Int[] occupiedCells;

		public int ID { get; set; }
	}
}


