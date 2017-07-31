using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
	/// <summary>
	/// For A* search
	/// Converts coordinate system to 2D array.
	/// </summary>
	public int rows = 0;
	public int cols = 0;
	public Node[] Nodes;

	public Graph(int[,] grid)
	{
		rows = grid.GetLength(0);
		cols = grid.GetLength(1);
		
		Nodes = new Node[grid.Length];
		for (int i = 0; i < Nodes.Length; i++)
		{
			var node = new Node();
			node.label = i.ToString();
			Nodes[i] = node;
		}

		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < cols; j++)
			{
				var node = Nodes[cols * i + j];
				node.pos = new Vector2(j, rows - i - 1); //magic happens
				
				if (grid[i, j] == 1)
				{
					continue;
				}

				int pos;
				
				//Debug.Break();

				//Up
				if (i > 0)
				{
					pos = cols * (i - 1) + j;
					if (grid[i - 1, j] != 1) 
					{
						node.AdjacentNodes.Add(Nodes[pos]);
					}
					
				}

				//right
				if (j < cols -1)
				{
					pos = cols * i + j + 1;
					if (grid[i, j + 1] != 1) 
					{
						node.AdjacentNodes.Add(Nodes[pos]);
					}
					
				}

				//down
				if (i < rows - 1)
				{
					pos = cols * (i + 1) + j;
					if (grid[i + 1, j] != 1)
					{
						node.AdjacentNodes.Add(Nodes[pos]);
					}
					
				}

				//left
				if (j > 0)
				{
					pos = cols * i + j - 1;
					if (grid[i, j - 1] != 1)
					{
						node.AdjacentNodes.Add(Nodes[pos]);
					}
					
				}
			}
		}
		//Debug.Break();
	}
}
