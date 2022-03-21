using UnityEngine;
using UnityEditor;

namespace StartFramework.GamePlay
{
    [CustomEditor(typeof(StartBezier))]
    public class DrawBezier : Editor
    {
        private void OnSceneViewGUI(SceneView sv)
        {
            StartBezier be = target as StartBezier;

            be.startPos = Handles.PositionHandle(be.startPos, Quaternion.identity);
            be.endPos = Handles.PositionHandle(be.endPos, Quaternion.identity);
            be.tangent0 = Handles.PositionHandle(be.tangent0, Quaternion.identity);
            be.tangent1 = Handles.PositionHandle(be.tangent1, Quaternion.identity);

            Handles.DrawBezier(be.startPos, be.endPos, be.tangent0, be.tangent1, be.color, null, be.width);
        }

        void OnEnable()
        {
            //SceneView.onSceneGUIDelegate += OnSceneViewGUI;
            SceneView.duringSceneGui += OnSceneViewGUI;
        }

        void OnDisable()
        {
            //SceneView.onSceneGUIDelegate -= OnSceneViewGUI;
            SceneView.duringSceneGui -= OnSceneViewGUI;
        }
    }
}