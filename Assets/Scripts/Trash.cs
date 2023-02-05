using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public ParticleSystem destroyParticle;

    public void DestroyTrash()
    {
        destroyParticle.Play();

        MakeDestroyEffects();
    }

    public void MakeDestroyEffects()
    {
        GetComponent<PolygonCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;

        Invoke("DestroyGameObject", 0.6f);
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
