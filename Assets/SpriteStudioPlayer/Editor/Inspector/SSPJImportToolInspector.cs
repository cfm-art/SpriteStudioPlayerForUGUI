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

        public override void OnInspectorGUI()
        {
            var tool = target as SSPJImportTool;
            if ( tool == null ) {
                base.OnInspectorGUI();
                return;
            }
            GUILayout.Label( tool.name + " のインポート" );

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
                        GUILayout.Label( first.File );
                        foreach ( var animation in group ) {
                            using ( new Horizontal() ) {
                                GUILayout.Space( 20 );
                                animation.IsImport = GUILayout.Toggle( animation.IsImport, "", GUILayout.MaxWidth( 24 ) );
                                GUILayout.Label( animation.Animation );
                            }
                        }
                    }
                }
                GUILayout.Space( 12 );
            }

            // インポートボタン
            if ( GUILayout.Button( "インポート" ) ) {
                Tracer.enable = MenuItems.ImportLog;
                Tracer.Startup();
                Tracer.Log( "Start import : " + tool.name );
                var targets = tool.Targets;
                SpriteStudioImporter.Import( tool.FullPath, targets );
                Tracer.Dump();
            }
        }
    }
}
