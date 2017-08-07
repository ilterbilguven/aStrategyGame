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
		if (!spawnPoint.activeSelf) // checks if the spawn point is assigned.
		{
			ErrorText.instance.ChangeMessage("Spawn Point isn't assigned. Assign a spawn point.");
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

	private void LateUpdate()
	{
		if (SelectMouse.instance.selected != null)
		{
			if (SelectMouse.instance.selected.transform.position != transform.position)
			{
				spawnPoint.SetActive(false);
			}
		}
		else
		{
			spawnPoint.SetActive(false);
		}

	}

	internal override void OnMouseDown()
	{
		base.OnMouseDown();
		if (updateGrid)
		{
			SelectMouse.onClick += SpawnPoint;
			spawnPoint.SetActive(true);
		}

	}

	void SpawnPoint(Vector3 pos)
	{
		spawnPoint.transform.position = pos;
	}



}