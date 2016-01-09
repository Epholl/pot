using UnityEngine;
using System.Collections;

/// <summary>
/// An input controller class that allows player movement using the keyboard keys.
/// </summary>
public class InputController : MonoBehaviour {

    private float movementX = 0f;
    private float movementY = 0f;
	
	void FixedUpdate () {
	    if (Input.GetKey("up"))
        {
            movementY = Mathf.Min(1, movementY + 0.1f);
        }
        else if (Input.GetKey("down"))
        {
            movementY = Mathf.Max(-1, movementY - 0.1f);
        }
        else if (Mathf.Abs(movementY) < 0.1f)
        {
            movementY = 0f;
        }
        else
        {
            movementY = movementY / 2;
        }

        if (Input.GetKey("right"))
        {
            movementX = Mathf.Min(1, movementX + 0.1f);
        }
        else if (Input.GetKey("left"))
        {
            movementX = Mathf.Max(-1, movementX - 0.1f);
        }
        else if (Mathf.Abs(movementX) < 0.1f)
        {
            movementX = 0f;
        }
        else
        {
            movementX = movementX / 2;
        }

        rigidbody2D.velocity = new Vector2(movementX*10, movementY*10);
	}
}
