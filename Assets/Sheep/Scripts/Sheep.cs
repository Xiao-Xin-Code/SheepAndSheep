using QMVC;

namespace Sheep
{
	public class Sheep : Architecture<Sheep>
	{
		protected override void Init()
		{
			RegisterModel<DataModel>(new DataModel());
			RegisterSystem<AssetSystem>(new AssetSystem());
			RegisterModel<LevelModel>(new LevelModel());
			RegisterSystem<PoolSystem>(new PoolSystem());
			RegisterSystem<AudioSystem>(new AudioSystem());
			RegisterSystem<LevelSystem>(new LevelSystem());
		}
	}
}

