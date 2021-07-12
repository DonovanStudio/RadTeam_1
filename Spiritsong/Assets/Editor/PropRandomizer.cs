using UnityEditor;
using UnityEngine;
public class PropRandomizer : EditorWindow
{
    [SerializeField] private GameObject parent;
    [SerializeField] private Vector3 minScaleDelta;
    [SerializeField] private Vector3 maxScaleDelta;
    [SerializeField] private Vector3 minRotDelta;
    [SerializeField] private Vector3 maxRotDelta;
    [MenuItem("Tools/Prop Rot-Scale Randomizer")]
    static void CreateReplaceWithPrefab()
    {
        GetWindow<PropRandomizer>();
    }
    private void OnGUI()
    {
        GUILayout.Label("Objects Parent", EditorStyles.boldLabel);
        parent = (GameObject)EditorGUILayout.ObjectField("Parent Object", parent, typeof(GameObject), true);
        EditorGUILayout.Space();
        GUILayout.Label("Scale Min/Max", EditorStyles.boldLabel);
        minScaleDelta = EditorGUILayout.Vector3Field("Min Scale Delta", minScaleDelta);
        maxScaleDelta = EditorGUILayout.Vector3Field("Max Scale Delta", maxScaleDelta);
        EditorGUILayout.Space();
        GUILayout.Label("Rotation Min/Max", EditorStyles.boldLabel);
        minRotDelta = EditorGUILayout.Vector3Field("Min Rotation Delta", minRotDelta);
        maxRotDelta = EditorGUILayout.Vector3Field("Max Rotation Delta", maxRotDelta);
        if (GUILayout.Button("Replace"))
        {
            Randomize();
        }
    }
    private void Randomize()
    {
        foreach (Transform child in parent.transform)
        {
            child.localScale += RandomVec3(minScaleDelta, maxScaleDelta);
            child.rotation = Quaternion.Euler(child.rotation.eulerAngles + RandomVec3(minRotDelta, maxRotDelta));
        }
    }
    private Vector3 RandomVec3(Vector3 min, Vector3 max)
    {
        float x, y, z;
        x = Random.Range(min.x * 1000, max.x * 1000) / 1000;
        y = Random.Range(min.y * 1000, max.y * 1000) / 1000;
        z = Random.Range(min.z * 1000, max.z * 1000) / 1000;
        return new Vector3(x, y, z);
    }
}