using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AIVision))]
public class AIVisionEditor : Editor
{
    private void OnSceneGUI()
    {
        AIVision aiv = (AIVision)target;

        Handles.color = Color.white;
        Handles.DrawWireArc(aiv.transform.position, Vector3.up, Vector3.forward, 360, aiv.GetViewRange());

        Vector3 viewAngleA = aiv.DirFromAngle(-aiv.GetViewAngle() / 2, false);
        Vector3 viewAngleB = aiv.DirFromAngle(aiv.GetViewAngle() / 2, false);

        /*
        Handles.color = Color.red;
        Handles.DrawLine(aiv.transform.position, aiv.transform.position + viewAngleA * aiv.GetViewRange());
        Handles.DrawLine(aiv.transform.position, aiv.transform.position + viewAngleB * aiv.GetViewRange());
        */

        Handles.color = new Color(1f, 0f, 0f, 0.1f);
        Handles.DrawSolidArc(aiv.transform.position, Vector3.up, viewAngleA, aiv.GetViewAngle(), aiv.GetViewRange());
        Handles.DrawSolidArc(aiv.transform.position, Vector3.up, viewAngleB, 360 - aiv.GetViewAngle(), aiv.GetHearingRange());

        if (aiv.GetCanSeePlayer())
        {
            Handles.color = Color.white;
            Handles.DrawLine(aiv.transform.position, aiv.GetPlayer().position);
        }
    }
}
