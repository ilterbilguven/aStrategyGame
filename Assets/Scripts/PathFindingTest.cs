using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathFindingTest : MonoBehaviour
{

	public GameObject MapGroup;
	public GameObject s;

	// Use this for initialization
	void Start ()
	{
		var map = GameObject.Find("Map").GetComponent<Map>();
		var graph = new Graph(map._map);
		var search = new Search(graph);
		search.Start(graph.Nodes[(int)(map.cols * s.transform.position.x + s.transform.position.y)], graph.Nodes[1765]);

		while (!search.finished)
		{
			search.Step();
		}
		print("Search done. Path length " + search.path.Count + " iteartions " + search.iterations);

		ResetMapGroup(graph);


		foreach (var node in search.path)
		{
			GetImage(node.label).color = Color.red;
		}
		//Debug.Break();
	}

	Image GetImage(string label)
	{
		var id = Int32.Parse(label);
		var go = MapGroup.transform.GetChild(id);
		return go.GetComponent<Image>();
	}

	void ResetMapGroup(Graph graph)
	{
		foreach (Node graphNode in graph.Nodes)
		{
			GetImage(graphNode.label).color = graphNode.AdjacentNodes.Count == 0 ? Color.white : Color.gray;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
