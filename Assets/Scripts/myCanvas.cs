using UnityEngine;

public class myCanvas : MonoBehaviour
{
	public GameObject informationMenu;

	public GameObject productionMenu;

	/// <summary>
	/// to access information and production menu easily, made it singleton
	/// </summary>
	public static myCanvas instance { get; private set; }

	private void Awake()
	{
		if (instance != null && instance != this)
			Destroy(this);
		else
			instance = this;
	}
}