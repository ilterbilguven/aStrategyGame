using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Building
{

	private GameObject _unit;


	/// <summary>
	/// I wanted to make "unit" move to spawn point after initialized, but I got a reference problem, and couldn't figured it out. 
	/// </summary>
	/// <param name="unit"></param>
	public override void Spawn(GameObject unit)
	{
		_unit = Instantiate(unit);
		
		_unit.transform.position = transform.parent.Find("SpawnStartPoint").position;

	}

}
