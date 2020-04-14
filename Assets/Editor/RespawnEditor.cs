using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RespawnController))]
public class RespawnEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var respawnController = target as RespawnController;

        if (respawnController.positionMode == RespawnController.PositionMode.specific)
        {
            respawnController.specificPosition = EditorGUILayout.Vector3Field("Specific Position", Vector3.zero);
        }
    }
}
