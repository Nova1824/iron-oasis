using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurretMovement : MonoBehaviour {

    private int ground;

    void Awake()
    {
        ground = LayerMask.GetMask("floor");
    }

    // Update is called once per frame
    void Update()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if(Physics.Raycast (camRay, out floorHit, 500, ground))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            transform.rotation = newRotation;
        }

    }

}
