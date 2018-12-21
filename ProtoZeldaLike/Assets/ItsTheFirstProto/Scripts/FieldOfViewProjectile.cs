using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewProjectile : MonoBehaviour {

    public float distanceVue = 5;
    public float distanceOuie = 7;
    public float viewAngle = 360;
    Collider2D[] playerInArea;
    public List<DeplacementDesGardesV2> visiblePlayer, visibleCreature;

    public LayerMask obstacleMask, playerMask, creatureMask;

    private void FixedUpdate()
    {
        FindVisibleEnemis();
    }

    void FindVisibleEnemis()
    {
        playerInArea = Physics2D.OverlapCircleAll(transform.position, distanceVue);
        visiblePlayer.Clear();

        for (int i = 0; i < playerInArea.Length; i++)
        {
            GameObject target = playerInArea[i].gameObject;
            Vector2 dirTarget = new Vector2(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y);
            float distancePlayer = Vector2.Distance(transform.position, target.transform.position);

            if (Physics2D.Raycast(transform.position, dirTarget, distancePlayer, playerMask) && !Physics2D.Raycast(transform.position, dirTarget, distancePlayer, obstacleMask))
            {
                if (target.GetComponent<DeplacementDesGardesV2>() != null)
                {
                    visiblePlayer.Add(target.GetComponent<DeplacementDesGardesV2>());
                }
            }
        }
    }

    public Vector2 dirFromAngle(float angle)
    {
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
    }
}
