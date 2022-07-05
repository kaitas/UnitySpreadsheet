/*
 GSSReader を AddComponent した GameObject のインスペクターに「スプレッドシート読み込み」ボタンを表示するエディタ拡張スクリプトです。
ボタンを押すとスプレッドシートのデータを取得できます。
https://dokuro.moe/unity-how-to-import-google-spreadsheet-data-directly/
 */

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GSSReader))]
public class GSSReaderInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var r = target as GSSReader;
        var t = "スプレッドシート読み込み";

        EditorGUI.BeginDisabledGroup(r.IsLoading);
        if (GUILayout.Button(t))
        {
            r.Reload();
        }
        EditorGUI.EndDisabledGroup();
    }
}