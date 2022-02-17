using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor.Callbacks;
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityEditor.PackageManager.UI
{
    public class SwitchingPlatform
    {
        private static AddRequest Request;
        
        [DidReloadScripts]
        [MenuItem("Example/Switch Platform")]
        public static void SwitchPlatform()
        {
            if (EditorUserBuildSettings.activeBuildTarget!=UnityEditor.BuildTarget.Android)
            {
                //PackageManagerExtensions.RegisterExtension();
                // パッケージをプロジェクトに加える
                //Request = Client.Add("com.unity.textmeshpro");
                EditorApplication.update += Progress;   
            }
        }
        
        static void Progress()
        {
            if (Request.IsCompleted)
            {
                if (Request.Status == StatusCode.Success)
                    Debug.Log("Installed: " + Request.Result.packageId);
                else if (Request.Status >= StatusCode.Failure)
                    Debug.Log(Request.Error.message);

                EditorApplication.update -= Progress;
                EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.Android);
            }
        }
    }
}