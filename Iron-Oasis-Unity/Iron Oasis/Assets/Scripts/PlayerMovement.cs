using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 400f;
    public float turnSpeed = 20f;

    private Transform hull;
    private Transform turret;
    private int ground;
    private Vector3 input;
    private Rigidbody rb;
    private Quaternion targetDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ground = LayerMask.GetMask("floor");
    }

    private void Start()
    {
        hull = this.transform.Find("Hull");
        turret = this.transform.Find("Turret");
    }

    void OnEnable()
    {
        rb.isKinematic = false;
    }
	
	void Update () {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
        AimTurret();
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
    }

    private void AimTurret()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, 500, ground))
        {
            Vector3 playerToMouse = floorHit.point - turret.transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            turret.transform.rotation = newRotation;
        }
    }
}
