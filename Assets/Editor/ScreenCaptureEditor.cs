using System.IO;
using UnityEditor;
using UnityEngine;
public class ScreenCaptureEditor : EditorWindow
{
    private static string directory = "Screenshots/Capture/";
    private static string latestScreenshot = "";
    private bool initDone = false;

    private GUIStyle BigText;

    [MenuItem("Screenshot/Take a Screenshot")]
    private static void TakeScreenshot()
    {
        Directory.CreateDirectory(directory);
        var currentTime = System.DateTime.Now;
        var filename = currentTime.ToString().Replace('/', '-').Replace(':', '_') + ".png";
        var path = directory + filename;
        ScreenCapture.CaptureScreenshot(path);
        latestScreenshot = path;
        Debug.Log("Screenshot saved: <b>\"" + path + "\"</b> with resolution <b>" + GetResolution2() + "</b>");
    }

    [MenuItem("Screenshot/Reveal in Explorer")]
    private static void ShowFolder()
    {
        if (File.Exists(latestScreenshot))
        {
            EditorUtility.RevealInFinder(latestScreenshot);
            return;
        }
        Directory.CreateDirectory(directory);
        EditorUtility.RevealInFinder(directory);
    }
    void InitStyles()
    {
        initDone = true;
        BigText = new GUIStyle(GUI.skin.label)
        {
            fontSize = 20,
            fontStyle = FontStyle.Bold
        };
    }

    private void OnGUI()
    {
        if (!initDone)
        {
            InitStyles();
        }

        GUILayout.Label("Screen Capture", BigText);
        if (GUILayout.Button("Take a screenshot"))
        {
            TakeScreenshot();
        }
        GUILayout.Label("Resolution: " + GetResolution2());
        if (GUILayout.Button("Reveal in Explorer"))
        {
            ShowFolder();
        }
        GUILayout.Label("Directory: " + directory);

    }

    [MenuItem("Screenshot/Open Window")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(ScreenCaptureEditor));
    }

    private static EditorWindow GetGameWindow()
    {
        System.Reflection.Assembly assembly = typeof(UnityEditor.EditorWindow).Assembly;
        System.Type type = assembly.GetType("UnityEditor.GameView");
        EditorWindow gameview = EditorWindow.GetWindow(type);
        return gameview;
    }

    private static string GetResolution()
    {
        // Interesting method using System Reflection
        var gameView = GetMainGameView();
        var prop = gameView.GetType().GetProperty("currentGameViewSize", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var gvsize = prop.GetValue(gameView, new object[0] { });
        var gvSizeType = gvsize.GetType();

        var ScreenHeight = (int)gvSizeType.GetProperty("height", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).GetValue(gvsize, new object[0] { });
        var ScreenWidth = (int)gvSizeType.GetProperty("width", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).GetValue(gvsize, new object[0] { });

        return ScreenWidth.ToString() + "x" + ScreenHeight.ToString();

    }
    static UnityEditor.EditorWindow GetMainGameView()
    {
        System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
        System.Reflection.MethodInfo GetMainGameView = T.GetMethod("GetMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        System.Object Res = GetMainGameView.Invoke(null, null);
        return (UnityEditor.EditorWindow)Res;
    }

    private static string GetResolution2()
    {
        // Actual much easier code
        Vector2 size = UnityEditor.Handles.GetMainGameViewSize();
        Vector2Int sizeInt = new Vector2Int((int)size.x, (int)size.y);
        return sizeInt.x.ToString() + "x" + sizeInt.y.ToString();
    }

}