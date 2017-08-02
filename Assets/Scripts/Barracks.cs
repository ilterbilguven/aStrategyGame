using UnityEngine;

public class Barracks : Building
{
	private GameObject _unit;


	/// <summary>
	///   I wanted to make "unit" move to spawn point after initialized, but I got a reference problem, and couldn't figured it
	///   out.
	/// </summary>
	/// <param name="unit"></param>
	public override void Spawn(string unit)
	{
		if (Physics2D.OverlapPoint(transform.parent.Find("SpawnPoint").position))
		{
			GameObject.Find("ErrorText").GetComponent<ErrorText>().ChangeMessage("Spawn Point is occupied. Assign a new spawn point.");
		}
		else if (!transform.parent.Find("SpawnPoint").gameObject.activeSelf)
		{
			GameObject.Find("ErrorText").GetComponent<ErrorText>().ChangeMessage("Spawn Point isn't assigned. Assign a spawn point.");

		}
		else
		{
			_unit = Instantiate((GameObject)Resources.Load(
				"Prefabs/Units/" + transform.parent.gameObject.name + "/" + unit));

			//_unit.transform.position = transform.parent.Find("SpawnPoint").position;
			_unit.transform.position = transform.parent.position;

			var moveunit = _unit.GetComponent<MoveUnit>();
			EmptyGrid();
			moveunit.Init();

			var startPoint = moveunit.map.cols * (moveunit.map.rows - Mathf.RoundToInt(_unit.transform.position.y) - 1) +
			                 Mathf.RoundToInt(_unit.transform.position.x);

			var endPoint = moveunit.map.cols * (moveunit.map.rows - Mathf.RoundToInt(transform.parent.Find("SpawnPoint").position.y) - 1) + Mathf.RoundToInt(transform.parent.Find("SpawnPoint").position.x);

			moveunit.search.Start(moveunit.graph.Nodes[startPoint], moveunit.graph.Nodes[endPoint]);
			updateGrid = true;
		}
		

	}
}