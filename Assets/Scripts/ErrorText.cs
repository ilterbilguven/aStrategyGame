using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorText : MonoBehaviour {


	public static ErrorText instance { get; private set; }

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

	// start with empty text
	void Start()
	{
		GetComponent<Text>().text = String.Empty;

		StartCoroutine(ShowError());
	}

	/// <summary>
	/// checks if there is an error.
	/// if there is an error message, show it for 1 sec.
	/// </summary>
	/// <returns></returns>
	IEnumerator ShowError()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.1f);
			if (GetComponent<Text>().text != String.Empty)
			{
				yield return new WaitForSeconds(1);
				GetComponent<Text>().text = String.Empty;
			}
		}
	}


	/// <summary>
	/// to show the error in the text field.
	/// </summary>
	/// <param name="s">error text</param>
	public void ChangeMessage(string s)
	{
		GetComponent<Text>().text = s;
	}
}
