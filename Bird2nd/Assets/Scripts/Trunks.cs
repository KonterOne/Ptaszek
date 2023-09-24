using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Trunks : MonoBehaviour
{
    public Transform top;
    public Transform bottom;

    public static float speed = 0f;
    public static float tmpspeed = 0f;
    private float leftEdge;

    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
    }

    private void Update()
    {
        transform.position += Vector3.left * (speed + tmpspeed) * Time.deltaTime;

        if (transform.position.x < leftEdge) {
            Destroy(gameObject);
        }
    }

}