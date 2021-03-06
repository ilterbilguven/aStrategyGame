﻿using UnityEngine;

/// <summary>
///   Takes building from "BuildingPicker".
///   Left Click: Build and update grid
///   Right Click: Abort and destroy
/// </summary>
public class FollowMouse : MonoBehaviour
{
	public GameObject building;
	private GameObject pMenu;

	private Vector3 rawInput;
	private Vector3 roundedOutput;
	private SelectMouse sm;


	/// <summary>
	/// singleton
	/// </summary>
	public static FollowMouse instance { get; private set; }

	private void Awake()
	{
		if (instance != null && instance != this)
			Destroy(this);
		else
			instance = this;
	}

	private void Start()
	{
		pMenu = myCanvas.instance.productionMenu; // to disable the unit and building contols when build mode is used.
		sm = SelectMouse.instance; // to disable the unit and building contols when build mode is used.
	}

	private void Update()
	{
		if (building != null)
		{
			rawInput = Input.mousePosition;
			rawInput.z = building.transform.position.z - Camera.main.transform.position.z;
			roundedOutput = new Vector3(Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(rawInput).x),
				Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(rawInput).y),
				Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(rawInput).z));
			building.transform.localPosition = roundedOutput;

			if (Input.GetMouseButtonDown(0))
				Drop();
			if (Input.GetMouseButtonDown(1))
				DestroyBuilding();
		}
	}

	public void Pick(GameObject b)
	{
		//Destroy(GameObject.Find("InformationMenu"));
		sm.enabled = false;
		building = b;
		pMenu.SetActive(false);
	}

	private void Drop()
	{
		if (building.GetComponentInChildren<Building>().dropCheck)
		{
			sm.enabled = true;
			building.GetComponentInChildren<Building>().updateGrid = true;
			building = null;
			pMenu.SetActive(true);
		}
	}

	public void DestroyBuilding()
	{
		if (building != null)
		{
			sm.enabled = true;
			Destroy(building);
			pMenu.SetActive(true);
		}
	}
}