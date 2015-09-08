using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	Vector3 moveVec;
	GameObject obj;
	public Transform bullet;
	public Transform effect;
	public Transform powerup;
	public Transform orb;
	public Transform bomb;
	GameObject orb1;
	GameObject orb2;
	bool isInvulnerable;

	int iWeaponLevel;
	int iPowerupCounter;
	bool bSecondWeaponSpawned;

	float invulnerableTimer;
	float halfScreenWidth;
	float halfScreenHeight;
	float fAttackSpeed;
	float fAttackTimer;

	Vector3 camLeft;
	Vector3 camTop;
	Vector3 camRight;
	Vector3 camBottom;

	void Awake()
	{
		GetComponent<ParticleSystem>().Play();
	}

	// Use this for initialization
	void Start () {
		moveVec = new Vector3(0.0f, 0.0f, 0.0f);
		isInvulnerable = true;
		invulnerableTimer = 2.0f;
		fAttackSpeed = 0.08f;
		fAttackTimer = 0.0f;
		float halfScreenWidth = Screen.width / 2.0f;
		float halfScreenHeight = Screen.height / 2.0f;
		iWeaponLevel = 1;
		iPowerupCounter = 0;
		bSecondWeaponSpawned = false;
	}
	
	// Update is called once per frame
	void Update () {
		camLeft = Camera.main.ScreenToWorldPoint(new Vector3 (0.0f, 0.0f, 100.0f));
		camTop = Camera.main.ScreenToWorldPoint(new Vector3 (0.0f, Screen.height, 100.0f));
		camRight = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width, 0.0f, 100.0f));
		camBottom = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 100.0f));


		if (isInvulnerable)
		{
			invulnerableTimer -= Time.deltaTime;

			if (invulnerableTimer <= 1.0f)
			{
				GetComponent<ParticleSystem>().Stop();
			}
			if (invulnerableTimer <= 0.0f)
			{
				isInvulnerable = false;
				invulnerableTimer = 2.0f;

			}
		}

		fAttackTimer += Time.deltaTime;

		moveVec.x = 0.0f;
		moveVec.y = 0.0f;
		if (GameGod.bLevelScrolling)
			moveVec.z = 10.0f;
		else
			moveVec.z = 0.0f;
		InputMove();
		transform.Translate (moveVec * Time.deltaTime);
		if (orb1)
			orb1.transform.Translate (moveVec * Time.deltaTime);
		if (orb2)
			orb2.transform.Translate (moveVec * Time.deltaTime);
		CheckBounds ();
	}

	void InputMove()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.LoadLevel(0);
		}

		if (!GameGod.playerDead)
		{
			if (OptionsMenu.isKeyboard)
			{
				if (Input.GetKey(KeyCode.UpArrow))
				{
					moveVec.z += 50.0f;
				}
				if (Input.GetKey(KeyCode.LeftArrow))
				{
					moveVec.x -= 50.0f;
				}
				if (Input.GetKey(KeyCode.DownArrow))
				{
					moveVec.z -= 50.0f;
				}
				if (Input.GetKey(KeyCode.RightArrow))
				{
					moveVec.x += 50.0f;
				}
				if (Input.GetKey(KeyCode.Z))
				{
					if (fAttackTimer >= fAttackSpeed)
					{
						SpawnBullet(transform.position, new Vector3(0.0f, 0.0f, 100.0f));
						if (iWeaponLevel >= 2)
							SpawnBullet(new Vector3(transform.position.x - 4.0f, transform.position.y, transform.position.z), new Vector3(-25.0f, 0.0f, 100.0f));
						if (iWeaponLevel == 3)
							SpawnBullet(new Vector3(transform.position.x + 4.0f, transform.position.y, transform.position.z), new Vector3(25.0f, 0.0f, 100.0f));
						fAttackTimer = 0.0f;
					}
					//SpawnBullet(new Vector3(transform.position.x - 4.0f, transform.position.y, transform.position.z), new Vector3(-50.0f, 0.0f, 100.0f));
					//SpawnBullet(new Vector3(transform.position.x + 4.0f, transform.position.y, transform.position.z), new Vector3(50.0f, 0.0f, 100.0f));
				}
				if (Input.GetKeyDown(KeyCode.X))
				{
					if (GameGod.bombs > 0)
					{
						Instantiate(bomb, transform.position, transform.rotation);
						GameGod.bombs -= 1;
					}
				}
			}
			else
			{
				Vector3 mPos = Input.mousePosition;
				mPos.z = 100.0f;
				Vector3 mouse = Camera.main.ScreenToWorldPoint(mPos);
				transform.position = Vector3.MoveTowards(transform.position, mouse, 100000.0f * Time.deltaTime);

				if (Input.GetKey(KeyCode.Mouse0))
				{
					if (fAttackTimer >= fAttackSpeed)
					{
						SpawnBullet(transform.position, new Vector3(0.0f, 0.0f, 100.0f));
						if (iWeaponLevel >= 2)
							SpawnBullet(new Vector3(transform.position.x - 4.0f, transform.position.y, transform.position.z), new Vector3(-25.0f, 0.0f, 100.0f));
						if (iWeaponLevel == 3)
							SpawnBullet(new Vector3(transform.position.x + 4.0f, transform.position.y, transform.position.z), new Vector3(25.0f, 0.0f, 100.0f));
						fAttackTimer = 0.0f;
					}

				}
				if (Input.GetKeyDown(KeyCode.Mouse1))
				{
					if (GameGod.bombs > 0)
					{
						Instantiate(bomb, transform.position, transform.rotation);
						GameGod.bombs -= 1;
					}
				}
			}
		}
	}

	void SpawnBullet(Vector3 pos, Vector3 vel)
	{
		//Object t = Instantiate(bullet, this.transform.position, transform.rotation) as Transform;
		//GameObject bul = (GameObject)t;
		Transform t = Instantiate(bullet, pos, transform.rotation) as Transform;
		GameObject bul = t.gameObject;
		bul.GetComponent<Bullet>().SetVelocity(vel);

	}

	void OnTriggerEnter(Collider collider)
	{
		if ((tag == "player" && collider.tag == "enemyBullet") || (tag == "player" && collider.tag == "enemy"))
		{
			Destroy(collider.gameObject);
			if (!isInvulnerable)
			{
				if (!GameGod.playerDead)
					GameGod.lives -= 1;
				GameGod.playerDead = true;
				GameGod.playerPos = transform.position;
				GameGod.playerRot = transform.rotation;
				Instantiate(effect, transform.position, transform.rotation);
				SpawnPowerups();
				Destroy (gameObject);
				orb1 = null;
				orb2 = null;
			}
		}

		if (tag == "player" && collider.tag == "Boss")
		{
			if (!isInvulnerable)
			{
				if (!GameGod.playerDead)
					GameGod.lives -= 1;
				GameGod.playerDead = true;
				GameGod.playerPos = transform.position;
				GameGod.playerRot = transform.rotation;
				Instantiate(effect, transform.position, transform.rotation);
				SpawnPowerups();
				Destroy (gameObject);
				orb1 = null;
				orb2 = null;
			}
		}

		if (tag == "player" && collider.tag == "weaponPowerup")
		{
			Destroy(collider.gameObject);
			iPowerupCounter += 1;
			if (iPowerupCounter >= 15 && iWeaponLevel < 5)
			{
				Vector3 orbPosition = transform.position;

				iWeaponLevel += 1;
				iPowerupCounter = 0;
				if (iWeaponLevel == 4)
				{
					orbPosition.x += 10.0f;
					Transform t = Instantiate(orb, orbPosition, transform.rotation) as Transform;
					GameObject o = t.gameObject;
					o.GetComponent<PlayerOrb>().bRightOrb = true;
					orb1 = o;
				}
				else if (iWeaponLevel == 5)
				{
					orbPosition.x -= 10.0f;
					if (!bSecondWeaponSpawned)
					{
						GameObject firstOrb = GameObject.FindGameObjectWithTag("Orb");
						firstOrb.transform.position = new Vector3(transform.position.x + 10.0f, transform.position.y, transform.position.z);
						orb1.transform.position = new Vector3(transform.position.x + 10.0f, transform.position.y, transform.position.z);
						bSecondWeaponSpawned = true;
					}
					Transform t = Instantiate(orb, orbPosition, transform.rotation) as Transform;
					GameObject o = t.gameObject;
					o.GetComponent<PlayerOrb>().bRightOrb = false;
					orb2 = o;
				}
			}
		}

		if (tag == "player" && collider.tag == "Environment")
		{
			if (!isInvulnerable)
			{
				if (!GameGod.playerDead)
					GameGod.lives -= 1;
				GameGod.playerDead = true;
				GameGod.playerPos = transform.position;
				GameGod.playerRot = transform.rotation;
				Instantiate(effect, transform.position, transform.rotation);
				SpawnPowerups();
				Destroy (gameObject);
			}
		}
	}

	void SpawnPowerups()
	{
		int spawnNumber = 0;
		if (iWeaponLevel > 1)
			spawnNumber = 15;
		else
			spawnNumber = iPowerupCounter;
		for (int i = 0; i < spawnNumber; ++i)
		{
			Vector3 direction = new Vector3(Random.Range(-10.0f, 10.0f), 0.0f, Random.Range(-10.0f, 10.0f));
			Transform t = Instantiate(powerup, transform.position, transform.rotation) as Transform;
			GameObject pow = t.gameObject;
			pow.GetComponent<WeaponPowerup>().SetVelocity(direction);
		}
	}

	void CheckBounds()
	{
		float offsetX = 0.0f;
		float offsetZ = 0.0f;

		if (transform.position.x + 5.0f >= camRight.x)
		{
			offsetX = -(Mathf.Abs(transform.position.x - camRight.x  + 5.0f));
		}
		if (transform.position.x - 5.0f <= camLeft.x)
		{
			offsetX = Mathf.Abs(transform.position.x - 5.0f - camLeft.x);
			//transform.position.Set(camLeft.x, transform.position.y, transform.position.z);
		}
		if (transform.position.z + 5.0f >= camTop.z)
		{
			offsetZ = -(Mathf.Abs(transform.position.z - camTop.z + 5.0f));
		}
		if (transform.position.z - 5.0f <= camBottom.z)
		{
			offsetZ = (Mathf.Abs(transform.position.z - camBottom.z - 5.0f));
		}
		transform.Translate(new Vector3(offsetX, 0.0f, offsetZ));
	}
}
