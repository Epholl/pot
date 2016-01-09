using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// A simple utility class to count the amount of time a player survived.
/// </summary>
public class Timer : MonoBehaviour {

    private float time;

    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
        time = 0.0f;
    }

    void Update()
    {
        time += Time.deltaTime;
    }

    void FixedUpdate()
    {
        text.text = "" + (int) time;
    }
}
