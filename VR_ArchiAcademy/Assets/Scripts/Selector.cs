using UnityEngine;

public class Selector : MonoBehaviour
{
    [SerializeField] GameObject prefabTest;

    private void Update()
    {
        FollowMousePosition();
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
        Instantiate(chosenBlock.gameObject, transform.position, Quaternion.identity);
    }
}
