using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Renderer))]
public class GestionProfondeur : MonoBehaviour {

    private const int IsometricRangePerYUnit = 100;

    public Transform target;

    public int targetOffset = 0;

    public Transform player;
	// Update is called once per frame
	void Update () {
        if (target == null)
            target = transform;
        Renderer renderer = GetComponent(typeof(Renderer))as Renderer;
        renderer.sortingOrder = -(int)(target.position.y * IsometricRangePerYUnit) + targetOffset;
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "VisionBehind")
        {
            if (player != null && player.position.y > target.position.y)
            {
                GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
            }
            else
            {
                GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "VisionBehind")
        {
            if (player != null)
            {
                GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
            }
        }
    }

}
