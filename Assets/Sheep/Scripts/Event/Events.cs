using System;

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


	public class JoinEvent
	{

	}


	public class LaunchTransitionEvent
	{
		/// <summary>
		/// 覆盖完成
		/// </summary>
		public event Action overEvent;

		public LaunchTransitionEvent(Action overEvent)
		{
			this.overEvent = overEvent;
		}

		public void Trigger() => overEvent?.Invoke();
	}

	public class RemoveInGridsEvent
	{
		public BlockController block;

		public RemoveInGridsEvent(BlockController block)
		{
			this.block = block;
		}
	}

}

