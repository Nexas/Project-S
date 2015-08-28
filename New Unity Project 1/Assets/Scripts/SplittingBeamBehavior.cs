using UnityEngine;
using System.Collections;

public class SplittingBeamBehavior : BaseBehavior {

	float fBeamPos;				// The position the beams will appear on the top of the screen(x pos)
	public Transform eBeam;
	public Transform eSplittingBeam;
	Vector3 camTop;

	// Use this for initialization
	void Start () {
		fBeamPos = 0.0f;
		nBulletCount = 10;
		fAttackSpeed = 0.2f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!isActive)
			return;

		camTop = Camera.main.ScreenToWorldPoint(new Vector3 (0.0f, Screen.height, 100.0f));

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
				fAttackSpeed = 0.2f;
				Vector3 velocity = new Vector3(0.0f, 0.0f, -250.0f);
				Vector3 position = camTop;
				position.x += Mathf.Abs(fBeamPos * position.x);
				position.y = 0.1f;
				SpawnBeam(velocity, position);
				nBulletCount -= 1;
				fBeamPos += 0.2f;
			}
			
			if (nBulletCount <= 0)
			{
				bReadyToAttack = false;
				nBulletCount = 10;
				fBeamPos = 0.25f;
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
