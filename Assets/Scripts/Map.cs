using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

	public int[,] _map;
	
	public int cols = 57;
	public int rows = 32; 

	void Awake()
	{
		_map = new int[rows, cols];
		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < cols; j++)
			{
				if (i == 0 || i == rows - 1)
				{
					_map[i, j] = 1;
					continue;
				}
				if (j == 0 || j == cols - 1)
				{
					_map[i, j] = 1;
					continue;
				}
				_map[i, j] = 0;
			}
		}
	}

	void Start()
	{
		//StartCoroutine(show());
		//StartCoroutine(shownodes());
	}

	IEnumerator show()
	{
		yield return new WaitForSeconds(10);

		string line = String.Empty;
		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < cols; j++)
			{
				//print(i + ", " + j);
				line += _map[i, j] + " ";
				}
			print(line);
			line = String.Empty;
		}
	}

	//IEnumerator shownodes()
	//{
	//	yield return new WaitForSeconds(1);
	//	var nodes = GameObject.Find("Soldier").GetComponent<MoveUnit>().graph.Nodes;
	//	foreach (Node node in nodes)
	//	{
	//		print(node.pos + " ");
	//	}
	//}
}
