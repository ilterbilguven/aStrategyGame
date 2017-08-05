using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myCanvas : MonoBehaviour
{

	public GameObject productionMenu;
	public GameObject informationMenu;


	public static myCanvas instance { get; private set; }

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
}
