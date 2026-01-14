namespace Sheep
{
	/// <summary>
	/// 放置到容器中
	/// </summary>
	public class PlaceToContainerEvent
	{
		public BlockController block;

		public PlaceToContainerEvent(BlockController block)
		{
			this.block = block;
		}
	}

	/// <summary>
	/// 初始化关卡
	/// </summary>
	public class InitLevelEvent
	{

	}

	/// <summary>
	/// 展开菜单
	/// </summary>
	public class UnFoldMenuEvent
	{

	}

}

