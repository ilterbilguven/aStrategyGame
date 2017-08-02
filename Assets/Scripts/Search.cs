using System.Collections.Generic;
using UnityEngine;

/// <summary>
///   A* search
/// </summary>
public class Search
{
	public List<Node> explored;
	public bool finished;
	public Node goalNode;

	public Graph graph;
	public bool isStartInitialized;
	public int iterations;
	public List<Node> path;
	public List<Node> reachable;
	public Node startNode;

	public Search(Graph _graph)
	{
		graph = _graph;
	}

	public void Start(Node start, Node goal)
	{
		reachable = new List<Node>();

		startNode = start;
		goalNode = goal;
		CalculateCost(startNode);
		//CalculateCost(goalNode);

		reachable.Add(start);

		explored = new List<Node>();
		path = new List<Node>();
		iterations = 0;

		for (var i = 0; i < graph.Nodes.Length; i++)
			graph.Nodes[i].Clear();
		isStartInitialized = true;
	}

	/// <summary>
	/// manhattan distance
	/// </summary>
	/// <param name="currentNode"></param>
	public void CalculateCost(Node currentNode)
	{
		currentNode.Gscore = Mathf.RoundToInt(Mathf.Abs((currentNode.pos - startNode.pos).x) +
		                                      Mathf.Abs((currentNode.pos - startNode.pos).y));
		currentNode.Hscore = Mathf.RoundToInt(Mathf.Abs((currentNode.pos - goalNode.pos).x) +
		                                      Mathf.Abs((currentNode.pos - goalNode.pos).y));
	}

	public void Step()
	{
		if (path.Count > 0)
			return;
		if (reachable.Count == 0)
		{
			finished = true;
			GameObject.Find("ErrorText").GetComponent<ErrorText>().ChangeMessage("Can't go there. There is no way to go.");
			return;
		}

		iterations++;

		var node = ChoseNode();
		if (node == goalNode)
		{
			while (node != null)
			{
				path.Insert(0, node);
				node = node.PreviousNode;
			}
			finished = true;
			return;
		}

		reachable.Remove(node);
		explored.Add(node);

		foreach (var t in node.AdjacentNodes)
			AddAdjacent(node, t);
	}

	public void AddAdjacent(Node node, Node adjacent)
	{
		if (FindNode(adjacent, explored) || FindNode(adjacent, reachable))
			return;
		CalculateCost(adjacent);
		adjacent.PreviousNode = node;
		reachable.Add(adjacent);
	}

	public bool FindNode(Node node, List<Node> list)
	{
		return GetNodeIndex(node, list) >= 0;
	}

	public int GetNodeIndex(Node node, List<Node> list)
	{
		for (var i = 0; i < list.Count; i++)
			if (node == list[i])
				return i;

		return -1;
	}

	public Node ChoseNode()
	{
		//Debug.Break();
		var _cost = Mathf.Infinity;
		var _node = new Node();

		foreach (var node in reachable)
			if (node.Gscore + node.Hscore < _cost)
			{
				_node = node;
				_cost = node.Gscore + node.Hscore;
			}

		return _node;
	}
}