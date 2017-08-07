using System.Collections;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// to show errors 
/// </summary>
public class ErrorText : MonoBehaviour
{
	/// <summary>
	/// to access errortext easily, made it singleton
	/// </summary>
	public static ErrorText instance { get; private set; }

	private void Awake()
	{
		if (instance != null && instance != this)
			Destroy(this);
		else
			instance = this;
	}

	// start with empty text
	private void Start()
	{
		GetComponent<Text>().text = string.Empty;

		StartCoroutine(ShowError());
	}

	/// <summary>
	///   checks if there is an error.
	///   if there is an error message, show it for 1 sec.
	/// </summary>
	/// <returns></returns>
	private IEnumerator ShowError()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.1f);
			if (GetComponent<Text>().text != string.Empty)
			{
				yield return new WaitForSeconds(1);
				GetComponent<Text>().text = string.Empty; // show the message 1for 1 sec.
			}
		}
	}


	/// <summary>
	///   to show the error in the text field.
	/// </summary>
	/// <param name="s">error text</param>
	public void ChangeMessage(string s)
	{
		GetComponent<Text>().text = s;
	}
}