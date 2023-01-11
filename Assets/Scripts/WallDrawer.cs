using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Wall))]
public class WallDrawer : Editor
{
    private void OnSceneGUI()
    {
        Wall wall = target as Wall;
        UnityEditor.Handles.Label(wall.transform.position, wall.fishRequired.ToString());
    }
} 