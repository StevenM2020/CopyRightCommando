//Script:       FieldOfViewEditor
//Author:       Steven Motz
//Date:         9/20/2022
//Purpose:      This script shows the enemies field of view and is only active in the editor
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        
        FieldOfView fov = (FieldOfView)target;

        // draw radius
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radius);

        // get angles in front of player
        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angle / 2);

        // draw angles in front of player
        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.radius);
        Handles.DrawLine(fov.transform.position, new Vector3(fov.transform.position.x, fov.playerRef.transform.position.y, fov.transform.position.z) + viewAngle02 * fov.radius);
        Handles.DrawLine(fov.transform.position, new Vector3(fov.transform.position.x, fov.playerRef.transform.position.y, fov.transform.position.z) + viewAngle01 * fov.radius);

        // draw lines to player
        Handles.color = Color.blue;
        Handles.DrawLine(fov.transform.position, fov.playerRef.transform.position);
        if (fov.canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.playerRef.transform.position);
            Handles.DrawLine(fov.transform.position, fov.playerRef.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
