using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildNavMesh : MonoBehaviour
{
    public NavMeshSurface surface;

    public void startBuild()
    {
        surface.BuildNavMesh();
    }
}
