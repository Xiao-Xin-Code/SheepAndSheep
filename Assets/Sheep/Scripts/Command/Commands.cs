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
}


