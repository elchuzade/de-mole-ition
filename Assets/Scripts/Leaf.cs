using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour
{
    public int leafDirection = 0;

    void Start()
    {
        Invoke("DestroyLeaf", 2);
    }

    void Update()
    {
        if (transform.localScale.x < 1 && leafDirection != 0) 
        {
            transform.localScale += new Vector3(0.001f, 0.001f * leafDirection, 0.001f);
            transform.position += new Vector3(0, 0.00045f * leafDirection, 0);
        }
    }

    public void Initialize(int direction)
    {
        leafDirection = direction;
        transform.localScale *= leafDirection;
    }

    public void DestroyLeaf()
    {
        Destroy(gameObject);
    }
}
