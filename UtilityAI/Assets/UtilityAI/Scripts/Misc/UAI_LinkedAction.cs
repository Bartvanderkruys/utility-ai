using UnityEngine;
using System;

[Serializable]
public class UAI_LinkedAction{
	public UAI_Action action;
	public bool actionEnabled = true;
	public float cooldown = 0.0f;
	public float cooldownTimer = 0.0f;
}
