using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRSimpleInteractable))]
public class UnitFloor : MonoBehaviour
{
    XRSimpleInteractable simpleInteractable;
    TransformBlock transformBlock;
    Blockfloor_V2 blockFloor;
    Renderer mesh;

    public bool isOverlapFinder = false; 

    private void Awake()
    {
        simpleInteractable = GetComponent<XRSimpleInteractable>();

        transformBlock = transform.parent.GetComponentInParent<TransformBlock>();
        if(transformBlock == null)
        {
            Debug.LogError("Transform Block component wasn't found in parent's parent");
        }

        blockFloor = transform.parent.GetComponentInParent<Blockfloor_V2>();
        if (blockFloor == null)
        {
            Debug.LogError("BlockFloor component wasn't found in parent's parent");
        }
        
    }

    private void Start()
    {
        DisableCollider();
    }

    private void OnEnable()
    {
        mesh = GetComponentInChildren<Renderer>();
        if (mesh == null)
        {
            Debug.LogWarning("Didnt find MeshRenderer");
        }

        simpleInteractable.selectEntered.AddListener(blockFloor.EditFloor);
    }

    private void OnDisable()
    {
        simpleInteractable.selectEntered.RemoveAllListeners();
    }

    public void OverlapFinder()
    {
        isOverlapFinder = true;
        simpleInteractable.enabled = false;
        mesh.enabled = false;
        GetComponentInChildren<Collider>().enabled = false;
        GetComponentInChildren<OverlapFinder>().ChangeLayer();
    }

    private void DisableCollider()
    {
        // Disable Units colliders while the handle is editing the size.
        GetComponentInChildren<BoxCollider>().enabled = false;
    }
}
