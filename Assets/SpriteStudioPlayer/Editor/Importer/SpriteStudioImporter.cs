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
                    Import( str );
                }
            }
        }

        /// <summary>
        /// インポート
        /// </summary>
        /// <param name="file"></param>
        private static void Import( string file )
        {
            // TODO: 出力先は自由に出来るようにする
            string exportPath = "Assets/Exports/";

            string path = Path.GetDirectoryName( file );

            // sspjのインポート
            var projectInformation = new SSPJImporter().Import( file );
            Debug.Log( projectInformation.ToString() );

            // ssceのインポート
            List<CellMap> cellMap = new List<CellMap>();
            foreach ( var cell in projectInformation.cellMaps ) {
                var importer = new SSCEImporter();
                var ssceInformation = importer.Import( path + '\\' + cell );
                Debug.Log( ssceInformation.ToString() );

                // エンジン側の形式へ変更
                var converter = new SSCEConverter();
                cellMap.Add( converter.Convert( path + '\\', ssceInformation ) );
            }
            
            // セルマップの保存
            for ( int i = 0; i < cellMap.Count; ++i ) {
                var cell = cellMap[i];
                string fileName = exportPath + "CellMaps/" + cell.name + ".asset";
                AssetDatabase.CreateAsset( cell, fileName );
                cellMap[i] = (CellMap) AssetDatabase.LoadAssetAtPath( fileName, typeof( CellMap ) );

                Debug.Log( "Save CellMap:" + fileName );
            }

            // ssaeのインポート
            List<GameObject> prefabs = null;
            try {
                foreach ( var animation in projectInformation.animePacks ) {
                    var ssaeInformation = new SSAEImporter().Import( path + '\\' + animation );
                    Debug.Log( ssaeInformation.ToString() );

                    var converter = new SSAEConverter();
                    prefabs = converter.Convert( projectInformation, ssaeInformation, cellMap );
                }

                if ( prefabs != null ) {
                    // prefab保存
                    foreach ( var prefab in prefabs ) {
                        string fileName = exportPath + "Sprites/" + prefab.name + ".prefab";
                        PrefabUtility.CreatePrefab( fileName, prefab );

                        Debug.Log( "Save Prefab:" + fileName );
                    }
                }
            } finally {
                // ヒエラルキーにGOが残らないようにする
                if ( prefabs != null ) {
                    foreach ( var p in prefabs ) {
                        GameObject.DestroyImmediate( p );
                    }
                }
            }
        }
    }
}