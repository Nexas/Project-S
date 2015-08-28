using UnityEngine;
using System.Collections;

public class HorizontalLineSpawner : MonoBehaviour {

	// All these variables must be set per spawner in the inspector.
	public float spawnOffset;	// Time in seconds in between each spawned enemy.
	public int numRows;		// Number of rows to spawn.
	public Transform obj;		// Enemy type this spawner spawns.
	public GameGod.eScriptNames names;
	
	// These variables will be set every frame.
	Vector3 camLeft;
	Vector3 camTop;
	Vector3 camRight;
	Vector3 camBottom;
	
	// These variables will be set on conditions.
	bool isSpawning;
	float spawnTimer;
	
	
	// Use this for initialization
	void Start () {
		isSpawning = false;
		spawnTimer = spawnOffset;
	}
	
	// Update is called once per frame
	void Update () {

		camLeft = Camera.main.ScreenToWorldPoint(new Vector3 (0.0f, 0.0f, 100.0f));
		camTop = Camera.main.ScreenToWorldPoint(new Vector3 (0.0f, Screen.height, 100.0f));
		camRight = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width, 0.0f, 100.0f));
		camBottom = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 100.0f));
		
		if (camTop.z + 2.0f >= transform.position.z)
		{
			if (!isSpawning)
			{
				Vector3 spawnPoint = new Vector3(camRight.x - (Screen.width * .01f), 0.1f, transform.position.z);
				transform.position = spawnPoint;
			}
			
			isSpawning = true;
		}
		
		if (isSpawning)
		{
			transform.Translate(new Vector3(0.0f, 0.0f, 10.0f * Time.deltaTime));
			
			spawnTimer += Time.deltaTime;
			
			if (spawnTimer >= spawnOffset && numRows > 0)
			{
				Vector3 spawnPoint = new Vector3(-15.0f, 0.0f, camTop.z);
				Vector3 spawnPoint2 = new Vector3(0.0f, 0.0f, camTop.z);
				Vector3 spawnPoint3 = new Vector3(15.0f, 0.0f, camTop.z);
				spawnTimer = 0.0f;

				SpawnUnit(spawnPoint);
				SpawnUnit(spawnPoint2);
				SpawnUnit(spawnPoint3);
				numRows -= 1;
			}
			else if (numRows <= 0)
			{
				Destroy(this.gameObject);
			}
		}
	}

	void SpawnUnit(Vector3 pos)
	{
		Transform t = Instantiate(obj, pos, transform.rotation) as Transform;
		GameObject enemy = GameGod.GetLibraryScript(names, t);
	}
}
