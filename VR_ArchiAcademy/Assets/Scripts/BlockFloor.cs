using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Block))]
public class BlockFloor : MonoBehaviour
{
    [SerializeField] List<Handle> handles = new List<Handle>();

    Handle handleNorth;
    Handle handleEast;
    Handle handleSouth;
    Handle handleWest;

    XRBaseInteractable xRBaseInteractable;
    AreaType areaType;
    Block block;
    BlocksTracker areaManager;
    bool enterEditMode = false;

    int width = 1; //transformX
    int depth = 1; // transformZ
    int height = 1; // transformY
    string areaName;

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    Vector3[] normals;
    Vector2[] uvs;

    private void Awake()
    {
        GenerateMeshData();
        CreateMesh();
    }

    private void Start()
    {
        areaType = FindObjectOfType<AreaType>();
        areaManager = FindObjectOfType<BlocksTracker>();
        areaManager.AddAreaToList(this);
        block = GetComponent<Block>();
        
        SetHandles();
        SetAreaType();
        ShowName(true);
    }

    private void Update()
    {
        if (!enterEditMode)
        {
            EditFloor(block.edit);
            enterEditMode = true;
        }
        if (block.edit)
        {
            ModifyVertices();
            mesh.SetVertices(vertices);
        }
        else
        {
            enterEditMode = false;
        }
    }

    private void SetHandles()
    {
        foreach (Handle handle in handles)
        {
            switch (handle.handleDir)
            {
                case Handle.handleDirection.north:
                    handleNorth = handle;
                    break;
                case Handle.handleDirection.east:
                    handleEast = handle;
                    break;
                case Handle.handleDirection.south:
                    handleSouth = handle;
                    break;
                case Handle.handleDirection.west:
                    handleWest = handle;
                    break;
            }
        }
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
        vertices[1].z = handleNorth.gameObject.transform.position.z;
        vertices[2].z = handleNorth.gameObject.transform.position.z;

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
        mesh.RecalculateNormals();

        // show mesh
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;

    }

    public void EditFloor(bool isEditing)
    {
        // enable or disable Blocks
        // checking in update if Block is in edit mode
        foreach(Handle handle in handles)
        {
            handle.gameObject.SetActive(isEditing);
        }
    }

    public Vector3 GetFloorSize()
    {
        Vector3 size = new Vector3();
        // todo to set physics.Overlap box
        return size;
    }

    public void SetAreaType()
    {
        areaName = areaType.area;
    }

    private void ShowName(bool showName)
    {
        // show name text
    }
    

}
