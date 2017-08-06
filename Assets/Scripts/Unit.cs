using System.Collections;
using UnityEngine;

/// <summary>
/// initializes the a* search and its dependencies
/// then applies the path if there is exists
/// </summary>
public class Unit : MonoBehaviour
{
	public Graph graph;
	public Map map;
	public Search search;

	private void Start()
	{
		//Init();
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

	private void FixedUpdate()
	{
		if (search != null && search.isStartInitialized && !search.finished)
			search.Step();
	}

	public IEnumerator move()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.1f); // If I don't put this, Unity hangs out like forever.
			if (search != null && search.finished)
			{
				Debug.Log("finished");
				foreach (var node in search.path)
				{
					yield return new WaitForSeconds(0.05f);
					//Debug.Break();
					//Debug.Log(node.pos.y + " " + node.pos.x);
					//transform.Translate(new Vector3(node.pos.y, node.pos.x, transform.position.z));
					transform.parent.position = new Vector3(node.pos.x, node.pos.y, transform.parent.position.z);
				}
				Init();
			}
		}
	}

	void OnMouseDown()
	{
		SelectMouse.instance.clearDelegate();
		SelectMouse.instance.selected = transform.parent.gameObject;
		SelectMouse.onClick += startSearch;

		myCanvas.instance.informationMenu.SetActive(false);
	}

	 public void startSearch(Vector3 pos)
	{
		Init();

		Debug.Log(pos);
		var startPoint = map.cols * (map.rows - (int)transform.parent.position.y - 1) + (int)transform.parent.position.x;

		var endPoint = map.cols * (map.rows - (int)pos.y - 1) + (int)pos.x;

		Debug.Log(transform.parent.position + " = " + startPoint + "; " + pos + " = " + endPoint + "; length: " +
		          graph.Nodes.Length);


		//Debug.Break();

		search.Start(graph.Nodes[startPoint], graph.Nodes[endPoint]); // move the unit.
	}
}