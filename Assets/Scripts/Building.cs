using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
	[SerializeField]
	private bool dropCheck = true;

	void Start()
	{
		int[,] map = GameObject.Find("Map").GetComponent<Map>()._map;
		//Debug.Log(transform.parent.name + " position: " + transform.parent.position + "; collider size  x y : " + Mathf.RoundToInt(GetComponent<Collider2D>().bounds.size.x) + " " + Mathf.RoundToInt(GetComponent<Collider2D>().bounds.size.y));

		for (int i = 0; i < Mathf.RoundToInt(GetComponent<Collider2D>().bounds.size.x); i++)
		{
			for (int j = 0; j < Mathf.RoundToInt(GetComponent<Collider2D>().bounds.size.y); j++)
			{
				
				map[map.GetLength(0) - Mathf.RoundToInt(transform.parent.position.y) - 1 - j, Mathf.RoundToInt(transform.parent.position.x) - i] = 1;
			}
			
		}
		
	}

	public bool canBeBuilt()
	{
		return dropCheck;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Building")) dropCheck = false;
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.CompareTag("Building")) dropCheck = false;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Building")) dropCheck = true;
	}

	public abstract void Spawn(GameObject unit);

	}
