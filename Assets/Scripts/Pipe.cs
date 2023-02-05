using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public bool rustyPipe;

    public ParticleSystem destroyParticle;

    public void DestroyPipe()
    {
        if (rustyPipe)
        {
            transform.parent.GetComponent<PipeChain>().DestroyPipechainEffect();
        }
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
