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


	public class GameOverEvent
	{

	}

	public class GameSucceedEvent
	{

	}


	public class HomeViewVisibleEvent
	{
		public bool visible;

		public HomeViewVisibleEvent(bool visible)
		{
			this.visible = visible;
		}
	}

    public class MaskVisibleEvent
    {
        public bool visible;

        public MaskVisibleEvent(bool visible)
        {
            this.visible = visible;
        }
    }

	public class ClearContainerEvent
	{
		
	}


	public class LevelVisibleEvent
	{
		public bool visible;

		public LevelVisibleEvent(bool visible)
		{
			this.visible = visible;
		}
	}

}

