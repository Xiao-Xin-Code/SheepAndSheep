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

        public LaunchTransitionCommand(Action overEvent)
        {
            this.overEvent = overEvent;
        }

        protected override void OnExecute()
        {
            this.SendEvent(new LaunchTransitionEvent(overEvent));
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
}


