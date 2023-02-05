using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeChain : MonoBehaviour
{
    public Pipe[] allPipes;

    public void DestroyPipechainEffect()
    {
        for (int i = 0; i < allPipes.Length; i++)
        {
            if (allPipes[i] != null)
            {
                allPipes[i].MakeDestroyEffects();
            }
        }
        Invoke("DestroyPipechain", 0.6f);
    }

    public void DestroyPipechain()
    {
        Destroy(gameObject);
    }
}
