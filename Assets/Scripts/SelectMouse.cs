using UnityEngine;

/// <summary>
///   To select objects with mouse and control them.
/// </summary>
public class SelectMouse : MonoBehaviour
{
	public delegate void RightClickAction(Vector3 pos); // delegate for right click events
	public static event RightClickAction onClick; 


	public Vector3 pos;
	public GameObject selected;

	/// <summary>
	/// to access easily, singleton is used.
	/// </summary>
	public static SelectMouse instance { get; private set; }

	private void Awake()
	{
		if (instance != null && instance != this)
			Destroy(this);
		else
			instance = this;
	}

	private void Update()
	{
		pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pos = new Vector3(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), 0); // get the real 2D integer position
		if (Input.GetMouseButtonDown(0))
		{
			if (Physics2D.OverlapPoint(pos) == null) // if there is nothing, clear!
			{
				selected = null;
				Debug.Log("null is selected.");
				clearDelegate();
				myCanvas.instance.informationMenu.SetActive(false);
			} // else if an object has OnMouseDown() method, it will handle
		}
		else if (Input.GetMouseButtonDown(1)) // right click
		{
			if (onClick != null)
				onClick(pos); // invoke delegate
		}
	}

	public void clearDelegate()
	{
		onClick = null;
	}
}