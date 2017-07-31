using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Search
{

	public Graph graph;
	public List<Node> reachable;
	public List<Node> explored;
	public List<Node> path;
	public Node startNode;
	public Node goalNode;
	public int iterations;
	public bool finished;
	public bool isStartInitialized = false;
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

		for (int i = 0; i < graph.Nodes.Length; i++)
		{
			graph.Nodes[i].Clear();
		}
		isStartInitialized = true;
	}

	public void CalculateCost(Node currentNode)
	{
		currentNode.Gscore = Mathf.RoundToInt(Mathf.Abs((currentNode.pos - startNode.pos).x) + Mathf.Abs((currentNode.pos - startNode.pos).y));
		currentNode.Hscore = Mathf.RoundToInt(Mathf.Abs((currentNode.pos - goalNode.pos).x) + Mathf.Abs((currentNode.pos - goalNode.pos).y));
	}

	public void Step()
	{
		if (path.Count > 0)
		{
			return;
		}
		if (reachable.Count == 0)
		{
			finished = true;
			return;
		}

		iterations++;

		var node = ChoseNode();
		if (node == goalNode)
		{
			while (node != null)
			{
				path.Insert(0,node);
				node = node.PreviousNode;
			}
			finished = true;
			return;
		}

		reachable.Remove(node);
		explored.Add(node);

		foreach (Node t in node.AdjacentNodes)
		{
			AddAdjacent(node,t);
		}
	}

	public void AddAdjacent(Node node, Node adjacent)
	{
		if (FindNode(adjacent, explored) ||  FindNode(adjacent, reachable))
		{
			return;
		}
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
		for (int i = 0; i < list.Count; i++)
		{
			if (node==list[i])
			{
				return i;
			}
		}

		return -1;
	}

	public Node ChoseNode()
	{
		//Debug.Break();
		float _cost = Mathf.Infinity;
		var _node = new Node();
		
		foreach (Node node in reachable)
		{
			if (node.Gscore + node.Hscore < _cost)
			{
				_node = node;
				_cost = node.Gscore + node.Hscore;
			}
		}
		
		return _node;
	}

}
