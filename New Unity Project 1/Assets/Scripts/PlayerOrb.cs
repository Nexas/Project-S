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
        Vector3 pos = GameGod.playerPos;
        fAttackSpeed -= Time.deltaTime;
        if (bRightOrb)
            pos.x += 10.0f;
        else
            pos.x -= 10.0f;

        transform.position = pos;

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
