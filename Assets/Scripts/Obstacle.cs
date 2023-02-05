using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GlobalVariables;

public class Obstacle : MonoBehaviour
{
    public float health = 100;
    public float armor = 1;

    public Obstacles type;

    public void DealDamage(float damage)
    {
        health -= damage / armor;
        if (health <= 0)
        {
            Destroy();
        }
    }

    public void Destroy()
    {
        if (type == Obstacles.Pipe)
        {
            GetComponent<Pipe>().DestroyPipe();
        } else if (type == Obstacles.Trash)
        {
            GetComponent<Trash>().DestroyTrash();
        } else
        {
            Destroy(gameObject);
        }
    }
}
