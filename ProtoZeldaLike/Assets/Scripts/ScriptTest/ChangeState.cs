using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeState : MonoBehaviour {

    public bool currentState = false;

    private void OnMouseDown()
    {
        if (currentState == false)
        {
            currentState = true;
            gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        }
    }

    private void Awake()
    {
        StartCoroutine(spreadThisShit());
    }

    private void Update()
    {
        //Debug.DrawRay(transform.position, new Vector2(2, 1.4f), Color.green);
        //Debug.DrawRay(transform.position, new Vector2(2, -1.4f), Color.green);
        //Debug.DrawRay(transform.position, new Vector2(-2, -1.4f), Color.green);
        //Debug.DrawRay(transform.position, new Vector2(-2, 1.4f), Color.green);

        if (Input.GetKeyDown(KeyCode.E))
        {
           // Debug.Log(new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, transform.position.z));
            if (new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, transform.position.z) == transform.position && currentState == false)
            {
               // Debug.Log("Test ChangeState");
                currentState = true;
                gameObject.GetComponent<SpriteRenderer>().color = Color.black;
            }
        }
    }

    IEnumerator spreadThisShit()
    {
        yield return new WaitForSeconds(Time.fixedDeltaTime);

        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, new Vector2(2, 1.4f), 2);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, new Vector2(2, -1.4f), 2);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, new Vector2(-2, -1.4f), 2);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, new Vector2(-2, 1.4f), 2);

        if (currentState)
        {
            if(GetComponent<PolygonCollider2D>().isTrigger == true)
            {
                GetComponent<PolygonCollider2D>().isTrigger = false;
            }

            gameObject.GetComponent<SpriteRenderer>().color = Color.black;
            if (hitUp.collider != null && !hitUp.collider.gameObject.GetComponent<ChangeState>().currentState)
            {
                hitUp.collider.gameObject.GetComponent<ChangeState>().currentState = true;
            }
            if (hitDown.collider != null && !hitDown.collider.gameObject.GetComponent<ChangeState>().currentState)
            {
                hitDown.collider.gameObject.GetComponent<ChangeState>().currentState = true;
            }
            if (hitRight.collider != null && !hitRight.collider.gameObject.GetComponent<ChangeState>().currentState)
            {
                hitRight.collider.gameObject.GetComponent<ChangeState>().currentState = true;
            }
            if (hitLeft.collider != null && !hitLeft.collider.gameObject.GetComponent<ChangeState>().currentState)
            {
                hitLeft.collider.gameObject.GetComponent<ChangeState>().currentState = true;
            }
        }
        
        StartCoroutine(spreadThisShit());
    }
}
