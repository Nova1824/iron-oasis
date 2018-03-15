using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class EnemyAI : MonoBehaviour {

    public float MovementSpeed;
    GameObject Player;
    BoxCollider PlayerCollider;
    public GameObject explosion;
    GameObject spawner;
    Material[] mats;
    public Material red;
    public float damage;
    float idleHeight;
    float idleTop = 4f;
    float idleBottom = 3f;

    // Use this for initialization
    void Start () {
        idleHeight = idleTop;
        Player = GameObject.FindGameObjectWithTag ( "Player" );
        PlayerCollider = Player.GetComponent<BoxCollider>();
        spawner = GameObject.Find("Spawner");
        mats = GetComponent<Renderer>().materials;
	}
	
	// Update is called once per frame
	void Update () {
        if(Player != null)
        {
            Vector3 EnemyToPlayer = Player.transform.position - transform.position;
            EnemyToPlayer.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(EnemyToPlayer);
            transform.rotation = newRotation;
            if (EnemyToPlayer.magnitude > Mathf.Sqrt(Mathf.Pow(PlayerCollider.size.x, 2) + Mathf.Pow(PlayerCollider.size.z, 2)))
            {
                transform.position += transform.forward * MovementSpeed * Time.deltaTime;
            }
            else
            {
                mats[1] = red;
                GetComponent<Renderer>().materials = mats;
                StartCoroutine(attack());
            }
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            transform.Rotate(new Vector3(0f, 5f, 0f));
            if (transform.position.y < idleHeight)
                transform.position += new Vector3(0, .05f, 0);
            else
                transform.position += new Vector3(0, -.05f, 0);
            if (transform.position.y >= idleTop && idleHeight == idleTop)
                idleHeight = idleBottom;
            if (transform.position.y <= idleBottom && idleHeight == idleBottom)
                idleHeight = idleTop;
        }
        
            
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Projectile")
        {
            explode();
            Destroy(col.gameObject);
        }

    }

    IEnumerator attack()
    {
        yield return new WaitForSeconds(0.5f);
        explode();
    }


    void explode()
    {
        CameraShaker.Instance.ShakeOnce(3f, 4f, .05f, .6f);
        spawner.GetComponent<EnemyManagerScript>().count--;
        Instantiate(explosion, transform.position, transform.rotation);
        if (Player != null && Mathf.Abs(Vector3.Magnitude(Player.transform.position - transform.position)) < 5)
            Player.GetComponent<PlayerHealth>().AddjustCurrentHealth(-damage);
        Destroy(this.gameObject);
    }


}
