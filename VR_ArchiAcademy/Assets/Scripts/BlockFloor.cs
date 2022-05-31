using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class BlockFloor : MonoBehaviour
{
    [SerializeField] GameObject handleNorth;
    [SerializeField] GameObject handleEast;
    [SerializeField] GameObject handleSouth;
    [SerializeField] GameObject handleWest;

    // Make quad
    // define size
    int width = 1; //transformX
    int depth = 1; // transformZ
    int height = 1; // transformY
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    Vector3[] normals;
    Vector2[] uvs;
    Vector3[] otherVertices;

    private void Start()
    {
        GenerateMeshData();
        CreateMesh();
    }

    private void Update()
    {
        ModifyVertices();
        mesh.SetVertices(vertices);
    }

    private void GenerateMeshData()
    {
        mesh = new Mesh();

        
        SetVertices();

        // Define triangles
        triangles = new int[6]
        {
            //upper left triangle
            0,1,2,
            //lower right triangle
            0,2,3
        };

        // Define normals
        /*normals = new Vector3[4]
        {
            -Vector3.down,
            -Vector3.down,
            -Vector3.down,
            -Vector3.down,
        };
        */

        // Define uvs

        uvs = new Vector2[4]
        {
            new Vector2(0,1),
            new Vector2(1,0),
            new Vector2(0,1),
            new Vector2(1,1)
        };
    }

    private void SetVertices()
    {
        // Vertices clockwise direction.
        vertices = new Vector3[]
        {
        new Vector3(0,0,0),
        new Vector3(0,0,depth),
        new Vector3(width,0,depth),
        new Vector3(width, 0,0)
        };
    }

    private void ModifyVertices()
    {
        // Update North Vertices
        vertices[1].z = handleNorth.transform.position.z;
        vertices[2].z = handleNorth.transform.position.z;

        //Update East Vertices
        vertices[2].x = handleEast.transform.position.x;
        vertices[3].x = handleEast.transform.position.x;

        //Update South Vertices
        vertices[3].z = handleSouth.transform.position.z;
        vertices[0].z = handleSouth.transform.position.z;

        //Update West Vertices
        vertices[0].x = handleWest.transform.position.x;
        vertices[1].x = handleWest.transform.position.x;
    }

    private void CreateMesh()
    {
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
        //mesh.uv = uvs;
        mesh.RecalculateNormals();

        // show mesh
        GetComponent<MeshFilter>().mesh = mesh;
    }

}
