using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseMovement : MonoBehaviour {

    public float speed = 1f;
    public float turnSpeed = 20f;

    private Vector3 input;
    private Rigidbody rb;
    private Quaternion targetDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        rb.isKinematic = false;
    }
	
	// Update is called once per frame
	void Update () {

        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        //Debug.Log(input);

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
            targetDirection = Quaternion.LookRotation(input);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetDirection, turnSpeed * Time.deltaTime);

            //Other turning method
            /*
            if (input != Vector3.zero)
            {
                targetDirection = Quaternion.LookRotation(input);
                transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetDirection.eulerAngles.y, turnSpeed * Time.deltaTime);
            }
            */

        }
}
