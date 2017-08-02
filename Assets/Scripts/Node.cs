using System.Collections.Generic;
using UnityEngine;

/// <summary>
///   For A* search
/// </summary>
public class Node
{
	public List<Node> AdjacentNodes = new List<Node>();
	public float Gscore = Mathf.Infinity;
	public float Hscore = Mathf.Infinity;
	public string label = "";
	public Vector2 pos = Vector2.zero;
	public Node PreviousNode;

	public void Clear()
	{
		PreviousNode = null;
	}
}