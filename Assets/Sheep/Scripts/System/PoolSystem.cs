using QMVC;
using Sheep;
using UnityEngine;

public class PoolSystem : AbstractSystem
{
    private Transform poolRoot;

    MonoPool<BlockController> blockPool;


    protected override void OnInit()
    {
		poolRoot = new GameObject("Pools").transform;
		Transform blockParent = new GameObject("Blocks").transform;
		blockParent.SetParent(poolRoot);
		blockPool = new MonoPool<BlockController>(Resources.Load<BlockController>(""), blockParent);
    }

    protected override void OnDeinit()
    {
        GameObject.Destroy(poolRoot.gameObject);
    }


	public BlockController GetBlock()
	{
		return blockPool.Get();
	}

	public void RecycleBlock(BlockController block)
	{
		blockPool.Recycle(block);
	}
}
