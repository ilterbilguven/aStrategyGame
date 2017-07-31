using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Building
{

	public GameObject _unit;
	public override void Spawn(GameObject unit)
	{
		_unit = Instantiate(unit);
		
		_unit.transform.position = transform.parent.Find("SpawnStartPoint").position;

	}

}
