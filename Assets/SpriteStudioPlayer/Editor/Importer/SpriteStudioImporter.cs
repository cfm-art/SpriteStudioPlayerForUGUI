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
            string path = Path.GetDirectoryName( file );
            var projectInformation = new SSPJImporter().Import( file );
            Debug.Log( projectInformation.ToString() );

            foreach ( var cell in projectInformation.cellMaps ) {
                var ssceInformation = new SSCEImporter().Import( path + '\\' + cell );
                Debug.Log( ssceInformation.ToString() );
            }

            foreach ( var animation in projectInformation.animePacks ) {
                new SSAEImporter().Import( path + '\\' + animation );
            }
        }
    }
}