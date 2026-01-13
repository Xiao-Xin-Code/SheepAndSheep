using QMVC;

namespace Sheep
{
    public abstract class BaseController : IController
    {
        public IArchitecture GetArchitecture()
        {
            return Sheep.Interface;
        }
    }
}


