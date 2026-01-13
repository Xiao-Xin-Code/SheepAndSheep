using DG.Tweening;
using QMVC;
using SheepSheep;
using System.Collections.Generic;
using UnityEngine;

public class VesselSystem : AbstractSystem
{
    public Vector3[] cells = new Vector3[8];

    public List<BlockController> vessel = new List<BlockController>();

    PoolSystem poolSystem;

    protected override void OnInit()
    {
        poolSystem = this.GetSystem<PoolSystem>();

        float startX = -8 / 2 * 1.2f + 1.2f;
        for (int i = 0; i < cells.Length; i++)
        {
            float cur = startX + 1.2f * i;
            cells[i] = new Vector3(cur, -8, 0);
        }
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
        Tweener tweener = block.transform.DOMove(cells[curIndex], 2);
        tweener.onPlay = () =>
        {
            if (sames.Count == 2)
            {
                for (int i = insertIndex - 2; i < vessel.Count; i++)
                {
                    DOTween.Kill(vessel[i].transform);
                    int index = Mathf.Min(insertIndex + i + 1, cells.Length - 1);
                    vessel[i].transform.DOMove(cells[index], 1);
                }
            }
            else
            {
                for (int i = insertIndex + 1; i < vessel.Count; i++)
                {
                    DOTween.Kill(vessel[i].transform);
                    int index = Mathf.Min(i, cells.Length - 1);
                    vessel[i].transform.DOMove(cells[index], 1);
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
                    vessel[i].transform.DOMove(cells[index], 1);
                }

            }
        };
    }
}
