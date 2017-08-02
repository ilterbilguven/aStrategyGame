using UnityEngine;

public class Graph
{
	public int cols;
	public Node[] Nodes;

	/// <summary>
	///   For A* search
	///   Converts coordinate system to 2D array.
	/// </summary>
	public int rows;

	public Graph(int[,] grid)
	{
		rows = grid.GetLength(0);
		cols = grid.GetLength(1);

		Nodes = new Node[grid.Length];
		for (var i = 0; i < Nodes.Length; i++)
		{
			var node = new Node();
			node.label = i.ToString();
			Nodes[i] = node;
		}

		for (var i = 0; i < rows; i++)
		for (var j = 0; j < cols; j++)
		{
			var node = Nodes[cols * i + j];
			node.pos = new Vector2(j, rows - i - 1); //magic happens

			if (grid[i, j] == 1)
				continue;

			int pos;

			//Debug.Break();

			//Northwest
			if (i > 0 && j > 0)
			{
				pos = cols * (i - 1) + j - 1;
				if (grid[i - 1, j - 1] != 1)
					node.AdjacentNodes.Add(Nodes[pos]);
			}

			//North
			if (i > 0)
			{
				pos = cols * (i - 1) + j;
				if (grid[i - 1, j] != 1)
					node.AdjacentNodes.Add(Nodes[pos]);
			}

			//Northeast
			if (i > 0 && j < cols - 1)
			{
				pos = cols * (i - 1) + j + 1;
				if (grid[i - 1, j + 1] != 1)
					node.AdjacentNodes.Add(Nodes[pos]);
			}

			//East
			if (j < cols - 1)
			{
				pos = cols * i + j + 1;
				if (grid[i, j + 1] != 1)
					node.AdjacentNodes.Add(Nodes[pos]);
			}

			//Southeast
			if (i < rows - 1 && j < cols - 1)
			{
				pos = cols * (i + 1) + j + 1;
				if (grid[i + 1, j + 1] != 1)
					node.AdjacentNodes.Add(Nodes[pos]);
			}


			//South
			if (i < rows - 1)
			{
				pos = cols * (i + 1) + j;
				if (grid[i + 1, j] != 1)
					node.AdjacentNodes.Add(Nodes[pos]);
			}

			//Soutwest
			if (i < rows - 1 && j > 0)
			{
				pos = cols * (i + 1) + j - 1;
				if (grid[i + 1, j - 1] != 1)
					node.AdjacentNodes.Add(Nodes[pos]);
			}

			//West
			if (j > 0)
			{
				pos = cols * i + j - 1;
				if (grid[i, j - 1] != 1)
					node.AdjacentNodes.Add(Nodes[pos]);
			}
		}
		//Debug.Break();
	}
}