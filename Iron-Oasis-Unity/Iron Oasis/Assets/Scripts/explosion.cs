using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour {

    ParticleSystem particles;

    // Use this for initialization
    void Start () {
        //find the particle system component of the game object that this scrip is attached to
        particles = GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        //destroy object after it has finished its animation
        if (!particles.isPlaying)
            Destroy(this.gameObject);
	}
}
