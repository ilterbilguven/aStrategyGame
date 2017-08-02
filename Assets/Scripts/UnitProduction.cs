using UnityEngine;

/// <summary>
///   Takes reference from button and sends the prefab to building to spawn it.
/// </summary>
public class UnitProduction : MonoBehaviour
{
	public SelectMouse building;

	public void Send()
	{
		building = GameObject.Find("MouseScript").GetComponent<SelectMouse>();

		//Debug.Log(transform.parent.GetComponent<Information>().title.text);

		//Debug.Log("Prefabs/Units/" + transform.parent.GetComponent<Information>().title.text + "/" + transform.gameObject.name);

		building.selected.transform.Find("Collider").GetComponent<Building>().Spawn(transform.gameObject.name);
	}
}