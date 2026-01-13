using QMVC;

namespace SheepSheep
{
    public class BaseController : IController
    {
        public IArchitecture GetArchitecture()
        {
            return SheepSheep.Interface;
        }

    }
}

