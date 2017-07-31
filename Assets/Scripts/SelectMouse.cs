using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMouse : MonoBehaviour
{
	public GameObject infoPrefab;
	public GameObject selected;
	public Vector3 pos;
	
	// Update is called once per frame
	void Update ()
	{
		pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if (Input.GetMouseButtonDown(0))
		{
			//Destroy(GameObject.Find("InformationMenu"));
			if (Physics2D.OverlapPoint(pos) == null)
			{
				//selected = null;
				Debug.Log("null is selected.");
			}
			else
			{
				Destroy(GameObject.Find("InformationMenu"));
				selected = Physics2D.OverlapPoint(pos).transform.parent.gameObject;
				Debug.Log(selected.GetComponentInChildren<Collider2D>().tag + " is selected.");

				switch (selected.GetComponentInChildren<Collider2D>().tag)
				{
					case "Building":
						var o = Instantiate(infoPrefab, GameObject.Find("Canvas").transform);
						o.name = infoPrefab.name;
						GameObject.Find("InformationMenu").GetComponentInChildren<Information>().title.text = selected.name;
						break;
				}
			}
		}

		if (selected != null && Input.GetMouseButtonDown(1))
		{
			Destroy(GameObject.Find("InformationMenu"));
			Debug.Log(selected.GetComponentInChildren<Collider2D>().tag);
			switch (selected.GetComponentInChildren<Collider2D>().tag)
			{
				case "Unit":
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
