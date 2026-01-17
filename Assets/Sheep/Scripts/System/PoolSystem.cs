using QMVC;
using Sheep;
using UnityEngine;

public class PoolSystem : AbstractSystem
{
    private Transform poolRoot;

    MonoPool<BlockController> blockPool;
	ComponentPool<SFXController> sfxPool;

	AssetSystem _assetSystem;


    protected override void OnInit()
    {
		_assetSystem = this.GetSystem<AssetSystem>();

		poolRoot = new GameObject("Pools").transform;
		Transform blockParent = new GameObject(nameof(BlockController)).transform;
		blockParent.SetParent(poolRoot);
		blockPool = new MonoPool<BlockController>(_assetSystem.block, blockParent);

		Transform sfxParent = new GameObject("SFX").transform;
		sfxParent.SetParent(poolRoot);
		sfxPool = new ComponentPool<SFXController>(_assetSystem.sfx, sfxParent);
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

	public void RecycleAllBlock()
	{
		blockPool.RecycleAll();
	}

	public void RecycleSFX(SFXController source)
	{
		sfxPool.Recycle(source);
	}
}
