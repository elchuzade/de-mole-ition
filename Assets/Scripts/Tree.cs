using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;

public class Tree : MonoBehaviour
{
    public Spline treeSpline;
    public float angle = 10;
    public float distance = 0.2f;

    float topLimit = 4;
    float bottomLimit = -2;

    Vector2 rootTipPosition;

    public RootEnd rootEnd; // Move the root end to collide with obstacles

    void Start()
    {
        rootTipPosition = treeSpline.nodes[treeSpline.nodes.Count - 1].Position;
        rootEnd.nextPosition = rootTipPosition;

        InvokeRepeating("CreateNewBranch", 0.1f, 0.1f);
    }

    void Update()
    {
        int maxNodes = 20;
        float step = 0.04f;

        for (int i = 0; i < maxNodes; i++)
        {
            if (i - 1 < treeSpline.nodes.Count)
            {
                treeSpline.nodes[treeSpline.nodes.Count - i - 1].Scale = Vector3.one * step * i;
            }
        }
    }

    Vector2 GetNewCoordinate(Vector2 _rootEnd)
    {
        Vector2 newPosition = new Vector2(0,0);
        float randomAngle = UnityEngine.Random.Range(-angle, angle);

        newPosition.x = distance * Mathf.Cos((float)(randomAngle * Math.PI / 180.0f));
        newPosition.y = Mathf.Sin((float)(randomAngle * Math.PI / 180.0f));

        float newRootPositionY = newPosition.y;

        if (_rootEnd.y + newPosition.y > topLimit || _rootEnd.y + newPosition.y < bottomLimit)
        {
            Debug.Log("fixing : " + _rootEnd.y + newPosition.y + " | " + _rootEnd.y + newPosition.y);
            newRootPositionY -= newPosition.y;
        }

        return new Vector2(_rootEnd.x + newPosition.x, _rootEnd.y + newRootPositionY);
        //return _rootEnd + newPosition;
    }

    public void CreateNewBranch()
    {
        Vector2 nextNodePosition = GetNewCoordinate(rootTipPosition);

        rootTipPosition = nextNodePosition;
        rootEnd.nextPosition = nextNodePosition + new Vector2(-6, 0); // Make tree end move towards the next node position

        SplineNode nextNode = new SplineNode(new Vector3(nextNodePosition.x, nextNodePosition.y, 0), Vector3.zero);
        treeSpline.AddNode(nextNode);
        treeSpline.nodes[treeSpline.nodes.Count - 1].Scale = Vector3.zero;

        // Keep total number of root segments max at 40
        if (treeSpline.nodes.Count > 40)
        {
            treeSpline.RemoveNode(treeSpline.nodes[0]);
        }

        treeSpline.GetComponent<SplineSmoother>().SmoothAll();
    }
}
