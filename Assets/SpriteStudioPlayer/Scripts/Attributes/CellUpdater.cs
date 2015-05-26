using UnityEngine;
using System.Collections;

namespace a.spritestudio.attribute
{
    /// <summary>
    /// 参照するセルマップの更新
    /// </summary>
    public class CellUpdater
        : AttributeBase
    {
        /// <summary>
        /// セルのインデックス
        /// </summary>
        private int index_;

        /// <summary>
        /// セル内の切り取りのインデックス
        /// </summary>
        private int fragment_;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cellIndex"></param>
        /// <param name="mapIndex"></param>
        public CellUpdater( int cellIndex, int mapIndex )
        {
            index_ = cellIndex;
            fragment_ = mapIndex;
        }

        /// <summary>
        /// 処理
        /// </summary>
        /// <param name="part"></param>
        protected override void OnUpdate( SpritePart part )
        {
            part.SetCellMap( index_, fragment_ );
        }
    }
}
