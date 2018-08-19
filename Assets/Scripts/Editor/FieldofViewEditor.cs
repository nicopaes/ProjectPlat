﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FieldofViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fov = target as FieldOfView;
        Handles.color = Color.white;

        Handles.DrawWireArc(fov.transform.position, Vector3.forward, Vector3.up,360f,fov.viewRadius);
        Vector3 viewAngleA = fov.DirFromAngle(-fov.viewAngle/2,false);
        Vector3 viewAngleB = fov.DirFromAngle(fov.viewAngle / 2, false);

        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.viewRadius);

        foreach (Transform visibleTarget in fov.visibleTargets)
        {
            Handles.color = Color.red;
            Handles.DrawLine(fov.transform.position, visibleTarget.position);
        }
    }

}
