using System.Collections.Generic;
using UnityEngine;

namespace Sheep
{
	public class GridController : BaseController
	{
		public GridController(Vector2 location)
		{
			blocks = new Stack<int>();
			Location = location;
		}
		Stack<int> blocks;

		public Vector2 Location { get; private set; }

		#region Êý¾Ý²Ù×÷

		public int Peek()
		{
			return blocks.Count > 0 ? blocks.Peek() : -1;
		}

		public void Push(int id)
		{
			blocks.Push(id);
		}

		public int Pop()
		{
			return blocks.Pop();
		}

		#endregion

	}
}


