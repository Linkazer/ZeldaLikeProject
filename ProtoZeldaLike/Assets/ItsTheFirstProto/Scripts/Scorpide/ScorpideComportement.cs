using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpideComportement : MonoBehaviour {

    public bool isAsleep = true;

    public Sprite asleepSprite;
    public Sprite wakeUpSprite;

    public float attackDmg;

    public float speed;

    Vector2 velocity;
    Rigidbody2D rigiBoy;

    public Transform target;

    public GameObject weapon;

    public float walkDistance;

    public List<Transform> canReachTarget;

    private void Start()
    {
        rigiBoy = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Vector2.Distance(target.position,transform.position) <= walkDistance || isAsleep)
        {
            velocity = Vector2.zero;
        }
        else if(target != null)
        {
            velocity = (target.position - transform.position).normalized * speed;
        }

        if(target == null && !isAsleep)
        {
            target = canReachTarget[Random.Range(0, canReachTarget.Count - 1)];
        }
    }

    private void FixedUpdate()
    {
        rigiBoy.MovePosition(rigiBoy.position + velocity * Time.fixedDeltaTime);
    }

    public void WakeUp(Transform whoWakeUp)
    {
        GetComponent<SpriteRenderer>().sprite = wakeUpSprite;
        target = whoWakeUp;
    }

    public void Attack()
    {
        weapon.SetActive(true);
        weapon.transform.up = target.position - transform.position;
    }
}
