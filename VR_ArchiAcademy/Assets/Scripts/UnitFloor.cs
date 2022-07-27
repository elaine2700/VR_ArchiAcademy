using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRSimpleInteractable))]
public class UnitFloor : MonoBehaviour
{
    XRSimpleInteractable simpleInteractable;
    TransformBlock transformBlock;
    Blockfloor_V2 blockFloor;
    Renderer mesh;

    [HideInInspector]
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

    private void OnEnable()
    {
        mesh = GetComponentInChildren<Renderer>();
        if (mesh == null)
        {
            Debug.LogWarning("Didnt find MeshRenderer");
        }

        simpleInteractable.selectEntered.AddListener(EditFloor);

        
    }

    private void OnDisable()
    {
        simpleInteractable.selectEntered.RemoveAllListeners();
    }

    private void EditFloor(SelectEnterEventArgs args)
    {
        transformBlock.MakeBlockEditable(!transformBlock.isEditing);
        blockFloor.ShowHandles(transformBlock.isEditing);
    }

    public void OverlapFinder()
    {
        isOverlapFinder = true;
        simpleInteractable.enabled = false;
        mesh.enabled = false;
        GetComponentInChildren<Collider>().enabled = false;
    }
}
