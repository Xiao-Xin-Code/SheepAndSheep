using QMVC;
using Sheep;
using UnityEngine;

public class PoolSystem : AbstractSystem
{
    private Transform poolRoot;

    MonoPool<BlockController> blockPool;
	ComponentPool<SFXController> sfxPool;


    protected override void OnInit()
    {
		poolRoot = new GameObject("Pools").transform;
		Transform blockParent = new GameObject(nameof(BlockController)).transform;
		blockParent.SetParent(poolRoot);
		blockPool = new MonoPool<BlockController>(Resources.Load<BlockController>("Item"), blockParent);

		Transform sfxParent = new GameObject("SFX").transform;
		sfxParent.SetParent(poolRoot);
		sfxPool = new ComponentPool<SFXController>(Resources.Load<SFXController>(""), sfxParent);
	}

    protected override void OnDeinit()
    {
        GameObject.Destroy(poolRoot.gameObject);
    }


	public BlockController GetBlock()
	{
		return blockPool.Get();
	}

	public SFXController GetSFX()
	{
		return sfxPool.Get();
	}

	public void RecycleBlock(BlockController block)
	{
		blockPool.Recycle(block);
	}

	public void RecycleSFX(SFXController source)
	{
		sfxPool.Recycle(source);
	}
}
