using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public float speed = 1f;
    public float turnSpeed = 20f;

    private Vector3 input;
    private Rigidbody rb;
    private Vector3 targetDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        rb.isKinematic = false;
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Debug.Log(input);

    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }
 
    private void Move()
    {
        rb.velocity = Vector3.Normalize(input) * speed * Time.deltaTime;
    }

    private void Turn()
    {

        if (input != Vector3.zero)
            targetDirection = new Vector3(input.x, 0f, input.z);

        Quaternion turn = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetDirection), Time.deltaTime * turnSpeed);

        //Quaternion turn = Quaternion.LookRotation(targetDirection);

        transform.rotation = turn;
    }
}
