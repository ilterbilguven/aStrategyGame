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

	/// <summary>
	///   constructor
	/// </summary>
	/// <param name="_graph"></param>
	public Search(Graph _graph)
	{
		graph = _graph;
	}

	/// <summary>
	///   initialize the search by adding first node that is start
	/// </summary>
	/// <param name="start">start point</param>
	/// <param name="goal">end point</param>
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
		finished = false;


		for (var i = 0; i < graph.Nodes.Length; i++)
			graph.Nodes[i].Clear();
		isStartInitialized = true;
	}

	/// <summary>
	///   calculating the euclid distance
	/// </summary>
	/// <param name="currentNode">given node</param>
	public void CalculateCost(Node currentNode)
	{
		currentNode.Gscore = Vector2.Distance(currentNode.pos, startNode.pos);

		currentNode.Hscore = Vector2.Distance(currentNode.pos, goalNode.pos);
	}

	/// <summary>
	///   Search happens
	/// </summary>
	public void Step()
	{
		if (path.Count > 0) // means there is a path.
			return;
		if (reachable.Count == 0) // means there is no path.
		{
			finished = true;
			ErrorText.instance.ChangeMessage("Can't go there. There is no way to go.");
			return;
		}

		iterations++;

		var node = ChoseNode();
		if (node == goalNode) // to check if it is end point.
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
		// this node is explored now. 

		foreach (var t in node.AdjacentNodes)
			AddAdjacent(node, t);
	}

	/// <summary>
	///   add this node's adjacent nodes to the reachable to continue.
	/// </summary>
	/// <param name="node">this node</param>
	/// <param name="adjacent">this node's adjacent nodes</param>
	public void AddAdjacent(Node node, Node adjacent)
	{
		if (FindNode(adjacent, explored) || FindNode(adjacent, reachable)
		) // if there are already in explored or reachable, skip.
			return;
		CalculateCost(adjacent);
		adjacent.PreviousNode = node;
		reachable.Add(adjacent);
	}

	/// <summary>
	///   check if the node is the given list
	/// </summary>
	/// <param name="node">given node</param>
	/// <param name="list">given list</param>
	/// <returns>true if exists, otherwise false</returns>
	public bool FindNode(Node node, List<Node> list)
	{
		return GetNodeIndex(node, list) >= 0;
	}

	/// <summary>
	///   get node's index in the list
	/// </summary>
	/// <param name="node">given node</param>
	/// <param name="list">given list</param>
	/// <returns>index if there exists, otherwise -1</returns>
	public int GetNodeIndex(Node node, List<Node> list)
	{
		for (var i = 0; i < list.Count; i++)
			if (node == list[i])
				return i;

		return -1;
	}

	/// <summary>
	///   choosing the least cost in the reachable list.
	/// </summary>
	/// <returns>node whose cost is the least</returns>
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