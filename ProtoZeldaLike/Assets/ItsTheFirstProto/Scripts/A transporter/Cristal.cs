using UnityEngine;
using UnityEditor;
using System;

[CreateAssetMenu(fileName = "new Cristal", menuName = "Create Cristal")]
public class Cristal : ScriptableObject
{
    new public string name = "New Cristal";
    public Sprite icon = null;
    public int typeCristal;
    

}

[CustomEditor(typeof(Cristal))]
public class CristalEditor : Editor
{
    public override void OnInspectorGUI()
    {
        
    }
}
