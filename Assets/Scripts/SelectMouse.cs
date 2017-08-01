using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// To select objects with mouse and control them.
/// </summary>
public class SelectMouse : MonoBehaviour
{
	public GameObject infoPrefab;
	public GameObject selected;
	private Vector3 pos;
	
	void Update ()
	{
		pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if (Input.GetMouseButtonDown(0))
		{
			//Destroy(GameObject.Find("InformationMenu"));
			if (Physics2D.OverlapPoint(pos) == null)
			{
				selected = null;
				Debug.Log("null is selected.");
				Destroy(GameObject.Find("InformationMenu"));
			}
			else
			{
				selected = Physics2D.OverlapPoint(pos).transform.parent.gameObject;
				Debug.Log(pos);
				switch (selected.GetComponentInChildren<Collider2D>().tag)
				{
					case "Building":
						Destroy(GameObject.Find("InformationMenu"));
						Debug.Log(selected.GetComponentInChildren<Collider2D>().tag + " is selected.");
						var o = Instantiate(infoPrefab, GameObject.Find("Canvas").transform);
						o.name = infoPrefab.name;
						GameObject.Find("InformationMenu").GetComponentInChildren<Information>().title.text = selected.name;
						break;
					case "Border":
						Debug.Log(selected.GetComponentInChildren<Collider2D>().tag + " is selected.");
						break;
				}
			}
		}

		if (selected != null && Input.GetMouseButtonDown(1))
		{
			Debug.Log(selected.GetComponentInChildren<Collider2D>().tag);
			switch (selected.GetComponentInChildren<Collider2D>().tag)
			{
				case "Unit":
					Destroy(GameObject.Find("InformationMenu"));
					var moveunit = selected.GetComponent<MoveUnit>();
					var startPoint = moveunit.map.cols * (moveunit.map.rows - Mathf.RoundToInt(selected.transform.position.y) - 1) + Mathf.RoundToInt(selected.transform.position.x);

					var endPoint = moveunit.map.cols * (moveunit.map.rows - Mathf.RoundToInt(pos.y) - 1) + Mathf.RoundToInt(pos.x);

					Debug.Log(selected.transform.position + " = " + startPoint + "; " + pos + " = " + endPoint + "; length: " + moveunit.graph.Nodes.Length);


					//Debug.Break();
					moveunit.search.Start(moveunit.graph.Nodes[startPoint], moveunit.graph.Nodes[endPoint]);
					print(moveunit.search.startNode.pos);
					print(moveunit.search.goalNode.pos);
					foreach (Node node in moveunit.search.path)
					{
						print(node.pos);
					}

					break;
			}
		}


	}
}
