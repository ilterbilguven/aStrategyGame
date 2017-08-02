using UnityEngine;

/// <summary>
///   All buildings need this
/// </summary>
public abstract class Building : MonoBehaviour
{
	public bool dropCheck = true;
	public int[,] map;
	public bool updateGrid;

	internal void Start()
	{
		map = GameObject.Find("Map").GetComponent<Map>()._map;
		//Debug.Log(transform.parent.name + " position: " + transform.parent.position + "; collider size  x y : " + Mathf.RoundToInt(GetComponent<Collider2D>().bounds.size.x) + " " + Mathf.RoundToInt(GetComponent<Collider2D>().bounds.size.y));
	}

	/// <summary>
	///   Update grid
	/// </summary>
	internal void Update()
	{
		if (updateGrid)
			FillGrid();
	}

	private void FillGrid()
	{
		for (var i = 0; i < Mathf.RoundToInt(GetComponent<Collider2D>().bounds.size.x); i++)
		for (var j = 0; j < Mathf.RoundToInt(GetComponent<Collider2D>().bounds.size.y); j++)
			map[map.GetLength(0) - Mathf.RoundToInt(transform.parent.position.y) - 1 - j,
				Mathf.RoundToInt(transform.parent.position.x) - i] = 1;
	}

	public void EmptyGrid()
	{
		updateGrid = false;
		for (var i = 0; i < Mathf.RoundToInt(GetComponent<Collider2D>().bounds.size.x); i++)
		for (var j = 0; j < Mathf.RoundToInt(GetComponent<Collider2D>().bounds.size.y); j++)
			map[map.GetLength(0) - Mathf.RoundToInt(transform.parent.position.y) - 1 - j,
				Mathf.RoundToInt(transform.parent.position.x) - i] = 0;
	}

	/// <summary>
	///   To block player build a building on another building
	/// </summary>
	/// <param name="collision"></param>
	private void OnTriggerEnter2D(Collider2D collision)
	{
		GameObject.Find("ErrorText").GetComponent<ErrorText>().ChangeMessage("Can't build there. Area is not empty.");

		dropCheck = false;
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		GameObject.Find("ErrorText").GetComponent<ErrorText>().ChangeMessage("Can't build there. Area is not empty.");

		dropCheck = false;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		GameObject.Find("ErrorText").GetComponent<ErrorText>().ChangeMessage("Can't build there. Area is not empty.");

		dropCheck = true;
	}


	/// <summary>
	///   If a building have something to spawn, it will implement this.
	/// </summary>
	/// <param name="unit"></param>
	public abstract void Spawn(string unit);
}