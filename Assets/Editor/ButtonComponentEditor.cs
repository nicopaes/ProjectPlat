using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ButtonComponent))]
public class ButtonComponentEditor : Editor
{
    private void OnSceneGUI()
    {
        ButtonComponent button = target as ButtonComponent;

        Vector3 buttonPosition = button.transform.position;
        Vector3 doorPosition = button.attachedReactionObject.transform.position;

        Handles.color = Color.white;
        Handles.DrawDottedLine(buttonPosition, doorPosition,0.5f);
        Handles.CubeHandleCap(1,buttonPosition,Quaternion.identity,0.2f,EventType.Ignore);
    }
}
