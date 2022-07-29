using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class ThumbnailCreator : MonoBehaviour
{
    [SerializeField] List<GameObject> prefabs = new List<GameObject>();
    //[SerializeField] int size = 1080;

    public void CreateThumbnails()
    {
        foreach(GameObject prefab in prefabs)
        {
            string thumbnailName = prefab.name;
            Texture2D newThumbnail = AssetPreview.GetAssetPreview(prefab);
            //Texture2D newThumbnail = AssetPreview.GetMiniThumbnail(prefab);
            // Encode texture into PNG
            byte[] bytes = newThumbnail.EncodeToPNG();
            Object.DestroyImmediate(newThumbnail);

            // Save the files in the project folder.
            if (bytes != null)
                File.WriteAllBytes(Application.dataPath + $"/../{thumbnailName}.png", bytes);
            else
                Debug.Log($"Was not able to create Thumbnail for {thumbnailName}");
            
            //File.WriteAllBytes(@"E:\Final Project\" + thumbnailName + ".png", bytes);
            //File.WriteAllBytes(@temp)
        }
        Debug.Log("Thumbnails created");
    }
}
