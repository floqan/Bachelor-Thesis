#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoadAttribute]
public static class DefaultSceneLoader
{
    static DefaultSceneLoader()
    {
        if (ApplicationManager.useDefaultLoader)
        {
            EditorApplication.playModeStateChanged += LoadDefaultScene;
        }
    }

    static void LoadDefaultScene(PlayModeStateChange state)
    {
        if (ApplicationManager.useDefaultLoader)
        {
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            }

            if (state == PlayModeStateChange.EnteredPlayMode)
            {
                EditorSceneManager.LoadScene(0);
            }
        }
        
    }
}
#endif