using QMVC;


namespace Sheep
{
	public class BlockEntity : BaseEntity
	{
		public BindableProperty<bool> Interactable = new BindableProperty<bool>(true);
		public string theme;
		public string content;


		public int ID { get; set; }
	}
}


