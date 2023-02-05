using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootEnd : MonoBehaviour
{
    public Vector2 nextPosition;
    public float moveSpeed = 1;

    void Awake()
    {
        nextPosition = transform.position;
    }

    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, nextPosition, moveSpeed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("coll");
    }
}
