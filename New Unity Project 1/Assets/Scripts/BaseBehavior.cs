using UnityEngine;
using System.Collections;

public class BaseBehavior : MonoBehaviour {

	protected bool isActive = false;
	protected float fAttackSpeed;
	protected float fAttackCooldown;
	protected bool bReadyToAttack;
	protected int nBulletCount;

	// Use this for initialization
	void Start () 
	{
	}
	
	public void SetActive(bool active)
	{
		isActive = active;
	}
}
