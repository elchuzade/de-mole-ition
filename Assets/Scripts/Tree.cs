using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;

public class Tree : MonoBehaviour
{
    public GameObject leafPrefab;

    public Spline treeSpline;
    public float angle = 10;
    public float distance = 0.2f;

    public Material deadTreeMaterial;
    public Transform treeSegments;

    public CameraMove cameraMove;

    float topLimit = 4;
    float bottomLimit = -2;

    bool growRoots = false;
    bool shrinkTree = false;

    Vector2 rootTipPosition;

    public RootEnd rootEnd; // Move the root end to collide with obstacles

    void Start()
    {
        rootTipPosition = treeSpline.nodes[treeSpline.nodes.Count - 1].Position;
        rootEnd.nextPosition = rootTipPosition;
    }

    void Update()
    {
        if (growRoots)
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
        else if (shrinkTree)
        {
            for (int i = 0; i < treeSpline.nodes.Count; i++)
            {
                if (treeSpline.nodes[i].Scale.y >= 0)
                {
                    treeSpline.nodes[i].Scale -= new Vector2(0.01f, 0.01f);
                }
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
            newRootPositionY -= newPosition.y;
        }

        return new Vector2(_rootEnd.x + newPosition.x, _rootEnd.y + newRootPositionY);
    }

    public void CreateNewBranch()
    {
        if (!growRoots) return;

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

        int randomNumber = UnityEngine.Random.Range(0, 100);
        // Randomly sometimes create a leaf
        if (randomNumber < 5)
        {
            CreateLeaf(1);
        } else if (randomNumber > 95)
        {
            CreateLeaf(-1);
        }
    }

    public void CreateLeaf(int leafDirection)
    {
        GameObject leaf = Instantiate(leafPrefab, rootTipPosition - new Vector2(6,0), Quaternion.identity);
        leaf.transform.SetParent(transform);
        leaf.GetComponent<Leaf>().Initialize(leafDirection);
    }

    public void StartGrowingRoots()
    {
        InvokeRepeating("CreateNewBranch", 0.1f, 0.1f);
        Invoke("StartGrowingRoots", 1);
        growRoots = true;
    }

    public void DestroyTree()
    {
        cameraMove.moveCamera = false;

        // Destroy all leafs
        Leaf[] leaves = FindObjectsOfType<Leaf>();
        for (int i = 0; i < leaves.Length; i++)
        {
            leaves[i].DestroyLeaf();
        }

        ChangeTreeMaterial();
        growRoots = false;
        shrinkTree = true;
    }

    public void ChangeTreeMaterial()
    {
        for (int i = 0; i < treeSegments.childCount; i++)
        {
            treeSegments.GetChild(i).GetComponent<MeshRenderer>().material = deadTreeMaterial;
        }
    }
}
