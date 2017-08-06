using UnityEngine;

/// <summary>
///   Takes reference from button and sends the prefab to building to spawn it.
/// </summary>
public class UnitProduction : MonoBehaviour
{
	public void Send()
	{

		//Debug.Log(transform.parent.GetComponent<Information>().sampleText.text);

		//Debug.Log("Prefabs/Units/" + transform.parent.GetComponent<Information>().sampleText.text + "/" + transform.gameObject.name);

		

		SelectMouse.instance.selected.GetComponentInChildren<Building>().Spawn(transform.gameObject.name);
	}
}