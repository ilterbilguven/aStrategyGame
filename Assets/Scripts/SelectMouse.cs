using System;
using UnityEngine;

/// <summary>
///   To select objects with mouse and control them.
/// </summary>
public class SelectMouse : MonoBehaviour
{
	
	public Vector3 pos;
	public GameObject selected;


	public delegate void RightClickAction(Vector3 pos);

	public static event RightClickAction onClick;

	public static SelectMouse instance { get; private set; }

	void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(this);
		}
		else
		{
			instance = this;
		}
	}

	void Update()
	{
		pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pos = new Vector3(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), 0); // get the real 2D integer position
		if (Input.GetMouseButtonDown(0))
		{
			if (Physics2D.OverlapPoint(pos) == null)
			{
				selected = null;
				Debug.Log("null is selected.");
				clearDelegate();
				myCanvas.instance.informationMenu.SetActive(false);
			}
		}
		else if (Input.GetMouseButtonDown(1))
		{
			if (onClick != null)
			{
				onClick(pos);
			}
		}
	}

	public void clearDelegate()
	{
		onClick = null;
	}
	
}