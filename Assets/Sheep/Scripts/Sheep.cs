using QMVC;
using UnityEngine;

namespace Sheep
{
	public class Sheep : Architecture<Sheep>
	{
		GridsController gridsController;

		protected override void Init()
		{
			RegisterModel<LevelModel>(new LevelModel());
			RegisterSystem<PoolSystem>(new PoolSystem());
			RegisterSystem<LevelSystem>(new LevelSystem());

			gridsController = new GridsController();
		}
	}
}

