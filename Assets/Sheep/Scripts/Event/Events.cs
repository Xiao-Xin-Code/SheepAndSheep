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

	/// <summary>
	/// 触发过渡
	/// </summary>
	public class LaunchTransitionEvent
	{
		/// <summary>
		/// 覆盖完成
		/// </summary>
		public event Action overEvent;

		public int state;

		public LaunchTransitionEvent(Action overEvent, int state)
		{
			this.overEvent = overEvent;
			this.state = state;
		}

		public void Trigger() => overEvent?.Invoke();
	}

	public class LaunchLevelEvent
	{

	}

	public class RemoveInGridsEvent
	{
		public BlockController block;

		public RemoveInGridsEvent(BlockController block)
		{
			this.block = block;
		}
	}

	public class RemoveInExtendEvent
	{
		public BlockController block;

		public RemoveInExtendEvent(BlockController block)
		{
			this.block = block;
		}
	}


	public class GameOverEvent
	{
		public bool canResurrection;

		public GameOverEvent(bool canResurrection)
		{
			this.canResurrection = canResurrection;
		}
	}

	public class GameSucceedEvent
	{

	}

	#region Visible

	/// <summary>
	/// Home显示控制
	/// </summary>
	public class HomeViewVisibleEvent
	{
		public bool visible;

		public HomeViewVisibleEvent(bool visible)
		{
			this.visible = visible;
		}
	}
	/// <summary>
	/// 遮挡 显示控制
	/// </summary>
	public class MaskVisibleEvent
	{
		public bool visible;

		public MaskVisibleEvent(bool visible)
		{
			this.visible = visible;
		}
	}
	/// <summary>
	/// 关卡 显示控制
	/// </summary>
	public class LevelVisibleEvent
	{
		public bool visible;

		public LevelVisibleEvent(bool visible)
		{
			this.visible = visible;
		}
	}


	public class LevelSetVisibleEvent
	{
		public bool visible;


		public LevelSetVisibleEvent(bool visible)
		{
			this.visible = visible;
		}

	}

	#endregion

	public class ClearContainerEvent
	{
		
	}


	public class LevelResurrectionEvent
	{

	}

}

