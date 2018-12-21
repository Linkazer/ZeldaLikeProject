using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptVisee : MonoBehaviour {

    private Quaternion savedRotation;

    public GameObject prefabCristal;

    public float maxCooldown = 2f;

    private float chargingAttack = 0;
    public float maxChargeAttack = 3f;

    float heading;

    private void Update()
    {
        if(Input.GetAxis("Y")>0 && chargingAttack<maxChargeAttack)
        {
            chargingAttack += 3*Time.deltaTime;
        }

        if(Input.GetAxis("Y")<=0 && chargingAttack > 0)
        {
            ProjectilDeplacement projScript = Instantiate(prefabCristal, transform.position + transform.forward * 1.0f, transform.rotation).GetComponent<ProjectilDeplacement>();
            projScript.velocity = new Vector2(-Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2")).normalized;
            projScript.speed = (chargingAttack / maxChargeAttack) * projScript.maxSpeed;
            projScript.currentPortee = (projScript.portee / projScript.maxSpeed) * projScript.speed;
            chargingAttack = 0;
        }
    }
    
    private void OnWillRenderObject()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal2"))>=0.2f && Mathf.Abs(Input.GetAxis("Vertical2")) >= 0.2f)
        {
            
        }
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
