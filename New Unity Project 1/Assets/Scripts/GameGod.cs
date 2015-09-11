using UnityEngine;
using System.Collections;

public class GameGod : MonoBehaviour {

	static public Vector3 playerPos;
	static public Quaternion playerRot;
	static public bool playerDead;
	static float respawnTimer;
	static public float fGameTimer;			// A global timer that simply increments over time.
	public Transform obj;
	static public int lives; 
	static public int bombs;
	static public bool bBossAlive;
	static public bool bLevelScrolling;
	static public bool bIsPaused;
	static public bool bIsCutscenePlaying;
	static bool created;
	static int level1ApplicationNumber;		// The number within the Unity Build that signifies the first level of gameplay.
	public enum eScriptNames {NONE = 0, LEFT_TO_RIGHT, RIGHT_TO_LEFT, SIN_WAVE,NUM_SCRIPTS};

	void Awake()
	{
		if (created)
			return;
		DontDestroyOnLoad (this);
	}

	// Use this for initialization
	void Start () {
		created = true;
		playerDead = false;
		respawnTimer = 2.0f;
		lives = 3;
		bombs = 3;
		bBossAlive = true;
		bLevelScrolling = true;
		level1ApplicationNumber = 2;
	}
	
	// Update is called once per frame
	void Update () {
		fGameTimer += Time.deltaTime;

		if (lives <= 0)
		{
			Restart();
			return;
		}

		if (!bBossAlive)
		{
			bBossAlive = true;
			bLevelScrolling = true;
			Application.LoadLevel(Application.loadedLevel + 1);
		}

		if (playerDead)
		{
			respawnTimer -= Time.deltaTime;
			if (respawnTimer <= 0.0f)
			{
				respawnTimer = 2.0f;
				playerDead = false;
				Instantiate(obj, playerPos, playerRot);
			}
		}
		else{
			if (Application.loadedLevel >= level1ApplicationNumber)
			playerPos = GameObject.FindWithTag("player").transform.position;
		}
	}

	void Restart()
	{
		playerDead = false;
		respawnTimer = 2.0f;
		lives = 3;
		bombs = 3;
		bBossAlive = true;
		bLevelScrolling = true;
		Application.LoadLevel(0);
	}

	public static GameObject GetLibraryScript(eScriptNames _script, Transform _t)
	{
		int scriptInt = (int)_script;

		GameObject enemy = _t.gameObject;
		switch (scriptInt)
		{
		case 1:
		{
			enemy.AddComponent<LeftToRightPattern>();
			break;
		}

		case 2:
		{
			enemy.AddComponent<RightToLeftPattern>();
			break;
		}

		case 3:
		{
			enemy.AddComponent<SinWavePattern>();
			break;
		}
		}

		return enemy;
	}
}
