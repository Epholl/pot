using UnityEngine;
using System.Collections;

/// <summary>
/// A simple controller for the enemies in the game. They calculate their movement and increase their speed gradually.
/// </summary>
public class BadStuffController : MonoBehaviour {

    private float speed = 1f;
    private Vector2 velocity;

    private Vector3 lastPosition;

    public GameObject endlevelMenu;

    void Start()
    {
        velocity = new Vector2(GetRandomMovement(), GetRandomMovement());
        rigidbody2D.velocity = velocity;
        lastPosition = transform.position;
    }

    void FixedUpdate()
    {
        speed *= 1.001f;
        rigidbody2D.velocity = velocity * speed;

        Vector3 currentPosition = transform.position;
        if (Mathf.Abs(currentPosition.x - lastPosition.x) < 0.00001f)
        {
            velocity = new Vector2(GetRandomMovement(), velocity.y);
        }
        if (Mathf.Abs(currentPosition.y - lastPosition.y) < 0.00001f)
        {
            velocity = new Vector2(velocity.x, GetRandomMovement());
        }
        lastPosition = currentPosition;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            endlevelMenu.SetActive(true);
        }
    }

    private float GetRandomMovement()
    {
        if (Random.value > 0.5f)
        {
            return 1f;
        }
        else
        {
            return -1f;
        }
    }
}
