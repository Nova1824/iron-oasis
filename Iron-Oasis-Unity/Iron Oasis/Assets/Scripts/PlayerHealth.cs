using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

public class PlayerHealth : MonoBehaviour
{

    public float maxHealth;
    public float curHealth;
    public GameObject healthbar;
    RectTransform healthbarTransform;
    Slider healthbarSlider;
    float speed_;
    Renderer[] rens;
    public Material red;
    public Material normal;
    public GameObject TankRagdollHull;
    public GameObject TankRagdollTurret;

    // Use this for initialization
    void Start()
    {
        healthbarSlider = healthbar.GetComponent<Slider>();
        healthbarTransform = healthbar.GetComponent<RectTransform>();
        rens = GetComponentsInChildren<Renderer>();
        heal(0);
    }

    // Update is called once per frame
    void Update()
    {
        AddjustCurrentHealth(speed_ * Time.deltaTime);
        healthbarSlider.maxValue = maxHealth;
        healthbarSlider.value = curHealth;
        healthbarTransform.sizeDelta = new Vector2((maxHealth / 1000)*(Screen.width / 3), 20);
        if( curHealth <= 0)
        {
            die();
        }
    }

    IEnumerator changeToNormalColor()
    {
        yield return new WaitForSeconds(0.1f);
        foreach (Renderer ren in rens)
        {
            ren.material = normal;
        }
    }

    public void AddjustCurrentHealth(float adj)
    {

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
        speed_ = speed;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Health Orb")
        {
            curHealth += 50;
            Destroy(col.gameObject);
        }

    }

    void die()
    {
        CameraShaker.Instance.ShakeOnce(20f, 20f, 1f, 2f);
        Instantiate(TankRagdollHull, transform.position + new Vector3(0,.4f,0), transform.rotation);
        Instantiate(TankRagdollTurret, transform.position + new Vector3(0, 1f, 0), transform.rotation);
        Destroy(this.gameObject);
    }

}
