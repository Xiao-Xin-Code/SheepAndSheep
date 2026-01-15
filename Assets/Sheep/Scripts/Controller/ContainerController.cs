using System.Collections.Generic;
using DG.Tweening;
using QMVC;
using UnityEngine;

namespace Sheep
{
    public class ContainerController : MonoController
    {
        List<BlockController> vessel = new List<BlockController>();
        [SerializeField] Vector2[] cells = new Vector2[8];

        PoolSystem _poolSystem;
        LevelSystem _levelSystem;

        public override void Init()
        {
			_poolSystem = this.GetSystem<PoolSystem>();
            _levelSystem = this.GetSystem<LevelSystem>();
            this.RegisterEvent<PlaceToContainerEvent>(PlaceToContainer);

            float start = -3;
            for(int i = 0; i < 7; i++)
            {
                cells[i] = transform.TransformPoint(new Vector3(start + i, 0, 0));
			}
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

            if (vessel.Count + 1 >= cells.Length) 
            {
                //over
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

            int curIndex = Mathf.Min(insertIndex, cells.Length - 1);
            Tweener tweener = block.transform.DOMove(cells[curIndex], 2);
            tweener.onPlay = () =>
            {
                if(sames.Count == 2)
                {
                    for (int i = insertIndex - 2; i < vessel.Count; ++i) 
					{
						DOTween.Kill(vessel[i].transform);
						int index = Mathf.Min(insertIndex + i + 1, cells.Length - 1);
						vessel[i].transform.DOMove(cells[index], 1);
					}
				}
                else
                {
                    for(int i = insertIndex + 1; i < vessel.Count; ++i)
                    {
                        DOTween.Kill(vessel[i].transform);
                        vessel[i].transform.DOMove(cells[i], 1);
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
						int index = Mathf.Min(i, cells.Length - 1);
						vessel[i].transform.DOMove(cells[index], 1);
					}
				}

                if (!_levelSystem.HasBlocks()) 
                {
                    if (vessel.Count == 0)
                    {
                        //成功
                        Debug.Log("成功");
                    }
                    else
                    {
						//失败
						Debug.Log("失败");
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