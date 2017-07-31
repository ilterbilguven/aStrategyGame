﻿using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

/// <summary>
/// Takes building from "BuildingPicker".
/// Left Click: Build and update grid
/// Right Click: Abort and destroy
/// </summary>
public class FollowMouse : MonoBehaviour
{

	private Vector3 rawInput;
	private Vector3 roundedOutput;
	public GameObject building;

	private GameObject pMenu;

	void Start()
	{
		pMenu = GameObject.Find("ProductionMenu");
	}
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
		pMenu.SetActive(false);
	}

	private void Drop()
	{
		if (building.GetComponentInChildren<Building>().dropCheck)
		{
			building.GetComponentInChildren<Building>().updateGrid = true;
			building = null;
			pMenu.SetActive(true);
		}
		
	}

	public void DestroyBuilding()
	{
		if (building != null)
		{
			Destroy(building);
			pMenu.SetActive(true);
		}
		
	}





}
