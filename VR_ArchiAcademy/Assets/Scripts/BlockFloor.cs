using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

[RequireComponent(typeof(Block))]
public class BlockFloor : MonoBehaviour
{
    [SerializeField] List<Handle> handles = new List<Handle>();
    [SerializeField] TextMeshPro areaNameText;

    Handle handleNorth;
    Handle handleEast;
    Handle handleSouth;
    Handle handleWest;

    XRBaseInteractable xRBaseInteractable;
    RoomType areaType;
    Block block;
    BlocksTracker areaManager;
    TransformBlock blockTransform;
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
        blockTransform = FindObjectOfType<TransformBlock>();
        areaType = FindObjectOfType<RoomType>();
        areaManager = FindObjectOfType<BlocksTracker>();
        //areaManager.AddAreaToList(this);
        block = GetComponent<Block>();

        SetHandles();
        //SetAreaType();
        
    }

    private void Start()
    {
        ShowName(true);
    }

    private void Update()
    {
        Debug.Log("Update");
        
        if (!enterEditMode && blockTransform.isEditing)
        {
            EditFloor(blockTransform.isEditableSize);
            enterEditMode = true;
        }
        if (blockTransform.isEditableSize)
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
        // Convert to world position
        // Update North Vertices
        vertices[1].z = transform.InverseTransformPoint(handleNorth.transform.position).z;
        vertices[2].z = transform.InverseTransformPoint(handleNorth.transform.position).z;

        //Update East Vertices
        vertices[2].x = transform.InverseTransformPoint(handleEast.transform.position).x;
        vertices[3].x = transform.InverseTransformPoint(handleEast.transform.position).x;

        //Update South Vertices
        vertices[3].z = transform.InverseTransformPoint(handleSouth.transform.position).z;
        vertices[0].z = transform.InverseTransformPoint(handleSouth.transform.position).z;

        //Update West Vertices
        vertices[0].x = transform.InverseTransformPoint(handleWest.transform.position).x;
        vertices[1].x = transform.InverseTransformPoint(handleWest.transform.position).x;
    }

    private void CreateMesh()
    {
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.RecalculateNormals();

        // show mesh
        GetComponentInChildren<MeshFilter>().mesh = mesh;
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
        SetHandles();
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
        areaNameText.text = areaName;
    }

    private void ShowName(bool showName)
    {
        // show name text
        areaNameText.gameObject.SetActive(showName);
    }

    // Called when selected on XR interaction on handle? or blockfloor?
    public void UpdateSize()
    {
        Vector3 newSize = new Vector3();
        newSize.y = 0;
        newSize.x = handleWest.transform.position.x - handleEast.transform.position.x;
        newSize.z = handleNorth.transform.position.z - handleSouth.transform.position.z;
        GetComponent<PreviewBlock>().SetBlockSize(newSize);
        // todo see newCollider
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

}
