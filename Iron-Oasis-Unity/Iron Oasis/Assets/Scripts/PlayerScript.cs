using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public float speed = 1;

    private bool isMoving;
    private Rigidbody rb;
    private float inputValueHorizontal;
    private float inputValueVertical;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        rb.isKinematic = false;
    }

    void OnDisable()
    {
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.1 || Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.1)
        {
            isMoving = true;
            inputValueHorizontal = Input.GetAxisRaw("Horizontal");
            inputValueVertical = Input.GetAxisRaw("Vertical");
        }
        else
        {
            isMoving = false;
        }

        Debug.Log(Input.GetAxisRaw("Horizontal") + "    " + Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        Vector3 move;
        if (isMoving)
        {
            move = transform.forward * speed * Time.deltaTime;
        }
        else
        {
            move = Vector3.zero;
        }

        //transform.position += move;
        //rb.MovePosition(rb.position + move);
        rb.velocity = move;
    }

    private void Turn()
    {
        Vector3 targetAngle = new Vector3(inputValueHorizontal, 0f, inputValueVertical);

        Quaternion turn = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetAngle), Time.deltaTime * 10f);

        transform.rotation = turn;
    }
}
