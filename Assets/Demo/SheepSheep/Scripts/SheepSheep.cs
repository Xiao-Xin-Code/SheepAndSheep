using QFramework;

namespace SheepSheep
{
    public class SheepSheep : Architecture<SheepSheep>
    {
        protected override void Init()
        {
            RegisterSystem(new PoolSystem());
            RegisterSystem(new GridsManagerSystem());
            RegisterSystem(new VesselSystem());
            RegisterSystem(new AudioSystem());
        }
    }
}


