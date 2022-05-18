using UnityEngine;

public class Block_Other : MonoBehaviour
{
    [SerializeField] Vector3 grid = new Vector3(0.05f,0.025f,0.05f);

    public Vector3 SnapToGrid(Vector3 NewPos)
    {
        return new Vector3(Mathf.Round(NewPos.x / grid.x) * grid.x,
            Mathf.Round(NewPos.y / grid.y) * grid.y,
            Mathf.Round(NewPos.z / grid.z) * grid.z);
    }

    private void Update()
    {
        
    }
}
