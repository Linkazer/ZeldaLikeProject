using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptVisee : MonoBehaviour {

    private Quaternion savedRotation;

    public GameObject prefabCristal;

    public float maxCooldown = 2f;
    private float cooldown = 0;

    private float chargingAttack = 0;
    public float maxChargeAttack = 3f;

    float heading;

    private void Update()
    {
        if(Input.GetAxis("Y")>0 && chargingAttack<maxChargeAttack && cooldown >= maxCooldown)
        {
            chargingAttack += 5*Time.deltaTime;
        }
        else if(cooldown<maxCooldown)
        {
            cooldown += Time.deltaTime;
        }

        if(Input.GetAxis("Y")<=0 && chargingAttack > 0)
        {
            Vector2 velocity = new Vector2(-Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2")).normalized;
            if (!(Mathf.Abs(velocity.x) < 0.1f && Mathf.Abs(velocity.y) < 0.1f))
            {
                ProjectilDeplacement projScript = Instantiate(prefabCristal, transform.position + transform.forward * 1.0f, transform.rotation).GetComponent<ProjectilDeplacement>();
                float speed = (chargingAttack / maxChargeAttack) * projScript.maxSpeed;
                if (speed < 5)
                {
                    speed = 5;
                }
                float currentPortee = (projScript.portee / projScript.maxSpeed) * speed;
                cooldown = 0;

                projScript.velocity = velocity;
                projScript.speed = speed;
                projScript.currentPortee = currentPortee;
            }
            chargingAttack = 0;
        }
    }
    
    private void OnWillRenderObject()
    {
        heading = Mathf.Atan2(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2"));
        transform.rotation = Quaternion.Euler(0f, 0f, heading * Mathf.Rad2Deg);

        savedRotation = transform.rotation;
        var eulers = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(60, eulers.y, eulers.z);
    }

    private void OnRenderObject()
    {
        transform.rotation = savedRotation;
    }
}
