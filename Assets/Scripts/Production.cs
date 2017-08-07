using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///   Fills Production Menu
/// </summary>
public class Production : ContentFiller
{

	private Vector3[] v;
	public float UpperLimit;


	public float paddingY;
	public float cellSizeY;


	public GameObject[] firstTwoChildren;
	public GameObject[] lastTwoChildren;

	public Vector2 l1;
	public Vector2 l2;


	private void Start()
	{
		v = new Vector3[4];
		transform.parent.parent.parent.GetComponent<RectTransform>().GetWorldCorners(v);
		UpperLimit = v[1].y;

		var _gridLayoutGroup = GetComponent<GridLayoutGroup>();
		var _contentSizeFitter = GetComponent<ContentSizeFitter>();

		//_gridLayoutGroup.enabled = false; 
		// Previous line screwed buttons. Normally, it shouldn't have. Because, when I disable it on the editor manually, there is no problem. Thus, buttons' alignment may have problems in editor/game.
		_contentSizeFitter.enabled = false;

		paddingY = _gridLayoutGroup.spacing.y;
		cellSizeY = _gridLayoutGroup.cellSize.y;

		firstTwoChildren = new GameObject[2];
		firstTwoChildren[0] = transform.GetChild(0).gameObject;
		firstTwoChildren[1] = transform.GetChild(1).gameObject;

		lastTwoChildren = new GameObject[2];
		lastTwoChildren[0] = transform.GetChild(transform.childCount - 2).gameObject;
		lastTwoChildren[1] = transform.GetChild(transform.childCount - 1).gameObject;

		for (int i = 0; i < (Screen.height / cellSizeY + 1) - 2; i++)
		{
			var clone = Instantiate(firstTwoChildren[0], transform);
			clone.GetComponent<RectTransform>().anchoredPosition = new Vector2(lastTwoChildren[0].GetComponent<RectTransform>().anchoredPosition.x, lastTwoChildren[0].GetComponent<RectTransform>().anchoredPosition.y - cellSizeY - paddingY);
			clone.name = firstTwoChildren[0].name;
			clone.transform.SetAsLastSibling();


			clone = Instantiate(firstTwoChildren[1], transform);
			clone.GetComponent<RectTransform>().anchoredPosition = new Vector2(lastTwoChildren[1].GetComponent<RectTransform>().anchoredPosition.x, lastTwoChildren[1].GetComponent<RectTransform>().anchoredPosition.y - cellSizeY - paddingY);
			clone.name = firstTwoChildren[1].name;
			clone.transform.SetAsLastSibling();

			SetLastChildren();
		}

		GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // try to fix alignment problem in game.

		//StartCoroutine(Fill());
		// Since list is static, filling from the asset folder is disabled.
	}

	private void Update()
	{

		l1 = lastTwoChildren[0].transform.position;
		l2 = lastTwoChildren[1].transform.position;

		if (firstTwoChildren[0].transform.position.y > UpperLimit + paddingY)
		{
			firstTwoChildren[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(firstTwoChildren[0].GetComponent<RectTransform>().anchoredPosition.x, transform.GetChild(transform.childCount - 1).GetComponent<RectTransform>().anchoredPosition.y - cellSizeY - paddingY);
			firstTwoChildren[0].transform.SetAsLastSibling();


			firstTwoChildren[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(firstTwoChildren[1].GetComponent<RectTransform>().anchoredPosition.x, transform.GetChild(transform.childCount - 1).GetComponent<RectTransform>().anchoredPosition.y);
			firstTwoChildren[1].transform.SetAsLastSibling();

			SetFirstChildren();
			SetLastChildren();
		}
		else if (lastTwoChildren[0].transform.position.y < -2)
		{
			

			lastTwoChildren[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(lastTwoChildren[1].GetComponent<RectTransform>().anchoredPosition.x, firstTwoChildren[1].GetComponent<RectTransform>().anchoredPosition.y + cellSizeY + paddingY);
			lastTwoChildren[1].transform.SetAsFirstSibling();
		
			lastTwoChildren[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(lastTwoChildren[0].GetComponent<RectTransform>().anchoredPosition.x, firstTwoChildren[0].GetComponent<RectTransform>().anchoredPosition.y + cellSizeY + paddingY);
			lastTwoChildren[0].transform.SetAsFirstSibling();
		
			SetFirstChildren();
			SetLastChildren();
		}

	}

	void SetFirstChildren()
	{
		firstTwoChildren[0] = transform.GetChild(0).gameObject;
		firstTwoChildren[1] = transform.GetChild(1).gameObject;
		
		}

	void SetLastChildren()
	{
		lastTwoChildren[0] = transform.GetChild(transform.childCount - 2).gameObject;
		lastTwoChildren[1] = transform.GetChild(transform.childCount - 1).gameObject;
	}

	internal override IEnumerator Fill()
	{
		foreach (var o in Resources.LoadAll("Prefabs/Buildings", typeof(GameObject)))
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