using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class ragdoll : MonoBehaviour {


    public GameObject explosion;
    Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0,500,0));
        rb.AddTorque(new Vector3(10, 10, 10));
        Instantiate(explosion, transform.position, transform.rotation);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
