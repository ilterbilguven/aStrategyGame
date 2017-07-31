using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class Information : ContentFiller
{

	[SerializeField] internal Text title;
	void Start()
	{
		title.text = GameObject.Find("MouseScript").GetComponent<SelectMouse>().selected.name;
		StartCoroutine(Fill());
	}

	internal override IEnumerator Fill()
	{
		while (true)
		{
		yield return new WaitForSeconds(0.1f);
			Debug.Log(title.text);
			if (title.text != String.Empty)
			{
				foreach (Object o in Resources.LoadAll("Prefabs/Units/" + title.text, typeof(GameObject)))
				{
					var _button = Instantiate(prefab, transform);
					_button.transform.localPosition = new Vector3(0, startingPoint, 0);
					_button.GetComponent<Button>().name = ((GameObject)o).name;
					_button.GetComponentInChildren<Text>().text = ((GameObject)o).name;
					startingPoint -= _button.GetComponent<RectTransform>().rect.height;
				}
				break;
			}			
		}


	}
}
