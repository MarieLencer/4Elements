/* Reference Note:
 * The following tutorials / documentation was used in this script:
 * Nav Mesh Build: https://learn.unity.com/tutorial/runtime-navmesh-generation
 */
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
