using System.IO;
using UnityEditor;
using UnityEngine;

public class SetModelTextures : EditorWindow
{
    private GameObject model;
    private string modelPath;

    [MenuItem("Window/Set Model Textures")]
    static void Init()
    {
        SetModelTextures window = (SetModelTextures)EditorWindow.GetWindow(typeof(SetModelTextures));
        window.Show();
    }

    private void OnGUI()
    {
        model = (GameObject)EditorGUILayout.ObjectField("Model", model, typeof(GameObject), false);

        if (model != null)
        {
            modelPath = AssetDatabase.GetAssetPath(model);

            if (GUILayout.Button("Set Textures"))
            {
                string materialsPath = Path.GetDirectoryName(modelPath) + "/Materials";
                Debug.Log(materialsPath);
                foreach (var item in AssetDatabase.LoadAllAssetsAtPath(materialsPath))
                {
                    Debug.Log(item);
                }

                //foreach (Material material in materials)
                //{
                //    // Set main texture
                //    string mainTexturePath = materialsPath + "/" + material.mainTexture.name + ".png";
                //    Texture2D mainTexture = AssetDatabase.LoadAssetAtPath(mainTexturePath, typeof(Texture2D)) as Texture2D;

                //    if (mainTexture != null)
                //    {
                //        material.mainTexture = mainTexture;
                //    }

                //    // Set normal map
                //    string normalMapName = material.GetTexture("_BumpMap").name + "_Normal";
                //    string normalMapPath = materialsPath + "/" + normalMapName + ".png";
                //    Texture2D normalMap = AssetDatabase.LoadAssetAtPath(normalMapPath, typeof(Texture2D)) as Texture2D;

                //    if (normalMap != null)
                //    {
                //        material.SetTexture("_BumpMap", normalMap);
                //    }

                //    // Set mask map
                //    string maskMapPath = materialsPath + "/" + material.GetTexture("_MetallicGlossMap").name + ".png";
                //    Texture2D maskMap = AssetDatabase.LoadAssetAtPath(maskMapPath, typeof(Texture2D)) as Texture2D;

                //    if (maskMap != null)
                //    {
                //        material.SetTexture("_MetallicGlossMap", maskMap);
                //    }
                //}

                AssetDatabase.Refresh();
            }
        }
    }
}
