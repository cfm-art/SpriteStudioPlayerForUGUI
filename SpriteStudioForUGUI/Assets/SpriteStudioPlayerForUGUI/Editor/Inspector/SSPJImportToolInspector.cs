using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace a.spritestudio.editor.inspector
{
    /// <summary>
    /// インポートツールのインスペクター
    /// </summary>
    [CustomEditor( typeof( SSPJImportTool ))]
    public class SSPJImportToolInspector
        :Editor
    {
        /// <summary>
        /// 全てチェック
        /// </summary>
        private bool allCheck_ = false;

        /// <summary>
        /// 折りたたみ
        /// </summary>
        private Dictionary<string, bool> folds_ = new Dictionary<string,bool>();

        public override void OnInspectorGUI()
        {
            var tool = target as SSPJImportTool;
            if ( tool == null ) {
                base.OnInspectorGUI();
                return;
            }
            folds_ = folds_ ?? new Dictionary<string, bool>();


            GUILayout.Label( tool.name + " のインポート" );

            GUILayout.Label( "出力先：" + MenuItems.ExportPath );
            if ( !SpriteStudioImporter.IsValidExportPath( MenuItems.ExportPath ) ) {
                GUIStyleState state = new GUIStyleState();
                state.textColor = Color.red;

                GUIStyle style = new GUIStyle( GUI.skin.label );
                style.normal = state;

                GUILayout.Label( "出力先パスが正しくない可能性があります。\nメニューの「SpriteStudioForUGUI/出力先設定」\nから設定してください。", style );
                GUILayout.Space( 12 );
            }

            // セルマップ
            GUILayout.Label( "セルマップ" );
            using ( new Horizontal() ) {
                GUILayout.Space( 12 );
                using ( new Vertical() ) {
                    foreach ( var cell in tool.CellMaps ) {
                        GUILayout.Label( cell );
                    }
                }
            }

            GUILayout.Space( 12 );

            using ( new Horizontal() ) {
                GUILayout.Label( "アニメーション" );

                // 全てチェック
                bool allCheck = GUILayout.Toggle( allCheck_, "全てチェック" );
                if ( allCheck != allCheck_ ) {
                    allCheck_ = allCheck;
                    foreach ( var animation in tool.Animations ) {
                        animation.IsImport = allCheck;
                    }
                }
            }
            GUILayout.Space( 12 );

            // アニメーション一覧
            foreach ( var group in (from x in tool.Animations group x by x.File) ) {
                var first = group.First();
                using ( new Horizontal() ) {
                    GUILayout.Space( 12 );
                    using ( new Vertical() ) {
                        bool fold;
                        fold = EditorGUILayout.Foldout(
                                    folds_.TryGetValue( first.File, out fold ) && fold,
                                    first.File );
                        if ( folds_[first.File] = fold ) {
                            foreach ( var animation in group ) {
                                using ( new Horizontal() ) {
                                    GUILayout.Space( 12 );
                                    animation.IsImport = GUILayout.Toggle( animation.IsImport, "", GUILayout.MaxWidth( 24 ) );
                                    GUILayout.Label( animation.Animation );
                                }
                            }
                        }
                    }
                }
                GUILayout.Space( 12 );
            }

            if ( GUI.changed ) {
                EditorUtility.SetDirty( tool );
            }

            // インポートボタン
            if ( GUILayout.Button( "インポート" ) ) {
                Tracer.enable = MenuItems.ImportLog;
                Tracer.Startup( MenuItems.LogLevel );
                Tracer.Log( "Start import : " + tool.name );
                var targets = tool.Targets;
                SpriteStudioImporter.Import( tool.FullPath, targets );
                Tracer.Dump();
            }
        }
    }
}
