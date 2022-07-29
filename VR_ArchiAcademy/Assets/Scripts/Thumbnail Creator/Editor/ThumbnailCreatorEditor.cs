using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ThumbnailCreator))]
public class ThumbnailCreatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ThumbnailCreator creator = (ThumbnailCreator)target;

        if(GUILayout.Button("Create Thumbnails"))
        {
            creator.CreateThumbnails();
        }
    }
}
