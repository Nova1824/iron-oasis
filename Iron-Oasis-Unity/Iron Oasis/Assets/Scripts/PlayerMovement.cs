using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public GameObject laser;  //reference to the laser projectile game object

    public float speed;  //movement speed
    public float turnSpeed;  //turn speed
    public float fireSpeed;  // fire rate
    public float projectileSpeed; //speed of projectile

    private float shootTime;  //handles fire rate
    private int ground;  // stores the layermask for the floor

    private Vector3 input;  //stores the player input from the keyboard (WSAD) (used for moving the tank)
    private Rigidbody rb;  // physics component of the tank
    private Quaternion targetDirection;  //direction of the tank based on input
    private Transform hull;  //location and orientation of the Hull game object
    private Transform turret;  //location and orientation of the turret game object

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ground = LayerMask.GetMask("floor");
    }

    void Start()
    {
        hull = this.transform.Find("Hull");
        turret = this.transform.Find("Turret");
    }

    void OnEnable()
    {
        rb.isKinematic = false;  // sets the tank to kinematic (this means that it is unaffected by forces applied to it by the physics engine  ie. you have to move it with methods directly)
    }
	
	void Update () {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));  //get the input from the controls that are set up in the editor
        if (Input.GetMouseButton(0) && Time.time >= shootTime)  //if you click and the gun is ready to shoot then
        {
            var projectile = (GameObject)Instantiate(laser, turret.transform.position + Vector3.Normalize(turret.transform.forward) * 2f, turret.transform.rotation);  //instantiate a laser at the end of the barrel
            projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * projectileSpeed; //set the projectile velocity to a specific value
            Destroy(projectile, 2.0f);  //removes game object (projectile) after 2 seconds. This should probably be increased and we should fix the problem where the laser phases through terrain objects
            shootTime = Time.time + 1f / fireSpeed; //set the new shoot time based on the current time and the rate of fire
        }
    }

    void FixedUpdate()
    {
        Move();
        Turn();
        AimTurret();
    }
 
    private void Move()
    {
        rb.velocity = Vector3.Normalize(input) * speed; //here we are normalizing the vector so you cant move faster when traveling on a diagonal 
    }

    private void Turn()
    {
        if (input != Vector3.zero)
            targetDirection = Quaternion.LookRotation(input); //when player is commanding the tank to move, set the target direction the the same direction as the input

        transform.rotation = Quaternion.Lerp(transform.rotation, targetDirection, turnSpeed * Time.deltaTime);  //lerp does a smooth transition from the current rotation to the target direction. the turnspeed determines how fast it rotates
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
