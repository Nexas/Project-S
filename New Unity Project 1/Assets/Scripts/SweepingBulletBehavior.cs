using UnityEngine;
using System.Collections;

public class SweepingBulletBehavior : BaseBehavior {

	bool bSecondSweep;
	float fAngleToStart;
	public Transform eDelayBullet;
	float fCurrentAngle;
	float fStopTimer;
	float fSlowTimer;

	// Use this for initialization
	void Start () {
		nBulletCount = 36;
		bSecondSweep = false;
		fAttackSpeed = 0.05f;
		fAngleToStart = 270;
		fAttackCooldown = 1.25f;
		fCurrentAngle = 270;
		fSlowTimer = 0.9f;
		fStopTimer = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
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
				fAttackSpeed = 0.05f;
				nBulletCount -= 1;
				Vector3 shotDir = new Vector3(0.0f, 0.0f, 1.0f);
				shotDir = Quaternion.Euler(0.0f, fCurrentAngle, 0.0f) * shotDir;
				shotDir.Normalize();
				shotDir *= 50.0f;
				SpawnBullet(shotDir, fSlowTimer, fStopTimer);
				fSlowTimer -= 0.05f;
				if (nBulletCount == 18)
				{
					bSecondSweep = true;
					fSlowTimer = 0.9f;
					fStopTimer = 0.0f;
					fCurrentAngle += 10.0f;
				}

				if (bSecondSweep)
					fCurrentAngle += 10.0f;
				else
					fCurrentAngle -= 10.0f;
			}
			else if (nBulletCount <= 0)
			{
				bReadyToAttack = false;
				fSlowTimer = 0.9f;
				fStopTimer = 1.0f;
				nBulletCount = 36;
				fCurrentAngle = fAngleToStart;
				bSecondSweep = false;
			}
		}
	}

	void SpawnBullet(Vector3 _vel, float _slowTimer, float _stopTimer)
	{
		Transform t = Instantiate(eDelayBullet, transform.position, transform.rotation) as Transform;
		GameObject obj = t.gameObject;
		t.GetComponent<EnemyDelayBullet>().SetVelocity(_vel);
		t.GetComponent<EnemyDelayBullet>().SetSlowTimer(_slowTimer);
		t.GetComponent<EnemyDelayBullet> ().SetStopTimer(_stopTimer);
	}
}

