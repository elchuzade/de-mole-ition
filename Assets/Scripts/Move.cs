using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public FloatingJoystick floatingJoystick;
    Vector3 mousePosition;
    public float moveSpeed = 5f;
    float previousAngle = 0;
    Rigidbody2D rb;
    Vector2 position = Vector2.zero;
    public float dashSpeed = 20f;
    public GameObject moleBody;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 direction = Vector3.up * floatingJoystick.Vertical + Vector3.right * floatingJoystick.Horizontal;
        rb.velocity = direction * moveSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        if (direction != Vector3.zero)
        {
            moleBody.transform.rotation = Quaternion.AngleAxis(angle - previousAngle, Vector3.forward);
        }
        else
        {
            previousAngle = angle;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            rb.velocity = direction * dashSpeed;
        }
    }
}
