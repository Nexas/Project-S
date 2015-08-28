using UnityEngine;
using System.Collections;

public class LeftSpawner : MonoBehaviour {

	// All these variables must be set per spawner in the inspector.
	public float spawnOffset;	// Time in seconds in between each spawned enemy.
	public int spawnNumber;		// Number of enemies to spawn.
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
				Vector3 spawnPoint = new Vector3(camLeft.x + (Screen.width * .01f), 0.1f, transform.position.z);
				transform.position = spawnPoint;
			}
			
			isSpawning = true;
		}
		
		if (isSpawning)
		{
			transform.Translate(new Vector3(0.0f, 0.0f, 10.0f * Time.deltaTime));
			
			spawnTimer += Time.deltaTime;
			
			if (spawnTimer >= spawnOffset && spawnNumber > 0)
			{
				Vector3 spawnPoint = new Vector3(camLeft.x, 0.0f, camTop.z);
				spawnTimer = 0.0f;
				Transform t = Instantiate(obj, transform.position, transform.rotation) as Transform;
				GameObject enemy = GameGod.GetLibraryScript(names, t);
				spawnNumber -= 1;
			}
			else if (spawnNumber <= 0)
			{
				Destroy(this.gameObject);
			}
		}
	}
}
