using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewGarde : MonoBehaviour {

    public float distanceVue = 5;
    public float distanceOuie = 7;
    public float viewAngle = 360;
    Collider2D[] playerInArea, creatureInArea;
    public List<Transform> visiblePlayer, visibleCreature;

    public LayerMask obstacleMask, playerMask, creatureMask;

    private void FixedUpdate()
    {
        FindVisiblePlayer();
        FindVisibleCreature();
    }

    void FindVisiblePlayer()
    {
        playerInArea = Physics2D.OverlapCircleAll(transform.position, distanceVue);
        visiblePlayer.Clear();

        for (int i = 0; i<playerInArea.Length; i++)
        {
            Transform target = playerInArea[i].transform;
            Vector2 dirTarget = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
            float distancePlayer = Vector2.Distance(transform.position, target.position);

            if(Physics2D.Raycast(transform.position, dirTarget, distancePlayer, playerMask) && !Physics2D.Raycast(transform.position, dirTarget, distancePlayer, obstacleMask))
            {
                visiblePlayer.Add(target);
            }
        }
    }

    void FindVisibleCreature()
    {
        creatureInArea = Physics2D.OverlapCircleAll(transform.position, distanceVue);
        visibleCreature.Clear();

        for (int i = 0; i < creatureInArea.Length; i++)
        {
            Transform target = creatureInArea[i].transform;
            Vector2 dirTarget = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
            float distancePlayer = Vector2.Distance(transform.position, target.position);

            if (Physics2D.Raycast(transform.position, dirTarget, distancePlayer, creatureMask) && !Physics2D.Raycast(transform.position, dirTarget, distancePlayer, obstacleMask))
            {
                visibleCreature.Add(target);
            }
        }
    }

    public Vector2 dirFromAngle(float angle)
    {
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
    }

}
