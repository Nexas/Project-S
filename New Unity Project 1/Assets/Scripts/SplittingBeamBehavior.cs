using UnityEngine;
using System.Collections;

public class SplittingBeamBehavior : BaseBehavior {

	float fBeamPos;				// The position the beams will appear on the top of the screen(x pos)
	public Transform eBeam;
	public Transform eSplittingBeam;
	Vector3 camTop;
	Vector3 camRight;

	// Use this for initialization
	void Start () {
		fBeamPos = 0.0f;
		nBulletCount = 5;
		fAttackSpeed = 1.0f;
		fAttackCooldown = 1.5f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!isActive)
			return;

		camTop = Camera.main.ScreenToWorldPoint(new Vector3 (0.0f, Screen.height, 100.0f));
		camRight = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width, Screen.height, 100.0f));

		if (!bReadyToAttack)
		{
			fAttackCooldown -= Time.deltaTime;
			if (fAttackCooldown <= 0.0f)
			{
				fAttackCooldown = 1.5f;
				bReadyToAttack = true;
			}
		}

		if (bReadyToAttack)
		{
			fAttackSpeed -= Time.deltaTime;
			if (fAttackSpeed <= 0.0f && nBulletCount > 0)
			{
				for (int i = 0; i < 5; ++i)
				{
					fAttackSpeed = 1.0f;
					Vector3 velocity = new Vector3(0.0f, 0.0f, -250.0f);
					Vector3 position = camTop;
					position.x += Mathf.Abs((camRight.x - camTop.x) * fBeamPos);
					position.y = 0.1f;
					SpawnSplittingBeam(velocity, position);
					nBulletCount -= 1;
					fBeamPos += 0.2f;
				}
			}
			
			if (nBulletCount <= 0)
			{
				bReadyToAttack = false;
				nBulletCount = 5;
				fBeamPos = 0.1f;
			}
		}
	}

	void SpawnBeam(Vector3 vel, Vector3 pos)
	{
		Transform t = Instantiate(eBeam, pos, transform.rotation) as Transform;
		GameObject bul = t.gameObject;
		bul.GetComponent<EnemyBeam>().SetVelocity(vel);
		bul.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
	}
	
	void SpawnSplittingBeam(Vector3 vel, Vector3 pos)
	{
		Transform t = Instantiate(eSplittingBeam, pos, transform.rotation) as Transform;
		GameObject bul = t.gameObject;
		bul.GetComponent<SplittingBeam> ().spawnTimer = 1.75f;
	}
}
