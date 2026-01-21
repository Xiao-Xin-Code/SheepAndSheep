using System;
using QMVC;

namespace Sheep
{
    /// <summary>
    /// 初始化Level
    /// </summary>
    public class InitLevelCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.SendEvent<InitLevelEvent>();
        }
    }

    /// <summary>
    /// 展开菜单
    /// </summary>
    public class UnFoldMenuCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.SendEvent<UnFoldMenuEvent>();
        }
    }

    /// <summary>
    /// 加入
    /// </summary>
    public class JoinCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.SendEvent<JoinEvent>();
        }
    }

    /// <summary>
    /// 过渡
    /// </summary>
    public class LaunchTransitionCommand : AbstractCommand
    {
        private event Action overEvent;
        private int state;

        public LaunchTransitionCommand(Action overEvent, int state)
        {
            this.overEvent = overEvent;
            this.state = state;
        }

        protected override void OnExecute()
        {
            this.SendEvent(new LaunchTransitionEvent(overEvent, state));
        }
    }

    public class LaunchLevelCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.SendEvent<LaunchLevelEvent>();
        }
    }


    public class BlockClickCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            
        }
    }

    public class PlaceToContainerCommand : AbstractCommand
    {
        public BlockController block;

        public PlaceToContainerCommand(BlockController block)
        {
            this.block = block;
        }

        protected override void OnExecute()
        {
            this.SendEvent(new PlaceToContainerEvent(block));
        }
    }

    public class RemoveInGridsCommand : AbstractCommand
    {
        BlockController block;

        public RemoveInGridsCommand(BlockController block)
        {
            this.block = block;
        }

        protected override void OnExecute()
        {
            this.SendEvent(new RemoveInGridsEvent(block));
        }
    }

    public class GameOverCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.SendEvent<GameOverEvent>();
        }
    }

    public class GameSucceedCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.SendEvent<GameSucceedEvent>();
        }
    }


    public class HomeViewVisibleCommand : AbstractCommand
    {
        bool visible;

        public HomeViewVisibleCommand(bool visible)
        {
            this.visible = visible;
        }

        protected override void OnExecute()
        {
            this.SendEvent(new HomeViewVisibleEvent(visible));
        }
    }


    public class ClearContainerCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.SendEvent<ClearContainerEvent>();
        }
    }
}


