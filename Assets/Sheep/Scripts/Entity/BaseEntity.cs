using QMVC;

namespace Sheep
{
    public class BaseEntity : IEntity
    {
        public IArchitecture GetArchitecture()
        {
            return Sheep.Interface;
        }
    }

}


