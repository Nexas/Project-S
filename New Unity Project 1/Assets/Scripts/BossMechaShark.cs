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
	float fAttackSpeed;
	public Transform eBullet;
	public OscillatingWaveBehavior phase1;
	public SweepingBulletBehavior phase2;
	public IntersectingBeamBehavior phase3;
	public Material[] materials;
	float flashTimer;			// How long to flash white.
	bool isFlashing;			// Whether or not to flash white.
    public Texture2D bgEmptyBar;       // The texture for the empty health bar.
    public Texture2D fgLifeBar;          // The texture for the health bar.

	// Use this for initialization
	void Start () {
		nMaxHealth = 900;
		nCurrentHealth = nMaxHealth;
		nPhase = 1;
		bOnScreen = false;
		phase1 = GetComponent<OscillatingWaveBehavior>();
		phase2 = GetComponent<SweepingBulletBehavior>();
		phase3 = GetComponent<IntersectingBeamBehavior>();
		flashTimer = 0.05f;
		isFlashing = false;
		fAttackSpeed = 0.25f;
	}
	
	// Update is called once per frame
	void Update () {
		camLeft = Camera.main.ScreenToWorldPoint(new Vector3 (0.0f, 0.0f, 100.0f));
		camRight = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width, 0.0f, 100.0f));
		camTop = Camera.main.ScreenToWorldPoint(new Vector3 (0.0f, Screen.height, 100.0f));
		camBot = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 100.0f));

		fAttackSpeed -= Time.deltaTime;

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

		if (GameGod.playerPos.z > transform.position.z && fAttackSpeed <= 0.0f)
		{
			Vector3 toPlayer = GameGod.playerPos;
			toPlayer -= transform.position;
			toPlayer.Normalize();
			toPlayer *= 50.0f;
			fAttackSpeed = 0.25f;
			SpawnBullet(toPlayer);
		}

		// TODO: Remove this testing code.
		if (Input.GetKey(KeyCode.T))
		{
			nPhase = 2;
			nCurrentHealth = (int)(nMaxHealth * .66f);
			phase3.SetActive(false);
			phase2.SetActive(true);
			phase1.SetActive(false);
		}
		if (Input.GetKey(KeyCode.U))
		{
			nPhase = 3;
			nCurrentHealth = (int)(nMaxHealth * .33f);
			phase1.SetActive(false);
			phase2.SetActive(false);
			phase3.SetActive(true);
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
				phase3.SetActive(false);
				this.GetComponent<ParticleSystem>().Play();
			}
			else if (nCurrentHealth <= (nMaxHealth * .33f) && nPhase < 3)
			{
				nPhase = 3;
				phase3.SetActive(true);
				phase2.SetActive(false);
				phase1.SetActive(false);
				this.GetComponent<ParticleSystem>().Play();
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

    void OnGUI()
    {
        if (bOnScreen && nCurrentHealth > 0)
        {
			GUI.depth = -1;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, 16), bgEmptyBar, ScaleMode.StretchToFill);
			GUI.depth = 0;
            GUI.DrawTexture(new Rect(0, 0, ((float)nCurrentHealth / (float)nMaxHealth) * (float)Screen.width, 16), fgLifeBar);
        }
    }

	void SpawnBullet(Vector3 vel)
	{
		Transform t = Instantiate(eBullet, this.transform.position, transform.rotation) as Transform;
		GameObject bul = t.gameObject;
		bul.GetComponent<EnemyBullet>().SetVelocity(vel);	
	}
}
