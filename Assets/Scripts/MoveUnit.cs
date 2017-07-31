using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveUnit : MonoBehaviour
{

	public Map map;
	public Search search;
	public Graph graph;

	void Start()
	{
		Init();
		//search.Start(graph.Nodes[(int) (map.cols * transform.position.x + transform.position.y)], graph.Nodes[1765]);
		//Debug.Break();
		StartCoroutine(move());
	}

	public void Init()
	{
		map = GameObject.Find("Map").GetComponent<Map>();

		graph = new Graph(map._map);
		search = new Search(graph);
	}

	void Update()
	{

		if (search.isStartInitialized && !search.finished)
		{
			
			search.Step();
		}
	}

	public IEnumerator move()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.1f); // If I don't put this, Unity hangs out like forever.
			if (search.finished)
			{
				Debug.Log("finished");
				foreach (Node node in search.path)
				{
					yield return new WaitForSeconds(0.01f);
					//Debug.Break();
					//Debug.Log(node.pos.y + " " + node.pos.x);
					//transform.Translate(new Vector3(node.pos.y, node.pos.x, transform.position.z));
					transform.position = new Vector3(node.pos.x, node.pos.y, transform.position.z);
				}
				Init();
			}
		}
	}
}
