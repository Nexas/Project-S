﻿using UnityEngine;
using System.Collections;

public class IntersectingBeamBehavior : BaseBehavior {

	float fBeamPosX;				// The position the beams will appear on the top of the screen(x pos)
	float fBeamPosY;
	public Transform eBeam;
	public Transform eSplittingBeam;
	Vector3 camRight;
	Vector3 camLeft;
	Vector3 camTop;
	bool leftSpawn;				// Is the horizontal beam spawning on the left or right side.

	// Use this for initialization
	void Start () {
		fBeamPosX = 0.0f;
		fBeamPosY = 0.0f;
		nBulletCount = 18;
		fAttackSpeed = 0.2f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!isActive)
			return;

		camLeft = Camera.main.ScreenToWorldPoint(new Vector3 (0.0f, 0.0f, 100.0f));
		camRight = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width, 0.0f, 100.0f));
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
				Vector3 velocity = new Vector3(0.0f, 0.0f, 0.0f);
				Vector3 position = new Vector3(0.0f, 0.0f, 0.0f);
				Vector3 rotation = new Vector3(0.0f, 0.0f, 0.0f);

				// Vertical Spawning
				for (int i = 0; i < 9; ++i)
				{
					position = camTop;
					position.x += Mathf.Abs(fBeamPosX * camTop.x);
					position.y = 0.1f;
					rotation.Set(0.0f, 180.0f, 0.0f);
					SpawnBeam(velocity, position, rotation);
					nBulletCount -= 1;
					fBeamPosX += 0.2f;
				}

				for (int i = 0; i < 9; ++i)
				{
					if (leftSpawn)
					{
						position = camLeft;
						position.z += Mathf.Abs(fBeamPosY * camTop.z);
						velocity.Set(250.0f, 0.0f, 0.0f);
						rotation.Set(0.0f, 90.0f, 0.0f);
					}
					else
					{
						position = camRight;
						position.z += Mathf.Abs(fBeamPosY * camTop.z);
						velocity.Set(-250.0f, 0.0f, 0.0f);
						rotation.Set(0.0f, -90.0f, 0.0f);
					}
					SpawnBeam(velocity, position, rotation);

					fBeamPosY += 0.2f;
					leftSpawn = !leftSpawn;
					nBulletCount -= 1;
				}
				fAttackSpeed = 0.2f;
			}
			
			if (nBulletCount <= 0)
			{
				bReadyToAttack = false;
				nBulletCount = 18;
				fBeamPosX = 0.0f;
				fBeamPosY = 0.0f;
			}
		}	
	}

	void SpawnBeam(Vector3 vel, Vector3 pos, Vector3 rot)
	{
		Transform t = Instantiate(eBeam, pos, transform.rotation) as Transform;
		GameObject bul = t.gameObject;
		bul.GetComponent<EnemyBeam>().SetVelocity(vel);
		bul.transform.rotation = Quaternion.Euler(rot);
	}
	
	void SpawnSplittingBeam(Vector3 vel, Vector3 pos)
	{
		Transform t = Instantiate(eSplittingBeam, pos, transform.rotation) as Transform;
		GameObject bul = t.gameObject;
		bul.GetComponent<SplittingBeam> ().spawnTimer = 1.75f;
	}
}
