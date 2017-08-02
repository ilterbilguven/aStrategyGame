using System;
using UnityEngine;

public class PowerPlant : Building
{
	/// <summary>
	///   PowerPlant has nothing to spawn.
	/// </summary>
	/// <param name="unit"></param>
	public override void Spawn(string unit)
	{
		throw new NotImplementedException();
	}
}