using System.Collections;
using UnityEngine;

/// <summary>
///   Other buildings will update this grid
/// </summary>
public class Map : MonoBehaviour
{
	public int[,] _map;

	public int cols = 27;
	public int rows = 32;

	private void Awake()
	{
		_map = new int[rows, cols];
		for (var i = 0; i < rows; i++) for (var j = 0; j < cols; j++) _map[i, j] = 0;
		
	}

	private void Start()
	{
		//StartCoroutine(show());
		//StartCoroutine(shownodes());
	}

	/// <summary>
	///   debug purposes
	/// </summary>
	/// <returns></returns>
	private IEnumerator show()
	{
		yield return new WaitForSeconds(10);

		var line = string.Empty;
		for (var i = 0; i < rows; i++)
		{
			for (var j = 0; j < cols; j++)
				//print(i + ", " + j);
				line += _map[i, j] + " ";
			print(line);
			line = string.Empty;
		}
	}
	//	{
	//	foreach (Node node in nodes)
	//	var nodes = GameObject.Find("Soldier").GetComponent<MoveUnit>().graph.Nodes;
	//	yield return new WaitForSeconds(1);
	//{

	//IEnumerator shownodes()
	//		print(node.pos + " ");
	//	}
	//}
}