using QMVC;
using UnityEngine;

public class PoolSystem : AbstractSystem
{
    private Transform poolRoot;


    protected override void OnInit()
    {
		poolRoot = new GameObject("Pools").transform;
    }

    protected override void OnDeinit()
    {
        GameObject.Destroy(poolRoot.gameObject);
    }
}
