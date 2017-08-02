using System.Collections;
using UnityEngine;

/// <summary>
///   Production menu and information menu uses this structure.
/// </summary>
public abstract class ContentFiller : MonoBehaviour
{
	[SerializeField] internal GameObject prefab;

	public float startingPoint;

	internal abstract IEnumerator Fill();
}