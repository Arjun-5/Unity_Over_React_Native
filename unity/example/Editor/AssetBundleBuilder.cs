using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class AssetBundleBuilder : Editor
{
    [MenuItem("Assets/Generate AssetBundles")]
    static void BuilAllAssetBundles()
    {
        BuildPipeline.BuildAssetBundles(@"C:\Users\Rahul\Desktop\AssetBundles",BuildAssetBundleOptions.ChunkBasedCompression,BuildTarget.Android);
    }
}