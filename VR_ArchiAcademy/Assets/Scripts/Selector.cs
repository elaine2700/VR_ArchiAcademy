using UnityEngine;

public class Selector : MonoBehaviour
{
    bool haveSelection;

    private void Start()
    {
        haveSelection = false;
    }

    private void Update()
    {
        //FollowMousePosition();
    }

    // Method used for testing purposes. It will be replaced by XR Controllers Input
    private void FollowMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            transform.position = raycastHit.point;
        }
    }

    // Gets the Prefab information from BlockButton script.
    public void ChooseBlock(Block chosenBlock)
    {
        if(haveSelection) { return; }
        haveSelection = true;
        Instantiate(chosenBlock.gameObject, transform.position, Quaternion.identity);
        GameObject previewCopy = Instantiate(chosenBlock.gameObject, transform.position, Quaternion.identity); // A copy to preview placement
        previewCopy.GetComponent<Block>().SetAsPreview();
    }

    public void SetSelector(bool selection)
    {
        haveSelection = selection;
    }


}
