using UnityEditor;
using UnityEngine;

public class BuildScript 
{
    static string[] devScene = { "Assets/Scenes/MainMenu.unity", "Assets/Scenes/LobbyScene.unity", "Assets/Scenes/Level1.unity" };
    static string[] relScene = { "Assets/Scenes/Game.unity" };
	// Use this for initialization

    #region IOS Build
    [MenuItem("Build/IOs/Development", false, 1)]
    static void BuildiOSDev () 
    {
        BuildiOS (true);
    }

    [MenuItem("Build/IOs/Release", false, 2)]
    static void BuildiOSRelease () 
    {
        BuildiOS (false);
    }

    [MenuItem("Build/IOs/All", false, 14)]
    static void BuildiOSAll () 
    {
        BuildiOS (true);
        BuildiOS (false);
    }

    static void BuildiOS (bool isDev) 
    {
        string buildPath = Application.dataPath.Replace("/Assets", "/Builds")+"/iOS/";

        if (isDev)
            buildPath += "Dev";

        buildPath += "_"+PlayerSettings.bundleVersion.Replace(".","_");

        BuildOptions options = isDev ? BuildOptions.Development : BuildOptions.None;

        if (EditorUtility.DisplayDialog("Append Project?", "Do you want to append the XCode project or start over?", "Append", "Start Over"))
        {
            options |= BuildOptions.AcceptExternalModificationsToPlayer;
        }

        options |= BuildOptions.AutoRunPlayer;

        BuildPipeline.BuildPlayer(isDev ? devScene : relScene, buildPath, BuildTarget.iOS, options);

        EditorUtility.RevealInFinder(Application.dataPath.Replace("/Assets", "/Builds")+"/iOS/");
	}
    #endregion

    #region Android Build
    [MenuItem("Build/Android/Development", false, 1)]
    static void BuildAndroidDev () 
    {
        BuildAndroid (true);
    }

    [MenuItem("Build/Android/Release", false, 2)]
    static void BuildAndroidRelease () 
    {
        BuildAndroid (false);
    }

    [MenuItem("Build/Android/All", false, 14)]
    static void BuildAndroidAll () 
    {
        BuildAndroid (true);
        BuildAndroid (false);
    }

    static void BuildAndroid (bool isDev) 
    {
        string buildPath = Application.dataPath.Replace("/Assets", "/Builds")+"/Android/";

        buildPath += "_"+PlayerSettings.bundleVersion.Replace(".","_");

        if (isDev)
            buildPath += "Dev";

        BuildPipeline.BuildPlayer(isDev ? devScene : relScene, buildPath, BuildTarget.Android, isDev ? BuildOptions.Development : BuildOptions.None);

        EditorUtility.RevealInFinder(Application.dataPath.Replace("/Assets", "/Builds")+"/Android/");
    }
    #endregion

    #region WebGL Build
    [MenuItem("Build/WebGL/Development", false, 1)]
    static void BuildWebDev () 
    {
        BuildWeb (true);
    }

    [MenuItem("Build/WebGL/Release", false, 2)]
    static void BuildWebRelease () 
    {
        BuildWeb (false);
    }

    [MenuItem("Build/WebGL/All", false, 14)]
    static void BuildWebAll () 
    {
        BuildWeb (true);
        BuildWeb (false);
    }

    static void BuildWeb (bool isDev) 
    {
        string buildPath = Application.dataPath.Replace("/Assets", "/Builds")+"/WebGL/";

        if (isDev)
            buildPath += "Dev";

        buildPath += "_"+PlayerSettings.bundleVersion.Replace(".","_");

        BuildPipeline.BuildPlayer(isDev ? devScene : relScene, buildPath, BuildTarget.WebGL, isDev ? BuildOptions.Development : BuildOptions.None);

        EditorUtility.RevealInFinder(Application.dataPath.Replace("/Assets", "/Builds")+"/WebGL/");
    }
    #endregion


    #region PC Build
    [MenuItem("Build/PC/Development", false, 1)]
    static void BuildPCDev () 
    {
        BuildPC (true);
    }

    [MenuItem("Build/PC/Release", false, 2)]
    static void BuildPCRelease () 
    {
        BuildPC (false);
    }

    [MenuItem("Build/PC/All", false, 14)]
    static void BuildPCAll () 
    {
        BuildPC (true);
        BuildPC (false);
    }

    static void BuildPC (bool isDev) 
    {
        DefaultScenes();

        string buildPath = Application.dataPath.Replace("/Assets", "/Builds")+"/PC/";

        if (isDev)
            buildPath += "Dev";

        buildPath += PlayerSettings.bundleVersion.Replace(".","_");

        BuildPipeline.BuildPlayer(isDev ? devScene : relScene, buildPath+"_Windows", BuildTarget.StandaloneWindows, isDev ? BuildOptions.Development : BuildOptions.None);
        BuildPipeline.BuildPlayer(isDev ? devScene : relScene, buildPath+"_Mac", BuildTarget.StandaloneOSXIntel, isDev ? BuildOptions.Development : BuildOptions.None);

        EditorUtility.RevealInFinder(Application.dataPath.Replace("/Assets", "/Builds")+"/PC/");

        //BuildPipeline.BuildPlayer(isDev ? devScene : relScene, buildPath+"_Linux", BuildTarget.StandaloneLinux, isDev ? BuildOptions.Development : BuildOptions.None);
    }
    #endregion

    #region Functions
    static void DefaultScenes()
    {
        if (EditorUtility.DisplayDialog("Use Default Scenes?", "Do you want to use the default scenes?", "Default", "Script"))
        {
            EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
            string[] scenesPath = new string[scenes.Length];

            for (int i = 0; i < scenesPath.Length; i++)
            {
                scenesPath[i] = scenes[i].path;
            }

            devScene = scenesPath;
            relScene = devScene;
        }
    }
    #endregion

    [MenuItem("Build/Build All", false, 14)]
    static void BuildAll () 
    {
        BuildiOSAll();
        BuildAndroidAll();
        BuildWebAll();
        BuildPCAll();
    }
}
