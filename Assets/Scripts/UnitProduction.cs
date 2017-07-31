using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitProduction : MonoBehaviour
{
	public SelectMouse building;
	public void Send()
	{
		building = GameObject.Find("MouseScript").GetComponent<SelectMouse>();

		Debug.Log(transform.parent.GetComponent<Information>().title.text);

		Debug.Log("Prefabs/Units/" + transform.parent.GetComponent<Information>().title.text + "/" + transform.gameObject.name);

		building.selected.transform.Find("Collider").GetComponent<Building>().Spawn((GameObject)Resources.Load("Prefabs/Units/" + transform.parent.GetComponent<Information>().title.text + "/" + transform.gameObject.name));

	}
}
