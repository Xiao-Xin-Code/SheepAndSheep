using DG.Tweening;
using QFramework;
using System.Collections.Generic;
using UnityEngine;

namespace SheepSheep
{
    public class VesselController : MonoController
    {
        public Transform[] cells = new Transform[8];

        public List<BlockController> vessel = new List<BlockController>();

        PoolSystem poolSystem;
        string s;

        protected override void Init()
        {
            poolSystem = this.GetSystem<PoolSystem>();
            this.RegisterEvent<BlockClickEvent>(PlaceEvent);
        }


        public void Place(BlockController block)
        {
            int last = -1;

            List<BlockController> sames = new List<BlockController>();

            for (int i = 0; i < vessel.Count; i++)
            {
                if (vessel[i].TypeEquals(block))
                {
                    sames.Add(vessel[i]);
                    last = i;
                }
            }

            int insertIndex = sames.Count > 0 ? last + 1 : vessel.Count;

            if (vessel.Count + 1 > cells.Length)
            {
                //over;
            }
            else
            {
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
            }

            int curIndex = Mathf.Min(insertIndex, cells.Length - 1);
            Tweener tweener = block.transform.DOMove(cells[curIndex].position, 2);
            tweener.onPlay = () =>
            {
                if (sames.Count == 2)
                {
                    for (int i = insertIndex - 2; i < vessel.Count; i++)
                    {
                        DOTween.Kill(vessel[i].transform);
                        int index = Mathf.Min(insertIndex + i + 1, cells.Length - 1);
                        vessel[i].transform.DOMove(cells[index].position, 1);
                    }
                }
                else
                {
                    for (int i = insertIndex + 1; i < vessel.Count; i++)
                    {
                        DOTween.Kill(vessel[i].transform);
                        int index = Mathf.Min(i, cells.Length - 1);
                        vessel[i].transform.DOMove(cells[index].position, 1);
                    }
                }

            };
            tweener.onComplete = () =>
            {
                if (sames.Count == 2)
                {
                    foreach (var item in sames)
                    {
                        poolSystem.RecycleBlock(item);
                    }
                    poolSystem.RecycleBlock(block);
                    sames.Clear();

                    for (int i = insertIndex - 2; i < vessel.Count; i++)
                    {
                        DOTween.Kill(vessel[i].transform);
                        int index = Mathf.Min(i, cells.Length - 1);
                        vessel[i].transform.DOMove(cells[index].position, 1);
                    }

                }
            };
        }


        private void PlaceEvent(BlockClickEvent evt)
        {
            Place(evt.block);
        }
    }
}

