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
		pos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // get the real position
		if (Input.GetMouseButtonDown(0)) // left click
			if (Physics2D.OverlapPoint(pos) == null) // if there is nothing, then nothing
			{
				selected = null;
				Debug.Log("null is selected.");
				Destroy(GameObject.Find("InformationMenu"));
			}
			else
			{
				selected = Physics2D.OverlapPoint(pos).transform.parent.gameObject; // if there is something, get the gameobject of it.
				Debug.Log(pos);
				switch (selected.GetComponentInChildren<Collider2D>().tag)
				{
					case "Building": //if it's a building then show the info menu.
						Destroy(GameObject.Find("InformationMenu"));
						Debug.Log(selected.GetComponentInChildren<Collider2D>().tag + " is selected.");
						var o = Instantiate(infoPrefab, GameObject.Find("Canvas").transform);
						o.name = infoPrefab.name;
						GameObject.Find("InformationMenu").GetComponentInChildren<Information>().title.text = selected.name;
						break;
					case "Border": // to prevent bug in info menu.
						Debug.Log(selected.GetComponentInChildren<Collider2D>().tag + " is selected.");
						break;
					case "Unit": 
						Destroy(GameObject.Find("InformationMenu"));
						Debug.Log(selected.GetComponentInChildren<Collider2D>().tag + " is selected.");
						break;
				}
			}

		if (selected != null && Input.GetMouseButtonDown(1)) // right click
		{
			Debug.Log(selected.GetComponentInChildren<Collider2D>().tag);
			switch (selected.GetComponentInChildren<Collider2D>().tag)
			{
				case "Unit":
					if (checkOccupation()) // if there is something, then do nothing and show error.
					{
						GameObject.Find("ErrorText").GetComponent<ErrorText>().ChangeMessage("Can't go there. Area is occupied.");
						break;
					}

					var moveunit = selected.GetComponent<MoveUnit>();
					moveunit.Init();
					var startPoint = moveunit.map.cols * (moveunit.map.rows - Mathf.RoundToInt(selected.transform.position.y) - 1) +
					                 Mathf.RoundToInt(selected.transform.position.x);

					var endPoint = moveunit.map.cols * (moveunit.map.rows - Mathf.RoundToInt(pos.y) - 1) + Mathf.RoundToInt(pos.x);

					Debug.Log(selected.transform.position + " = " + startPoint + "; " + pos + " = " + endPoint + "; length: " +
					          moveunit.graph.Nodes.Length);


					//Debug.Break();

					moveunit.search.Start(moveunit.graph.Nodes[startPoint], moveunit.graph.Nodes[endPoint]); // move the unit.

					break;

				case "Building":
					if (checkOccupation()) // if there is something, then do nothing and show error.
					{
						GameObject.Find("ErrorText").GetComponent<ErrorText>().ChangeMessage("Can't assign there as a spawn point. Area is occupied.");
						break;
					}
					selected.transform.Find("SpawnPoint").gameObject.SetActive(true);
					selected.transform.Find("SpawnPoint").position = new Vector3(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y)); // assign the spawn point.

					break;
			}
		}
	}

	bool checkOccupation()
	{
		return Physics2D.OverlapPoint(pos);
	}
}