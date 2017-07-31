using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Production menu and information menu uses this structure.
/// </summary>
public abstract class ContentFiller : MonoBehaviour
{
	[SerializeField]
	internal GameObject prefab;
	public float startingPoint = 0;

	internal abstract IEnumerator Fill();

}