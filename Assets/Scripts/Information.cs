﻿using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///   When you clicked on a building. Info menu will be filled with this script.
/// </summary>
public class Information : ContentFiller
{
	[SerializeField] private Text sampleText;
	[SerializeField] private Image sampleImage;


	void OnEnable()
	{
		var title =  Instantiate(sampleText, transform);
		title.text = SelectMouse.instance.selected.name;
		var image = Instantiate(sampleImage, transform);
		image.sprite = SelectMouse.instance.selected.GetComponentInChildren<SpriteRenderer>().sprite;

		bool IsProductionTextCreated = false;

		foreach (var o in Resources.LoadAll("Prefabs/Units/" + title.text, typeof(GameObject)))
		{
			if (!IsProductionTextCreated)
			{
				var productionText = Instantiate(sampleText, transform);
				productionText.text = "Production";
				IsProductionTextCreated = true;
			}
			
			var _button = Instantiate(prefab, transform);
			_button.transform.localPosition = new Vector3(0, startingPoint, 0);
			_button.GetComponent<Button>().name = ((GameObject)o).name;
			startingPoint -= _button.GetComponent<RectTransform>().rect.height;
		}
	}

	private void OnDisable()
	{
		foreach (Transform child in transform)
		{
			Debug.Log(child.name);
			Destroy(child.gameObject);
			startingPoint = 0;
		}
	}

	internal override IEnumerator Fill()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.1f);
			Debug.Log(sampleText.text);
			if (sampleText.text != string.Empty)
			{
				foreach (var o in Resources.LoadAll("Prefabs/Units/" + sampleText.text, typeof(GameObject)))
				{
					var _button = Instantiate(prefab, transform);
					_button.transform.localPosition = new Vector3(0, startingPoint, 0);
					_button.GetComponent<Button>().name = ((GameObject) o).name;
					_button.GetComponentInChildren<Text>().text = ((GameObject) o).name;
					startingPoint -= _button.GetComponent<RectTransform>().rect.height;
				}
				break;
			}
		}
	}
}