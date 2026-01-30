using DG.Tweening;
using QMVC;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sheep
{
    public class ContainerController : MonoController
    {
        List<BlockController> vessel = new List<BlockController>();
        List<BlockController> extendVessel = new List<BlockController>();

        [SerializeField] ContainerView _view;

        PoolSystem _poolSystem;
        LevelSystem _levelSystem;
        LevelModel _levelModel;
        AudioSystem _audioSystem;

        public override void Init()
        {
			_poolSystem = this.GetSystem<PoolSystem>();
            _levelSystem = this.GetSystem<LevelSystem>();
            _levelModel = this.GetModel<LevelModel>();
            _audioSystem = this.GetSystem<AudioSystem>();

			this.RegisterEvent<PlaceToContainerEvent>(PlaceToContainer);
            this.RegisterEvent<LevelResurrectionEvent>(LevelResurrection);
            this.RegisterEvent<ClearContainerEvent>(ClearContainer);
            this.RegisterEvent<RemoveInExtendEvent>(RemoveInExtend);
        }

        int placeCount = 0;
		private Dictionary<BlockController, Tweener> animMap = new Dictionary<BlockController, Tweener>();

        private void Place(BlockController block)
        {
            lock (vessel)
            {
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
                animMap.Add(block, tweener);

                placeCount++;
                tweener.onPlay = () =>
                {
                    int startIndex = sames.Count == 2 ? insertIndex - 2 : insertIndex + 1;
                    int targetIndex = insertIndex + 1;
                    for (int i = startIndex; i < vessel.Count; ++i)
                    {
                        DOTween.Kill(vessel[i].transform);
                        if (animMap.ContainsKey(vessel[i]))
                        {
                            placeCount--;
                            animMap.Remove(vessel[i]);
                        }
                        Tweener tempTweener = vessel[i].transform.DOMove(_view.Cells[targetIndex].position, 0.3f);
                        targetIndex++;
                    }
                };
                tweener.onComplete = () =>
                {
                    if (animMap.ContainsKey(block)) animMap.Remove(block);
                    placeCount--;
                    if (sames.Count == 2)
                    {
                        _audioSystem.PlaySFX("Eliminate");
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
                            if (animMap.ContainsKey(vessel[i]))
                            {
                                placeCount--;
                                animMap.Remove(vessel[i]);
                            }
                            int index = Mathf.Min(i, _view.Cells.Length - 1);
                            vessel[i].transform.DOMove(_view.Cells[index].position, 0.3f);
                        }
                    }

                    if (placeCount == 0)
                    {
                        if (vessel.Count == _view.Cells.Length)
                        {
                            _levelModel.levelState.Value = extendVessel.Count >= 3 ? LevelState.Failure : LevelState.FailureWithResurrection;
                        }
                        else
                        {
                            if (!_levelSystem.HasBlocks() && extendVessel.Count == 0) 
                            {
                                if (vessel.Count == 0)
                                {
                                    if (_levelModel.levelup)
                                    {
                                        _levelModel.levelState.Value = LevelState.Succeed;
                                    }
                                    else
                                    {
                                        _levelModel.levelup = true;
                                        this.SendCommand(new LaunchTransitionCommand(null, 2));
                                        _poolSystem.RecycleAllBlock();//回收使用的Block
                                        _levelSystem.ClearBlocks();//清空关卡中的Block
                                        placeCount = 0;
                                        vessel.Clear();//清空容器中的Block
                                        extendVessel.Clear();
                                        this.SendCommand<LaunchLevelCommand>();
                                        //启动新的
                                    }
                                }
                                else
                                {
                                    Debug.Log("失败");
                                    throw new Exception("其余数据已清空，但是vessel任然存在");
                                    //_levelModel.levelState.Value = LevelState.Failure;
                                }
                            }
                        }
                    }
                };
            }
        }

        private void PlaceToContainer(PlaceToContainerEvent evt)
        {
            Place(evt.block);
        }

        private void ClearContainer(ClearContainerEvent evt)
        {
            placeCount = 0;
            vessel.Clear();
            extendVessel.Clear();
		}


        private void LevelResurrection(LevelResurrectionEvent evt)
        {
            int needCount = 3 - extendVessel.Count;

            List<BlockController> extendBlocks = new List<BlockController>();

            //获取需要的block
            for(int i  = 7 - needCount; i < 7; i++)
            {
				extendBlocks.Add(vessel[i]);
            }
            //从vessel中移除
            for(int i = 0;i< extendBlocks.Count; i++)
            {
                vessel.Remove(extendBlocks[i]);
                extendVessel.Add(extendBlocks[i]);
            }
            //更新ExtendVessel的状态
            for(int i = 0; i < extendVessel.Count; i++)
            {
                extendVessel[i].interactable = true;
                extendVessel[i].transform.position = _view.ExtendCells[i].position;
            }
        }


        private void RemoveInExtend(RemoveInExtendEvent evt)
        {
            extendVessel.Remove(evt.block);
		}

	}

}