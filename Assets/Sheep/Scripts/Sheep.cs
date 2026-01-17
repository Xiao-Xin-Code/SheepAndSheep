using QMVC;

namespace Sheep
{
	public class Sheep : Architecture<Sheep>
	{
		protected override void Init()
		{
			RegisterModel<DataModel>(new DataModel());
			RegisterSystem<AssetSystem>(new AssetSystem());
			RegisterSystem<PoolSystem>(new PoolSystem());
			RegisterSystem<AudioSystem>(new AudioSystem());
			
			RegisterModel<LevelModel>(new LevelModel());
			RegisterSystem<LevelSystem>(new LevelSystem());
		}
	}
}

