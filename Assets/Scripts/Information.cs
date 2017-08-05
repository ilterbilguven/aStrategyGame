using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///   When you clicked on a building. Info menu will be filled with this script.
/// </summary>
public class Information : ContentFiller
{
	[SerializeField] internal Text title;

	public static Information instance { get; private set; }

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


	void OnEnable()
	{
		title.text = SelectMouse.instance.selected.name;
		foreach (var o in Resources.LoadAll("Prefabs/Units/" + title.text, typeof(GameObject)))
		{
			var _button = Instantiate(prefab, transform);
			_button.transform.localPosition = new Vector3(0, startingPoint, 0);
			_button.GetComponent<Button>().name = ((GameObject)o).name;
			_button.GetComponentInChildren<Text>().text = ((GameObject)o).name;
			startingPoint -= _button.GetComponent<RectTransform>().rect.height;
		}
	}

	private void OnDisable()
	{
		foreach (Transform child in transform)
		{
			Debug.Log(child.name);
			Destroy(child.gameObject);
		}
	}

	internal override IEnumerator Fill()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.1f);
			Debug.Log(title.text);
			if (title.text != string.Empty)
			{
				foreach (var o in Resources.LoadAll("Prefabs/Units/" + title.text, typeof(GameObject)))
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