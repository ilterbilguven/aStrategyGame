using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Fills Production Menu
/// </summary>
public class Production : ContentFiller
{

	void Start()
	{
		StartCoroutine(Fill());
	}

	internal override IEnumerator Fill()
	{
		foreach (Object o in Resources.LoadAll("Prefabs/Buildings", typeof(GameObject)))
		{
			var _button = Instantiate(prefab, transform);
			_button.transform.localPosition = new Vector3(0, startingPoint, 0);
			_button.GetComponent<Button>().name = ((GameObject) o).name;
			_button.GetComponentInChildren<Text>().text = ((GameObject) o).name;
			startingPoint -= _button.GetComponent<RectTransform>().rect.height;
		}
		yield return new WaitForSeconds(0.01f);
	}
}
