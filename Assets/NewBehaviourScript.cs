using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    Mesh mesh;
    void Start() {
        MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
        renderer.material = new Material(Shader.Find("Diffuse"));
        renderer.material.color = Color.red;

        MeshFilter filter = gameObject.AddComponent<MeshFilter>();
        mesh = new Mesh(); 
        
        mesh.vertices = new Vector3[8];
        mesh.triangles = new int[]{
             0, 1, 4,
             5, 4, 1,
             1, 3, 5,
             3, 7, 5,
             3, 2, 6,
             6, 7, 3,
             2, 0, 4,
             6, 2, 4,
         };
        mesh.uv = new Vector2[mesh.vertices.Length];
        mesh.RecalculateNormals();
        filter.mesh = mesh;
    }

    public float z = 0;
    public float xSize = 1;
    public float ySize = 1;
    float width = 1f;

    void Update() {
        mesh.vertices = new Vector3[]{
             new Vector3(-width, -width, z),
             new Vector3(-width, width+ySize, z),
             new Vector3(width+xSize, -width, z),
             new Vector3(width+xSize, width+ySize, z),

             new Vector3(width, width, z),
             new Vector3(width, -width+ySize, z),
             new Vector3(-width+xSize, width, z),
             new Vector3(-width+xSize, -width+ySize, z),
         };
        mesh.RecalculateNormals();
    }
}
