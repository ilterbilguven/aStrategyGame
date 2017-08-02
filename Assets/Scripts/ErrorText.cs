using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorText : MonoBehaviour {

	void Start()
	{
		GetComponent<Text>().text = String.Empty;

		StartCoroutine(ShowError());
	}

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

	public void ChangeMessage(string s)
	{
		GetComponent<Text>().text = s;
	}
}
