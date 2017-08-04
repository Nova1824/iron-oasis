using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagerScript : MonoBehaviour
{

    public Transform enemy; // for now i'm testing spawning with cubes
    public float spawnTime = 3.0f;
    private int count = 0;
	// Use this for initialization
	void Start () {;
        InvokeRepeating("Spawn", spawnTime, spawnTime);
	}

    private void Spawn()
    {
        if (count > 20) return;
        // these are the range of coords that seem to exist inside the boundries of the map
        float xCoord = Random.Range(-40, 45);
        float zCoord = Random.Range(-38, 20);

        Instantiate(enemy, new Vector3(xCoord, 2.15f, zCoord), Quaternion.identity);
        count++;
    }

}