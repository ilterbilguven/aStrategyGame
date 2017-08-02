using UnityEngine;

/// <summary>
///   To select objects with mouse and control them.
/// </summary>
public class SelectMouse : MonoBehaviour
{
	public GameObject infoPrefab;
	private Vector3 pos;
	public GameObject selected;

	private void Update()
	{
		pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if (Input.GetMouseButtonDown(0))
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

		if (selected != null && Input.GetMouseButtonDown(1))
		{
			Debug.Log(selected.GetComponentInChildren<Collider2D>().tag);
			switch (selected.GetComponentInChildren<Collider2D>().tag)
			{
				case "Unit":
					Destroy(GameObject.Find("InformationMenu"));
					var moveunit = selected.GetComponent<MoveUnit>();
					moveunit.Init();
					var startPoint = moveunit.map.cols * (moveunit.map.rows - Mathf.RoundToInt(selected.transform.position.y) - 1) +
					                 Mathf.RoundToInt(selected.transform.position.x);

					var endPoint = moveunit.map.cols * (moveunit.map.rows - Mathf.RoundToInt(pos.y) - 1) + Mathf.RoundToInt(pos.x);

					Debug.Log(selected.transform.position + " = " + startPoint + "; " + pos + " = " + endPoint + "; length: " +
					          moveunit.graph.Nodes.Length);


					//Debug.Break();

					moveunit.search.Start(moveunit.graph.Nodes[startPoint], moveunit.graph.Nodes[endPoint]);

					break;

				case "Building":
					selected.transform.Find("SpawnPoint").position = new Vector3(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));

					break;
			}
		}
	}
}