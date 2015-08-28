using UnityEngine;
using System.Collections;

public class BossMechaShark : MonoBehaviour {

	int nMaxHealth;
	int nCurrentHealth;
	int nPhase;			// The phase the boss is in
	bool bOnScreen;				// Do not act until the boss is on screen.
	Vector3 camRight;
	Vector3 camLeft;
	Vector3 camBot;
	Vector3 camTop;
	public OscillatingWave phase1;
	public SplittingBeamBehavior phase2;
	public IntersectingBeamBehavior phase3;
	public Material[] materials;
	float flashTimer;			// How long to flash white.
	bool isFlashing;			// Whether or not to flash white.

	// Use this for initialization
	void Start () {
		nMaxHealth = 900;
		nCurrentHealth = nMaxHealth;
		nPhase = 1;
		bOnScreen = false;
		phase1 = GetComponent<OscillatingWave>();
		phase2 = GetComponent<SplittingBeamBehavior>();
		phase3 = GetComponent<IntersectingBeamBehavior>();
		flashTimer = 0.05f;
		isFlashing = false;
	}
	
	// Update is called once per frame
	void Update () {
		camLeft = Camera.main.ScreenToWorldPoint(new Vector3 (0.0f, 0.0f, 100.0f));
		camRight = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width, 0.0f, 100.0f));
		camTop = Camera.main.ScreenToWorldPoint(new Vector3 (0.0f, Screen.height, 100.0f));
		camBot = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 100.0f));

		if (!bOnScreen && transform.position.z + 7.5f < camTop.z)
		{
			bOnScreen = true;
			phase1.SetActive(true);
		}

		if (isFlashing)
		{
			flashTimer -= Time.deltaTime;
			if (flashTimer <= 0.0f)
			{
				flashTimer = 0.05f;
				isFlashing = false;
				GetComponent<Renderer>().material = materials[0];
			}
		}

		// TODO: Remove this testing code.
		if (Input.GetKey(KeyCode.T))
		{
			nPhase = 2;
			nCurrentHealth = (int)(nMaxHealth * .66f);
			phase3.SetActive(true);
			phase1.SetActive(false);
		}
		if (Input.GetKey(KeyCode.U))
		{
			nPhase = 3;
			nCurrentHealth = (int)(nMaxHealth * .33f);
		}
	}

	int GetHealth()
	{
		return nCurrentHealth;
	}

	void SetHealth(int nHealth)
	{
		nCurrentHealth = nHealth;
	}



	void OnTriggerEnter(Collider collision)
	{
		if (tag == "enemy" && collision.tag == "enemyBullet")
		{
			// Do nothing
		}
		
		if (tag == "Boss" && collision.tag == "playerBullet")
		{
			Destroy(collision.gameObject);
			if (bOnScreen)					// This is to ensure that player bullets don't hit the boss early
				SetHealth(GetHealth() - 1);

			if (nCurrentHealth <= (nMaxHealth * .66f) && nCurrentHealth > (nMaxHealth * .33f) && nPhase < 2)
			{
				nPhase = 2;
				phase2.SetActive(true);
				phase1.SetActive(false);
			}
			else if (nCurrentHealth <= (nMaxHealth * .33f) && nPhase < 3)
			{
				nPhase = 3;
			}
			else if (nCurrentHealth <= 0.0f)
			{
				GameGod.bBossAlive = false;
				Application.LoadLevel(0);

			}

			if (!isFlashing && bOnScreen)
			{
				isFlashing = true;
				GetComponent<Renderer>().material = materials[1];
			}
		}
	}
}
