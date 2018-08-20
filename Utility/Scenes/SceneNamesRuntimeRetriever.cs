using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace UnityCore
{
    public class RuntimeSceneNamesRetriever : MonoSingleton<RuntimeSceneNamesRetriever>
    {
        public static readonly string ScenesNamesResourcePath = "Assets/Resources/ScenesNames.asset";

        public ScriptableSceneNames ScenesList { get; private set; }

        private void Start()
        {
            var resourceName = ScenesNamesResourcePath.Split('/').ToList().Last().Split('.').First();

            if (ScenesList == null)
                ScenesList = Resources.Load<ScriptableSceneNames>(resourceName);

            Regex regex = new Regex(@"([^/]*/)*([\w\d\-]*)\.unity");

            for (int i = 0; i < ScenesList.Names.Count; i++)
            {
                ScenesList.Names[i] = regex.Replace(ScenesList.Names[i], "$2");
            }
        }

        [MenuItem("Scenes/Serialize Build Scenes")]
        static void GetSceneNames()
        {
            EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;

            var scenesList = (ScriptableSceneNames)AssetDatabase.LoadAssetAtPath(ScenesNamesResourcePath, typeof(ScriptableSceneNames));

            if (scenesList == null)
            {
                scenesList = ScriptableObject.CreateInstance<ScriptableSceneNames>();
                AssetDatabase.CreateAsset(scenesList, ScenesNamesResourcePath);
            }

            scenesList.Names = new List<string>();

            foreach (var scene in scenes)
            {
                scenesList.Names.Add(scene.path);
            }

            AssetDatabase.SaveAssets();
        }
    }
}