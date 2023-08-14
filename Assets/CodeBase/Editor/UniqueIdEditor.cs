using System;
using System.Linq;
using CodeBase.Logic.Spawn;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(UniqueId))]
    public class UniqueIdEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            var uniqueId = target as UniqueId;
            if(IsPrefab(uniqueId))
                return;
            
            
            if (string.IsNullOrEmpty(uniqueId.Id))
                GenerateId(uniqueId);
            else
            {
                UniqueId[] uniqueIds = FindObjectsOfType<UniqueId>();
                if(uniqueIds.Any(other =>other!=uniqueId && other == uniqueId))
                    GenerateId(uniqueId);
            }
        }

        private bool IsPrefab(UniqueId uniqueId) => 
            uniqueId.gameObject.scene.rootCount == 0;

        private void GenerateId(UniqueId uniqueId)
        {
            uniqueId.Id = $"{uniqueId.gameObject.scene.name}_{Guid.NewGuid().ToString()}";
            if (!Application.isPlaying)
            {
                EditorUtility.SetDirty(uniqueId);
                EditorSceneManager.MarkSceneDirty(uniqueId.gameObject.scene);
            }
             
           
        }
    }
}