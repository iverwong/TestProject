using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MergeMesh
{
    [MenuItem("GameObject/Custom Edit/MergeMesh", false, 0)]
    static void Merge()
    {
        GameObject selected = Selection.activeGameObject;
        SkinnedMeshRenderer[] skinnedMeshRenderers = selected.GetComponentsInChildren<SkinnedMeshRenderer>();
        CombineInstance[] combineInstances = new CombineInstance[skinnedMeshRenderers.Length];
        for (int i = 0; i< skinnedMeshRenderers.Length; i++)
        {
            combineInstances[i].mesh = skinnedMeshRenderers[i].sharedMesh;
            combineInstances[i].transform = skinnedMeshRenderers[i].transform.localToWorldMatrix;
        }
        Mesh mesh = new Mesh();
        mesh.CombineMeshes(combineInstances);
        mesh.name = "MergeMesh";
        selected.AddComponent<MeshFilter>().sharedMesh = mesh;
    }
}
