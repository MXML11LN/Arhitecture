using CodeBase.Logic.Spawn;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(EnemySpawner))]
    public class EnemySpawnerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(EnemySpawner spawner, GizmoType gizmoType)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(spawner.transform.position, 1f);
            Gizmos.color = Color.white;
            if (SceneViewRange(spawner))
            {
                DrawMonsterName(spawner);
                Gizmos.DrawIcon(spawner.transform.position + Vector3.up, "scull.png", false);
            }
        }

        private static void DrawMonsterName(EnemySpawner spawner)
        {
            var textColor = Color.white;
            var anchor = Vector2.up * 4;
            var textSize = 20f;
            string text = spawner.monsterTypeId.ToString();
            var pos = spawner.transform.position;
            var view = SceneView.currentDrawingSceneView;
            if (!view)
                return;
            Vector3 screenPosition = view.camera.WorldToScreenPoint(pos);
            if (screenPosition.y < 0 || screenPosition.y > view.camera.pixelHeight || screenPosition.x < 0 ||
                screenPosition.x > view.camera.pixelWidth || screenPosition.z < 0)
                return;
            var pixelRatio = HandleUtility.GUIPointToScreenPixelCoordinate(Vector2.right).x -
                             HandleUtility.GUIPointToScreenPixelCoordinate(Vector2.zero).x;
            Handles.BeginGUI();
            var style = new GUIStyle(GUI.skin.label)
            {
                fontSize = (int)textSize,
                normal = new GUIStyleState() { textColor = textColor }
            };
            Vector2 size = style.CalcSize(new GUIContent(text)) * pixelRatio;
            var alignedPosition =
                ((Vector2)screenPosition +
                 size * ((anchor + Vector2.left + Vector2.up) / 2f)) * (Vector2.right + Vector2.down) +
                Vector2.up * view.camera.pixelHeight;
            GUI.Label(new Rect(alignedPosition / pixelRatio, size / pixelRatio), text, style);
            Handles.EndGUI();
        }

        private static bool SceneViewRange(EnemySpawner spawner)
        {
            Camera cam = SceneView.currentDrawingSceneView.camera;
            return Vector3.Distance(cam.transform.position, spawner.transform.position) < 40f;
        }
    }
}