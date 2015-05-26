using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

using a.spritestudio;

namespace a.spritestudio.editor
{
    /// <summary>
    /// .ssceのインポートした情報をエンジン側のクラスへ変換する
    /// </summary>
    public class SSCEConverter
    {
        /// <summary>
        /// 変換
        /// </summary>
        /// <param name="data"></param>
        /// <param name="texturePath"></param>
        /// <returns></returns>
        public CellMap Convert( string texturePath, SSCEImporter.Information data )
        {
            CellMap cellMap = CellMap.Create();
            cellMap.name = data.name;

            var texture = (Texture) AssetDatabase.LoadAssetAtPath( texturePath + data.imagePath, typeof( Texture ) );
            texture.wrapMode = data.wrapMode;
            texture.filterMode = data.filterMode;
            EditorUtility.SetDirty( texture );

            cellMap.Texture = texture;
            foreach ( var cell in data.cellMaps ) {
                cellMap.AddCell( cell.name, cell.uv, cell.size );
            }

            return cellMap;
        }
    }
}
