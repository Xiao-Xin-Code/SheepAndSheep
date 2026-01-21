using System.Collections.Generic;
using DG.Tweening;
using QMVC;
using UnityEngine;

namespace Sheep
{
    public class ContainerController : MonoController
    {
        List<BlockController> vessel = new List<BlockController>();

        [SerializeField] ContainerView _view;

        PoolSystem _poolSystem;
        LevelSystem _levelSystem;
        LevelModel _levelModel;

        public override void Init()
        {
			_poolSystem = this.GetSystem<PoolSystem>();
            _levelSystem = this.GetSystem<LevelSystem>();
            _levelModel = this.GetModel<LevelModel>();

			this.RegisterEvent<PlaceToContainerEvent>(PlaceToContainer);
            this.RegisterEvent<ClearContainerEvent>(ClearContainer);
        }

        int placeCount = 0;

        private void Place(BlockController block)
        {
            Debug.Log("放置Block" + block.content);

            //容量测试
            if (vessel.Count >= _view.Cells.Length)
            {
                return;
            }

			List<BlockController> vesselSnapshot = new List<BlockController>(vessel);

			int last = -1;
			List<BlockController> sames = new List<BlockController>();
			for (int i = 0; i < vesselSnapshot.Count; ++i)
			{
				if (vessel[i].TypeEquals(block))
				{
					sames.Add(vessel[i]);
					last = i;
				}
			}
			int insertIndex = sames.Count > 0 ? last + 1 : vessel.Count;

			//更新数据
			if (sames.Count == 2)
			{
				foreach (var item in sames)
				{
					vessel.Remove(item);
				}
			}
			else
			{
				vessel.Insert(insertIndex, block);
			}

            //动画效果
            Tweener tweener = block.transform.DOMove(_view.Cells[insertIndex].position, 0.5f);
            tweener.onPlay = () =>
            {
				placeCount++;
				int startIndex = sames.Count == 2 ? insertIndex - 2 : insertIndex + 1;
                int targetIndex = insertIndex + 1;
				for (int i = startIndex; i < vessel.Count; ++i)
				{
					DOTween.Kill(vessel[i].transform);
					vessel[i].transform.DOMove(_view.Cells[targetIndex].position, 1);
                    targetIndex++;
				}
            };
            tweener.onComplete = () =>
            {
				if (sames.Count == 2)
                {
                    foreach (var item in sames) 
                    {
                        //回收
                        _poolSystem.RecycleBlock(item);
					}
                    //回收
                    _poolSystem.RecycleBlock(block);
                    sames.Clear();

					for (int i = insertIndex - 2; i < vessel.Count; i++)
					{
						DOTween.Kill(vessel[i].transform);
						int index = Mathf.Min(i, _view.Cells.Length - 1);
						vessel[i].transform.DOMove(_view.Cells[index].position, 1);
					}
				}
				placeCount--;
				if (placeCount == 0)
                {
                    if(vessel.Count == _view.Cells.Length)
                    {
						Debug.Log("失败");
						_levelModel.levelState.Value = LevelState.Failure;
					}
                    else
                    {
                        if (!_levelSystem.HasBlocks())
                        {
							if (vessel.Count == 0)
                            {
								Debug.Log("成功");
								if (_levelModel.levelup)
								{
									_levelModel.levelState.Value = LevelState.Succeed;
								}
								else
								{
									_levelModel.levelup = true;
									Debug.Log("启动新的");
									this.SendCommand(new LaunchTransitionCommand(null, 2));
									_poolSystem.RecycleAllBlock();//回收使用的Block
									_levelSystem.ClearBlocks();//清空关卡中的Block
                                    placeCount = 0;
									vessel.Clear();//清空容器中的Block
									this.SendCommand<LaunchLevelCommand>();
									//启动新的
								}
							}
                            else
                            {
								Debug.Log("失败");
								_levelModel.levelState.Value = LevelState.Failure;
							}
						}
                    }
                }
            };
        }

        private void PlaceToContainer(PlaceToContainerEvent evt)
        {
            Place(evt.block);
        }

        private void ClearContainer(ClearContainerEvent evt)
        {
            placeCount = 0;
            vessel.Clear();
		}
    }
}