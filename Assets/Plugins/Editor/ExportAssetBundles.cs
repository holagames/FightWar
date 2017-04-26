// C# Example
// Builds an asset bundle from the selected objects in the project view.
// Once compiled go to "Menu" -> "Assets" and select one of the choices
// to build the Asset Bundle

using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;


public class ExportAssetBundles
{
    [MenuItem("Assets/CN Build AssetBundle From Selection - Track dependencies")]
    static void CNExportResource()
    {
        // Bring up save panel
        Object[] ResourceName = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

        string Name = "";
        foreach (Object o in ResourceName)
        {
            if (o.name.IndexOf("BG") > -1 && o.name.Length > 2)
            {
                Name = o.name;
            }
            else if (o.name.IndexOf("Monster") > -1 && o.name.IndexOf("_") == -1 && o.name.Length > 7)
            {
                Name = o.name;
                break;
            }
            else if (o.name.IndexOf("Role") > -1 && o.name.IndexOf("_") == -1 && o.name.Length > 4)
            {
                Name = o.name;
                break;
            }
            else if (o.name.IndexOf("Head") > -1 && o.name.Length > 4)
            {
                Name = o.name;
            }
        }
        if (Name == "")
        {
            ResourceName = Selection.GetFiltered(typeof(TextAsset), SelectionMode.DeepAssets);
            Name = ResourceName[0].name;
        }

        string path = EditorUtility.SaveFilePanel("Save Resource", "D:\\Project\\SanGo\\AssetBundles\\CN\\Web", Name, "unity3d");
        if (path.Length != 0)
        {
            // Build the resource file from the active selection.
            Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, path, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets);
            Selection.objects = selection;
        }
    }
    [MenuItem("Assets/CN Build Android AssetBundle From Selection - Track dependencies")]
    static void CNExportAndroidResource()
    {
        // Bring up save panel
        Object[] ResourceName = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

        string Name = "";
        foreach (Object o in ResourceName)
        {
            if (o.name.IndexOf("BG") > -1 && o.name.Length > 2)
            {
                Name = o.name;
            }
            else if (o.name.IndexOf("Monster") > -1 && o.name.IndexOf("_") == -1 && o.name.Length > 7)
            {
                Name = o.name;
                break;
            }
            else if (o.name.IndexOf("Role") > -1 && o.name.IndexOf("_") == -1 && o.name.Length > 4)
            {
                Name = o.name;
                break;
            }
            else if (o.name.IndexOf("Head") > -1 && o.name.Length > 4)
            {
                Name = o.name;
            }
        }
        if (Name == "")
        {
            ResourceName = Selection.GetFiltered(typeof(TextAsset), SelectionMode.DeepAssets);
            Name = ResourceName[0].name;
        }

        string path = EditorUtility.SaveFilePanel("Save Resource", "D:\\Project\\Base\\Assets\\AssetBundles\\Android", Name, "unity3d");
        if (path.Length != 0)
        {
            // Build the resource file from the active selection.
            Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, path, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets, BuildTarget.Android);
            Selection.objects = selection;
        }
    }
    [MenuItem("Assets/CN Build iPhone AssetBundle From Selection - Track dependencies")]
    static void CNExportiPhoneResource()
    {
        // Bring up save panel
        Object[] ResourceName = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

        string Name = "";
        foreach (Object o in ResourceName)
        {
            if (o.name.IndexOf("BG") > -1 && o.name.Length > 2)
            {
                Name = o.name;
            }
            else if (o.name.IndexOf("Monster") > -1 && o.name.IndexOf("_") == -1 && o.name.Length > 7)
            {
                Name = o.name;
                break;
            }
            else if (o.name.IndexOf("Role") > -1 && o.name.IndexOf("_") == -1 && o.name.Length > 4)
            {
                Name = o.name;
                break;
            }
            else if (o.name.IndexOf("Head") > -1 && o.name.Length > 4)
            {
                Name = o.name;
            }
        }
        if (Name == "")
        {
            ResourceName = Selection.GetFiltered(typeof(TextAsset), SelectionMode.DeepAssets);
            Name = ResourceName[0].name;
        }

        string path = EditorUtility.SaveFilePanel("Save Resource", "D:\\Project\\SanGo\\AssetBundles\\CN\\iPhone", Name, "unity3d");
        if (path.Length != 0)
        {
            // Build the resource file from the active selection.
            Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, path, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets, BuildTarget.iPhone);
            Selection.objects = selection;
        }
    }

    [MenuItem("Assets/Build AssetBundle Binary file From Selection - Track dependencies and encrypt ")]
    static void ExportResource()
    {
        // Bring up save panel
        string path = EditorUtility.SaveFilePanel("Save Resource", "", "New Resource", "unity3d");
        if (path.Length != 0)
        {
            // Build the resource file from the active selection.
            Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, path, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets);
            Selection.objects = selection;


            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
            byte[] buff = new byte[fs.Length + 1];
            fs.Read(buff, 0, (int)fs.Length);
            buff[buff.Length - 1] = 0;
            fs.Close();
            File.Delete(path);

            string BinPath = path.Substring(0, path.LastIndexOf('.')) + "1.bytes";
            FileStream cfs = new FileStream(BinPath, FileMode.Create);
            cfs.Write(buff, 0, buff.Length);
            buff = null;
            cfs.Close();

            //string AssetsPath = BinPath.Substring(BinPath.IndexOf("Assets"));
            //Object ta = AssetDatabase.LoadAssetAtPath(AssetsPath, typeof(Object));
            //BuildPipeline.BuildAssetBundle(ta, null, path);
        }
    }
    [MenuItem("Assets/Build Android Script AssetBundle From Selection")]
    static void ExportResourceNoTrack()
    {
        // Bring up save panel
        Object[] ResourceName = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

        foreach (Object o in ResourceName)
        {
            string Name = o.name;
            string path = EditorUtility.SaveFilePanel("Save Resource", "D:\\Project\\Base\\Assets\\AssetBundles\\Android", Name, "unity3d");
            if (path.Length != 0)
            {
                BuildPipeline.BuildAssetBundle(Selection.activeObject, Selection.objects, path, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets, BuildTarget.Android);
            }
        }
    }

    [MenuItem("Assets/[Atlas] CN Build Android AssetBundle From Selection - Track dependencies")]
    static void CNExportAndroidAtlasResource()
    {
        // Bring up save panel
        Object[] ResourceName = Selection.GetFiltered(typeof(GameObject), SelectionMode.DeepAssets);
        foreach (Object o in ResourceName)
        {
            Debug.Log(o.GetType());
        }
        string Name = "";
        Name = ResourceName[0].name;

        Debug.Log(Application.absoluteURL);
        string path = EditorUtility.SaveFilePanel("Save Resource", "D:\\Project\\Base\\Assets\\AssetBundles\\Android\\Atlas", Name, "unity3d");
        if (path.Length != 0)
        {
            // Build the resource file from the active selection.
            Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, path, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets, BuildTarget.Android);
            Selection.objects = selection;
        }
    }

    [MenuItem("Assets/LUA Android AssetBundle From Selection")]
    static void LUAExportAndroidResource()
    {
        // Bring up save panel
        Object[] ResourceName = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

        foreach (Object o in ResourceName)
        {
            string Name = o.name;
            string path = "D:\\Project\\Base\\Assets\\AssetBundles\\Android\\Lua\\LuaAsset\\" + Name + ".unity3d";
            if (path.Length != 0)
            {
                // Build the resource file from the active selection.
                Object[] selection = new Object[1];
                selection[0] = o;
                BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, path, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets, BuildTarget.Android);
                Selection.objects = selection;
            }
        }
    }


    public class BuildSceneEditor
    {
        [@MenuItem("build/BuildAndroidMyWarStreamed")]
        static void BuildAndroidMyWar()
        {
            string[] levels = new string[] { "Assets/Scenes/MyWar.unity" };
            BuildPipeline.BuildStreamedSceneAssetBundle(levels, "MyWar_Android.unity3d", BuildTarget.Android);
        }

        [@MenuItem("build/BuildAndroidCreaterStreamed")]
        static void BuildAndroidCreater()
        {
            string[] levels = new string[] { "Assets/Scenes/Creater.unity" };
            BuildPipeline.BuildStreamedSceneAssetBundle(levels, "Creater_Android.unity3d", BuildTarget.Android);
        }

        [@MenuItem("build/BuildiOSMyWarStreamed")]
        static void BuildiOSMyWar()
        {
            string[] levels = new string[] { "Assets/Scenes/MyWar.unity" };
            BuildPipeline.BuildStreamedSceneAssetBundle(levels, "MyWar_iOS.unity3d", BuildTarget.iPhone);
        }

        [@MenuItem("build/BuildiOSCreaterStreamed")]
        static void BuildiOSCreater()
        {
            string[] levels = new string[] { "Assets/Scenes/Creater.unity" };
            BuildPipeline.BuildStreamedSceneAssetBundle(levels, "Creater_iOS.unity3d", BuildTarget.iPhone);
        }

        [@MenuItem("build/BuildZip")]
        static void BuildiZip()
        {
            Object[] ResourceName = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

            foreach (Object o in ResourceName)
            {
                string Name = o.name;
                string path = "D:\\Project\\Base\\Assets\\AssetBundles\\Android\\Lua\\" + Name + ".txt";

                using (ZipOutputStream zos = new ZipOutputStream(File.Open(path, FileMode.OpenOrCreate)))
                {
                    zos.Password = "projectn";

                    FileStream fs = File.OpenRead("D:\\Project\\Base\\Assets\\Resources\\Lua\\" + Name + ".txt");
                    Debug.Log(fs.Length);
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);

                    ZipEntry entry = new ZipEntry(Name + ".txt");
                    zos.PutNextEntry(entry);
                    zos.Write(buffer, 0, buffer.Length);
                }
            }
        }

    }
}