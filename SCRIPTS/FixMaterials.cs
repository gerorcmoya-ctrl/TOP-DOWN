#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class FixMaterials : MonoBehaviour
{
    [MenuItem("Tools/Fix All Pink Materials")]
    static void FixAllMaterials()
    {
        string[] guids = AssetDatabase.FindAssets("t:Material");
        int fixed_ = 0;

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);

            if (mat.shader.name.Contains("Standard") ||
                mat.shader.name.Contains("Legacy") ||
                mat.shader.name.Contains("Hidden/InternalErrorShader"))
            {
                mat.shader = Shader.Find("Universal Render Pipeline/Lit");
                EditorUtility.SetDirty(mat);
                fixed_++;
            }
        }

        AssetDatabase.SaveAssets();
        Debug.Log($"Se arreglaron {fixed_} materiales.");
    }
}
#endif