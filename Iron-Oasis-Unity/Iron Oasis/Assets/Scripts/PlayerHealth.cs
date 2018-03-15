using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

public class PlayerHealth : MonoBehaviour
{
    RectTransform healthbarTransform; //location and size of the healthbar
    Slider healthbarSlider; // Used for setting the value of the healthbar
    Renderer[] rens; //array of all the renderers in the children game objects (renderer handle the material and color of the objects)

    public float maxHealth;
    public float curHealth;
    public GameObject healthbar; //parent game object for the healthbar (contains the slider and rectTransform as children
    public Material red;
    public Material normal;
    public GameObject TankRagdollHull;
    public GameObject TankRagdollTurret;

    float _speed; //speed of HP regen (underscore denotes that this variable is private to this class; 'speed' without underscore is the parameter for the heal function)
    
    // Use this for initialization
    void Start()
    {
        healthbarSlider = healthbar.GetComponent<Slider>();
        healthbarTransform = healthbar.GetComponent<RectTransform>();
        rens = GetComponentsInChildren<Renderer>();
        heal(0); // set healing to zero HP per second
    }

    // Update is called once per frame
    void Update()
    {
        if (maxHealth <= 1000f)
            maxHealth = 1000f;
        AddjustCurrentHealth(_speed * Time.deltaTime); //set healing rate to the _speed variable (multiplied by delta time to adjust for changes in frame rate) deltaTime is the time in sec. since the last frame update
        healthbarSlider.maxValue = maxHealth;
        healthbarSlider.value = curHealth;
        healthbarTransform.sizeDelta = new Vector2((maxHealth / 1000)*(Screen.width / 3), 20); // this code is important. it scales the size of the healthbar based on the screen width and the current MaxHealth value. So if you have 1000 maxHealth then the health bar will be a third of the screen. So dont go past 1000 maxHealth.
        if( curHealth <= 0)
        {
            die();
        }
    }

    IEnumerator changeToNormalColor()
    {
        //changes all the materials of the tank back to normal after .1 seconds
        yield return new WaitForSeconds(0.1f);
        foreach (Renderer ren in rens)
        {
            ren.material = normal;
        }
    }

    public void AddjustCurrentHealth(float adj)
    {

        //if the player is damaged, turn the material of the tank to red
        if (adj < 0)
        {
            foreach (Renderer ren in rens)
            {
                ren.material = red;
            }
            StartCoroutine(changeToNormalColor());
        }
            

        curHealth += adj;

        if (curHealth < 0)
            curHealth = 0;

        if (curHealth > maxHealth)
            curHealth = maxHealth;

        if (maxHealth < 1)
            maxHealth = 1;
        
    }

    public void heal(float speed)
    {
        //set the healing rate to the parameter 'speed'
        _speed = speed;
    }

    void OnTriggerEnter(Collider col)
    {
        //if you hit a health orb then apply a certain amount of health and destroy the orb
        if (col.gameObject.name == "Health Orb")
        {
            curHealth += 50;
            Destroy(col.gameObject);
        }

        if (col.gameObject.name == "Energy Orb")
        {
            //not implimented yet
        }

        if (col.gameObject.name == "Exp Orb")
        {
            //not implimented yet
        }

    }

    void die()
    {
        //make the camera shake like crazy
        CameraShaker.Instance.ShakeOnce(20f, 20f, 1f, 2f);
        //instantiate the ragdolls and move them up a little so it looks like they are falling apart when they fall
        Instantiate(TankRagdollHull, transform.position + new Vector3(0,.4f,0), transform.rotation);
        Instantiate(TankRagdollTurret, transform.position + new Vector3(0, 1f, 0), transform.rotation);
        //destroy the moveable tank (this doesn't delete the ragdoll tank)
        Destroy(this.gameObject);
    }

}
