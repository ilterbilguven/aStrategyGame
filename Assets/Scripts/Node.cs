using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For A* search
/// </summary>
public class Node {

	public List<Node> AdjacentNodes = new List<Node>();
	public Node PreviousNode = null;
	public string label = "";
	public float Gscore = Mathf.Infinity;
	public float Hscore = Mathf.Infinity;
	public Vector2 pos = Vector2.zero;

	public void Clear()
	{
		PreviousNode = null;
	}
}
