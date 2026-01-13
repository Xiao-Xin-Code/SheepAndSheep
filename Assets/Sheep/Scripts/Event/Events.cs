namespace Sheep
{
	public class PlaceToContainerEvent
	{
		public BlockController block;

		public PlaceToContainerEvent(BlockController block)
		{
			this.block = block;
		}
	}


	public class InitLevelEvent
	{

	}

}

