using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

namespace a.spritestudio.editor
{
    /// <summary>
    /// 
    /// </summary>
    public class SpriteStudioImporter
        : AssetPostprocessor
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="importedAssets"></param>
        /// <param name="deletedAssets"></param>
        /// <param name="movedAssets"></param>
        /// <param name="movedFromAssetPaths"></param>
        static void OnPostprocessAllAssets(
            string[] importedAssets,
            string[] deletedAssets,
            string[] movedAssets,
            string[] movedFromAssetPaths )
        {
            foreach ( string str in importedAssets ) {
                if ( str.EndsWith( ".sspj" ) ) {
                    bool isAutoImport = EditorPrefs.GetBool( "assfugui-ai", false );
                    Tracer.enable = MenuItems.ImportLog;
                    Tracer.Startup();
                    Tracer.Log( "Start import : " + str );
                    if ( isAutoImport ) {
                        Import( str, null );
                    } else {
                        CreateImportTool( str );
                    }
                    Tracer.Dump();
                }
            }
        }

        /// <summary>
        /// インポート用のツールだけ生成
        /// </summary>
        /// <param name="file"></param>
        private static void CreateImportTool( string file )
        {
            string path = Path.GetDirectoryName( file );

            // インポート用ツール生成
            SSPJImportTool tool = SSPJImportTool.Create( Path.GetFileNameWithoutExtension( file ) );

            // sspjのインポート
            var projectInformation = new SSPJImporter().Import( file );
            Tracer.Log( projectInformation.ToString() );

            foreach ( var cell in projectInformation.cellMaps ) {
                tool.AddCell( Path.GetFileNameWithoutExtension( cell ) );
            }

            foreach ( var animation in projectInformation.animePacks ) {
                // ssaeをパース
                var ssaeInformation = new SSAEImporter().ImportNamesOnly( path + '\\' + animation );
                Tracer.Log( ssaeInformation.ToString() );

                var name = Path.GetFileNameWithoutExtension( animation );
                foreach ( var fragment in ssaeInformation ) {
                    tool.AddAnimation( name, fragment );
                }
            }

            AssetDatabase.CreateAsset( tool, file + ".asset" );
        }

        /// <summary>
        /// インポート
        /// </summary>
        /// <param name="file"></param>
        /// <param name="targets"></param>
        internal static void Import( string file, SSPJImportTool.TargetAnimation[] targets )
        {
            // TODO: 出力先は自由に出来るようにする
            string exportPath = MenuItems.ExportPath + Path.GetFileNameWithoutExtension( file ) + "/";

            string path = Path.GetDirectoryName( file );

            // インポート用ツール生成
            SSPJImportTool tool = targets == null ? SSPJImportTool.Create( Path.GetFileNameWithoutExtension( file ) ) : null;

            // マテリアル
            Dictionary<types.AlphaBlendType, Material> materials = new Dictionary<types.AlphaBlendType, Material>() {
                { types.AlphaBlendType.kAdd, AssetDatabase.LoadAssetAtPath( "Assets/SpriteStudioPlayerForUGUI/Materials/Add.mat", typeof( Material ) ) as Material },
                { types.AlphaBlendType.kMix, AssetDatabase.LoadAssetAtPath( "Assets/SpriteStudioPlayerForUGUI/Materials/Mix.mat", typeof( Material ) ) as Material },
                { types.AlphaBlendType.kMul, AssetDatabase.LoadAssetAtPath( "Assets/SpriteStudioPlayerForUGUI/Materials/Mul.mat", typeof( Material ) ) as Material },
                { types.AlphaBlendType.kSub, AssetDatabase.LoadAssetAtPath( "Assets/SpriteStudioPlayerForUGUI/Materials/Sub.mat", typeof( Material ) ) as Material },
            };

            // sspjのインポート
            var projectInformation = new SSPJImporter().Import( file );

            // ssceのインポート
            List<CellMap> cellMap = new List<CellMap>();
            foreach ( var cell in projectInformation.cellMaps ) {
                // ssceをパース
                var importer = new SSCEImporter();
                var ssceInformation = importer.Import( path + '\\' + cell );
                Tracer.Log( ssceInformation.ToString() );

                // エンジン側の形式へ変更
                var converter = new SSCEConverter();
                cellMap.Add( converter.Convert( path + '\\', ssceInformation ) );

                if ( tool != null ) {
                    tool.AddCell( Path.GetFileNameWithoutExtension( cell ) );
                }
            }
            
            // セルマップの保存
            CreateFolders( exportPath + "CellMaps" );
            for ( int i = 0; i < cellMap.Count; ++i ) {
                var cell = cellMap[i];
                string fileName = exportPath + "CellMaps/" + cell.name + ".asset";
                var savedCellMap = AssetDatabase.LoadAssetAtPath( fileName, typeof( CellMap ) );
                if ( savedCellMap == null ) {
                    AssetDatabase.CreateAsset( cell, fileName );
                } else {
                    // 既にあるので上書きする
                    cell.CopyTo( savedCellMap as CellMap );
                    EditorUtility.SetDirty( savedCellMap );
                }
                cellMap[i] = (CellMap) AssetDatabase.LoadAssetAtPath( fileName, typeof( CellMap ) );

                Tracer.Log( "Save CellMap:" + fileName );
            }

            // ssaeのインポート
            List<GameObject> prefabs = new List<GameObject>();
            try {
                string basePath = exportPath + "Sprites/";
                foreach ( var animation in projectInformation.animePacks ) {
                    var ssceName = Path.GetFileNameWithoutExtension( animation );
                    if ( targets != null ) {
                        // 対象に含まれているか判定
                        bool isLoad = System.Array.Find( targets, ( o ) => o.File == ssceName ) != null;
                        if ( !isLoad ) { continue; }
                    }
                    // ssaeをパース
                    var ssaeInformation = new SSAEImporter().Import( path + '\\' + animation, ssceName, targets );
                    Tracer.Log( ssaeInformation.ToString() );

                    // GameObjectへ変換
                    var converter = new SSAEConverter();
                    var result = converter.Convert( projectInformation,
                            ssaeInformation, cellMap, materials );
                    prefabs.AddRange( result.animations );

                    // prefab保存
                    string name = Path.GetFileNameWithoutExtension( animation );
                    CreateFolders( basePath + name );
                    foreach ( var prefab in result.animations ) {
                        string fileName = basePath + name + "/" + prefab.name + ".prefab";
                        var savedPrefab = AssetDatabase.LoadAssetAtPath( fileName, typeof( GameObject ) );
                        if ( savedPrefab == null ) {
                            PrefabUtility.CreatePrefab( fileName, prefab );
                        } else {
                            // 既にあるので置き換え
                            PrefabUtility.ReplacePrefab( prefab, savedPrefab );
                        }

                        Tracer.Log( "Save Prefab:" + fileName );

                        if ( tool != null ) {
                            tool.AddAnimation( name, prefab.name );
                        }
                    }
                }
                if ( tool != null ) {
                    AssetDatabase.CreateAsset( tool, file + ".asset" );
                }
            } finally {
                // ヒエラルキーにGOが残らないようにする
                if ( prefabs != null ) {
                    foreach ( var p in prefabs ) {
                        GameObject.DestroyImmediate( p );
                    }
                }
                AssetDatabase.SaveAssets();
            }
        }

        /// <summary>
        /// フォルダ生成
        /// </summary>
        /// <param name="path"></param>
        public static void CreateFolders( string path )
        {
            if ( !Directory.Exists( path ) ) {
                Directory.CreateDirectory( path );
                AssetDatabase.ImportAsset( path );
            }
        }
    }
}