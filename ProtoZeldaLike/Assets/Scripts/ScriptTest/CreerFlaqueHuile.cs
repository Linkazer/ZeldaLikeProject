using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreerFlaqueHuile : MonoBehaviour {

    public GameObject prefab;

    public List<Vector2> alreadyUsedSpace;

    private void Start()
    {
        alreadyUsedSpace = new List<Vector2>();
    }

    // Update is called once per frame
    void Update () {
		if(Input.GetMouseButtonDown(1))
        {
            Vector2 positionVisee = new Vector2(Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).x), Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).y));
            for (int i = -5; i < 6; i++)
            {
                for (int j = -5; j < 6; j++)
                {
                    if (!alreadyUsedSpace.Contains(new Vector2(i + positionVisee.x, j + positionVisee.y)))
                    {
                        Instantiate(prefab, (transform.position + transform.right * i + transform.up * j) + new Vector3(positionVisee.x, positionVisee.y, 0), transform.rotation);
                        alreadyUsedSpace.Add(new Vector2(i + positionVisee.x, j + positionVisee.y));
                    }
                }
            }
        }
	}
}
