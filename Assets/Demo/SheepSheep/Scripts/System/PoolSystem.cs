using QFramework;
using UnityEngine;

namespace SheepSheep
{
    public class PoolSystem : AbstractSystem
    {
        Transform root;

        MonoPool<BlockController> blockPool;
        MonoPool<AudioSource> audioSourcePool;

        protected override void OnInit()
        {
            root = new GameObject("Pool").transform;
            Transform blocks = new GameObject("blocks").transform;
            blocks.SetParent(root);
            Transform audioSources = new GameObject("audioSource").transform;
            audioSources.SetParent(root);
            blockPool = new MonoPool<BlockController>(Resources.Load<BlockController>("block"), blocks);
            audioSourcePool = new MonoPool<AudioSource>(Resources.Load<AudioSource>("audioSource"), audioSources);
        }

        public BlockController GetBlock()
        {
            return blockPool.Get();
        }

        public void RecycleBlock(BlockController block)
        {
            blockPool.Recycle(block);
        }


        public AudioSource GetAudioSource()
        {
            return audioSourcePool.Get();
        }

        public void RecycleAudioSource(AudioSource audioSource)
        {
            audioSourcePool.Recycle(audioSource);
        }
    }
}

