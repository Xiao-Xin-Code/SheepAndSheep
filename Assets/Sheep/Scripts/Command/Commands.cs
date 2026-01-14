using QMVC;

namespace Sheep
{
    public class InitLevelCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.SendEvent<InitLevelEvent>();
        }
    }

    public class UnFoldMenuCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.SendEvent<UnFoldMenuEvent>();
        }
    }

    public class JoinCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            
        }
    }
}


