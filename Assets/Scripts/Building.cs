using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All buildings need this
/// </summary>
public abstract class Building : MonoBehaviour
{
	
	public bool dropCheck = true;
	public bool updateGrid = false;
	public int[,] map;

	void Start()
	{
		map = GameObject.Find("Map").GetComponent<Map>()._map;
		//Debug.Log(transform.parent.name + " position: " + transform.parent.position + "; collider size  x y : " + Mathf.RoundToInt(GetComponent<Collider2D>().bounds.size.x) + " " + Mathf.RoundToInt(GetComponent<Collider2D>().bounds.size.y));


	}

	/// <summary>
	/// Update grid
	/// </summary>
	void Update()
	{
		if (updateGrid)
		{
			for (int i = 0; i < Mathf.RoundToInt(GetComponent<Collider2D>().bounds.size.x); i++)
			{
				for (int j = 0; j < Mathf.RoundToInt(GetComponent<Collider2D>().bounds.size.y); j++)
				{

					map[map.GetLength(0) - Mathf.RoundToInt(transform.parent.position.y) - 1 - j,
						Mathf.RoundToInt(transform.parent.position.x) - i] = 1;
				}
			}
		}
	
	}

	/// <summary>
	/// To block player build a building on another building
	/// </summary>
	/// <param name="collision"></param>

	private void OnTriggerEnter2D(Collider2D collision)
	{
		dropCheck = false;
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		dropCheck = false;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		dropCheck = true;
	}
	/// <summary>
	/// If a building have something to spawn, it will implement this.
	/// </summary>
	/// <param name="unit"></param>
	public abstract void Spawn(GameObject unit);

	}
