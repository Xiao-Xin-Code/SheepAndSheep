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
        DataModel _dataModel;

        public override void Init()
        {
			_poolSystem = this.GetSystem<PoolSystem>();
            _levelSystem = this.GetSystem<LevelSystem>();
            _levelModel = this.GetModel<LevelModel>();
            _dataModel = this.GetModel<DataModel>();

			this.RegisterEvent<PlaceToContainerEvent>(PlaceToContainer);
        }

        private void Place(BlockController block)
        {
            int last = -1;
            List<BlockController> sames = new List<BlockController>();
            for (int i = 0; i < vessel.Count; ++i) 
            {
                if (vessel[i].TypeEquals(block))
                {
                    sames.Add(vessel[i]);
                    last = i;
                }
            }
            int insertIndex = sames.Count > 0 ? last + 1 : vessel.Count;

            if (vessel.Count + 1 >= _view.Cells.Length) 
            {
                //over
                _levelModel.levelState.Value = LevelState.Failure;
                return;
            }
            else
            {
                if(sames.Count == 2)
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
            }

            int curIndex = Mathf.Min(insertIndex, _view.Cells.Length - 1);
            Tweener tweener = block.transform.DOMove(_view.Cells[curIndex].position, 2);
            tweener.onPlay = () =>
            {
                if(sames.Count == 2)
                {
                    for (int i = insertIndex - 2; i < vessel.Count; ++i) 
					{
						DOTween.Kill(vessel[i].transform);
						int index = Mathf.Min(insertIndex + i + 1, _view.Cells.Length - 1);
						vessel[i].transform.DOMove(_view.Cells[index].position, 1);
					}
				}
                else
                {
                    for(int i = insertIndex + 1; i < vessel.Count; ++i)
                    {
                        DOTween.Kill(vessel[i].transform);
                        vessel[i].transform.DOMove(_view.Cells[i].position, 1);
                    }
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

                if(DOTween.TotalPlayingTweens() == 0)
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
                            if(vessel.Count == 0)
                            {
								Debug.Log("成功");
                                if (_levelModel.levelup)
                                {
									_levelModel.levelState.Value = LevelState.Succeed;
								}
                                else
                                {
                                    _levelModel.levelup = true;
                                    this.SendCommand(new LaunchTransitionCommand(null, 2));
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
    }
}