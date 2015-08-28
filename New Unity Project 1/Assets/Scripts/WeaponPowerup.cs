using UnityEngine;
using System.Collections;

public class WeaponPowerup : MonoBehaviour {

	float moveTimer;
	Vector3 vel;

	// Use this for initialization
	void Start () {
		moveTimer = 2.0f;
	}

	public void SetVelocity(Vector3 _vel)
	{
		vel.x = _vel.x;
		vel.y = _vel.y;
		vel.z = _vel.z;
	}
	
	// Update is called once per frame
	void Update () {
		if (moveTimer > 0.0f)
		{
			moveTimer -= Time.deltaTime;
			transform.Translate(vel * Time.deltaTime);
		}

		Vector3 toPlayer = GameGod.playerPos - transform.position;
		float step = 32.5f * Time.deltaTime;

		if (toPlayer.magnitude <= 30.0f && GameGod.playerDead == false)
		{
			transform.position = Vector3.MoveTowards(transform.position, GameGod.playerPos, step);
		}
	}


}
