using UnityEngine;
using System.Collections;

public class PlayerOrb : MonoBehaviour {

	public Transform knife;
	public bool bRightOrb;
	float fAttackSpeed;
	float fOrbitDistance;

	// Use this for initialization
	void Start () {
		fAttackSpeed = 0.24f;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 playerPos = GameGod.playerPos;
		fAttackSpeed -= Time.deltaTime;
		//if (bRightOrb)
			fOrbitDistance = 10.0f;
		//else
			//fOrbitDistance = -10.0f;

		transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
		transform.position = GameGod.playerPos + (transform.position - GameGod.playerPos).normalized * fOrbitDistance;
		transform.RotateAround(GameGod.playerPos, Vector3.up, 180 * Time.deltaTime);
		//transform.position = playerPos;

		//transform.position = target.position + (transform.position - target.position).normalized * orbitDistance;
		//transform.RotateAround(target.position, Vector3.up, orbitDegreesPerSec * Time.deltaTime);

		/*Vector3 relativePos = (GameGod.playerPos + new Vector3(0, 1.5f, 0)) - transform.position;
		Quaternion rotation = Quaternion.LookRotation(relativePos);
		
		Quaternion current = transform.localRotation;
		
		transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime);
		transform.Translate (0, 0, 3 * Time.deltaTime);
		if (bRightOrb)
			transform.Translate(10.0f * Time.deltaTime, 0, 0);
		else
			transform.Translate(-10.0f * Time.deltaTime, 0, 0);*/

		if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Mouse0))
		{
			if (fAttackSpeed <= 0.0f)
			{
				Instantiate(knife, transform.position, transform.rotation);
				fAttackSpeed = 0.24f;
			}
		}

		if (GameGod.playerDead)
			Destroy (gameObject);
	}
}
