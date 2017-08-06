using UnityEngine;

public class Barracks : Building
{
	[SerializeField] private GameObject _unit;

	public Unit moveunit;

	[SerializeField] private GameObject spawnPoint;

	/// <summary>
	///  spawns the unit and moves it to the spawn point.
	/// </summary>
	/// <param name="unit">name of the unit</param>
	public override void Spawn(string unit)
	{
		if (Physics2D.OverlapPoint(spawnPoint.transform.position)) //checks there is something.
		{
			GameObject.Find("ErrorText").GetComponent<ErrorText>().ChangeMessage("Spawn Point is occupied. Assign a new spawn point.");
		}
		else if (!spawnPoint.activeSelf) // checks if the spawn point is assigned.
		{
			GameObject.Find("ErrorText").GetComponent<ErrorText>()
				.ChangeMessage("Spawn Point isn't assigned. Assign a spawn point.");

		}
		else
		{
			Debug.Log(spawnPoint.transform.position);
			EmptyGrid();
			

			_unit = Instantiate((GameObject) Resources.Load("Prefabs/Units/" + transform.parent.gameObject.name + "/" + unit), transform.parent.position, Quaternion.identity);

			moveunit = _unit.GetComponentInChildren<Unit>();
			 // emptying the grid to allow movement for the spawned unit.
			//moveunit.Init();
			moveunit.startSearch(spawnPoint.transform.position);
			updateGrid = true; // filling the grid again
		}
	}

	internal override void OnMouseDown()
	{
		base.OnMouseDown();
		SelectMouse.onClick += SpawnPoint;
	}

	void SpawnPoint(Vector3 pos)
	{
		spawnPoint.SetActive(true);
		spawnPoint.transform.position = pos;
	}



}