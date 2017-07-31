using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{

	public Vector3 rawInput;
	public Vector3 roundedOutput;
	public GameObject building;


	void Update ()
	{
		if (building != null)
		{
			rawInput = Input.mousePosition;
			rawInput.z = building.transform.position.z - Camera.main.transform.position.z;
			roundedOutput = new Vector3(Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(rawInput).x), Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(rawInput).y), Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(rawInput).z));
			building.transform.localPosition = roundedOutput;

			if (Input.GetMouseButtonDown(0))
			{
				Drop();
			}
			if (Input.GetMouseButtonDown(1))
			{
				DestroyBuilding();
			}
		}
	
	}

	public void Pick(GameObject b)
	{
		building = b;
	}

	private void Drop()
	{
		if (building.GetComponentInChildren<Building>().canBeBuilt())
		{
			building.GetComponentInChildren<Building>().updateGrid = true;
			building = null;
		}
		
	}

	public void DestroyBuilding()
	{
		if (building != null) Destroy(building);
	}





}
