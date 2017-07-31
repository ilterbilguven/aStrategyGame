using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// After selecting a building in production menu, building will attach itself to the mouse by sending it to the "FollowMouse".
/// </summary>
public class BuildingPicker : MonoBehaviour
{
	private FollowMouse fm;
	[SerializeField]
	private GameObject temp_building;
	public void Send()
	{
		fm = GameObject.Find("MouseScript").GetComponent<FollowMouse>();


		temp_building = null;
		temp_building = (GameObject)Instantiate(Resources.Load("Prefabs/Buildings/" + transform.gameObject.name));
		temp_building.name = transform.gameObject.name;
		fm.Pick(temp_building);

	}


}
