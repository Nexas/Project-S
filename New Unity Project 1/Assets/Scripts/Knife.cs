using UnityEngine;
using System.Collections;

public class Knife : MonoBehaviour {

	Renderer rend;
    bool enemyFound;
    GameObject target;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
        enemyFound = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!rend.isVisible)
			Destroy (gameObject);

        target = FindClosestEnemyWithTag("enemy");
		if (target == null)
            target = FindClosestEnemyWithTag("Environment");
        

        if (target)
        {
            Vector3 targetDir = target.transform.position - transform.position;
            Vector3 rotDir = Vector3.RotateTowards(transform.forward, targetDir, 50.0f * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(rotDir);
        }

        Vector3 forwardVec = transform.forward;

		transform.Translate((forwardVec * 100.0f) * Time.deltaTime);
		
	}

    GameObject FindClosestEnemyWithTag(string _tag)
    {
        GameObject[] objects;
        objects = GameObject.FindGameObjectsWithTag(_tag);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject g in objects)
        {
            Vector3 diff = g.transform.position - position;
            float currDistance = diff.sqrMagnitude;
            if (currDistance < distance)
            {
                closest = g;
                distance = currDistance;
            }
        }

        if (closest != null)
            enemyFound = true;

        return closest;
    }
}

