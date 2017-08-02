using UnityEngine;

public class Barracks : Building
{
	private GameObject _unit;


	/// <summary>
	///  spawns the unit and moves it to the spawn point.
	/// </summary>
	/// <param name="unit">name of the unit</param>
	public override void Spawn(string unit)
	{
		if (Physics2D.OverlapPoint(transform.parent.Find("SpawnPoint").position)) //checks there is something.
		{
			GameObject.Find("ErrorText").GetComponent<ErrorText>().ChangeMessage("Spawn Point is occupied. Assign a new spawn point.");
		}
		else if (!transform.parent.Find("SpawnPoint").gameObject.activeSelf) // checks if the spawn point is assigned.
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
			EmptyGrid(); // emptying the grid to allow movement for the spawned unit.
			moveunit.Init();

			var startPoint = moveunit.map.cols * (moveunit.map.rows - Mathf.RoundToInt(_unit.transform.position.y) - 1) +
			                 Mathf.RoundToInt(_unit.transform.position.x);

			var endPoint = moveunit.map.cols * (moveunit.map.rows - Mathf.RoundToInt(transform.parent.Find("SpawnPoint").position.y) - 1) + Mathf.RoundToInt(transform.parent.Find("SpawnPoint").position.x);

			moveunit.search.Start(moveunit.graph.Nodes[startPoint], moveunit.graph.Nodes[endPoint]);
			updateGrid = true; // filling the grid again
		}
		

	}
}