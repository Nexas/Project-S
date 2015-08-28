using UnityEngine;
using System.Collections;

public class OscillatingWave : BaseBehavior {

	bool bSweepingLeft;			// Are the shots being swept left or right?
	float fAngleToAttack;
	float fMaxAngle;
	float fMinAngle;
	public Transform eBullet;

	// Use this for initialization
	void Start () 
	{
		nBulletCount = 50;
		fAttackSpeed = .1f;
		fAttackCooldown = 1.25f;
		bReadyToAttack = false;
		fAngleToAttack = 225.0f;
		fMaxAngle = 260.0f;
		fMinAngle = 225.0f;
		bSweepingLeft = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!isActive)
			return;
		if (!bReadyToAttack)
		{
			fAttackCooldown -= Time.deltaTime;
			if (fAttackCooldown <= 0.0f)
			{
				fAttackCooldown = 1.25f;
				bReadyToAttack = true;
			}
		}
		if (bReadyToAttack)
		{
			fAttackSpeed -= Time.deltaTime;
			if (fAttackSpeed <= 0.0f && nBulletCount > 0)
			{
				float tempAngle = fAngleToAttack;
				for (int i = 0; i < 8; ++i)
				{
					Vector3 shotDir = new Vector3(0.0f, 0.0f, 1.0f);
					shotDir = Quaternion.Euler(0.0f, tempAngle, 0.0f) * shotDir;
					shotDir.Normalize();
					shotDir *= 50.0f;
					SpawnBullet(shotDir);
					tempAngle += 45;
				}
				fAttackSpeed = .1f;
				nBulletCount -= 1;
			}
			if (fAngleToAttack < fMaxAngle && bSweepingLeft)
			{
				fAngleToAttack += 0.25f;
			}
			else if (fAngleToAttack >= fMaxAngle)
			{
				bSweepingLeft = false;
				fAngleToAttack = fMaxAngle;
			}
			
			if (fAngleToAttack > fMinAngle && !bSweepingLeft)
			{
				fAngleToAttack -= 0.25f;
			}
			else if (fAngleToAttack <= fMinAngle)
			{
				bSweepingLeft = true;
				fAngleToAttack = fMinAngle;
			}
			
			
			if (nBulletCount <= 0)
			{
				bReadyToAttack = false;
				nBulletCount = 100;
				fAngleToAttack = fMinAngle;
			}
		}
	}

	void SpawnBullet(Vector3 vel)
	{
		Transform t = Instantiate(eBullet, this.transform.position, transform.rotation) as Transform;
		GameObject bul = t.gameObject;
		bul.GetComponent<EnemyBullet>().SetVelocity(vel);
	}
}
