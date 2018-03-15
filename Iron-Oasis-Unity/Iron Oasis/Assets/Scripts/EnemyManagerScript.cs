using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagerScript : MonoBehaviour
{


    // USE OBJECT POOLS INSTEAD OF INSTANTIATE
    

    public Transform enemy; 
    public float spawnTime = 3.0f;
    public int count = 0;
    public GameObject Player;

	// Use this for initialization
	void Start () {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Spawn()
    {
        if(Player != null)
        {
            if (count > 20) return;
            // these are the range of coords that seem to exist inside the boundries of the map
            float xCoord = Random.Range(-40, 45);
            float zCoord = Random.Range(-38, 20);

            Instantiate(enemy, new Vector3(xCoord, 1.15f, zCoord), Quaternion.identity);
            count++;
        }

    }

}