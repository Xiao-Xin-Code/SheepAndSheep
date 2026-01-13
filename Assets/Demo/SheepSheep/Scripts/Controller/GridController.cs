using System.Collections.Generic;
using UnityEngine;

namespace SheepSheep
{
    public class GridController : BaseController
    {
        public GridController(Vector2 location)
        {
            blocks = new Stack<BlockController>();
            Location = location;
        }

        Stack<BlockController> blocks;
        public Vector2 Location { get; private set; }


        public BlockController Peek()
        {
            return blocks.Count == 0 ? null : blocks.Peek();
        }

        public void Push(BlockController block)
        {
            if (block == null) return;
            blocks.Push(block);
        }

        public BlockController Pop()
        {
            return blocks.Pop();
        }
    }
}

