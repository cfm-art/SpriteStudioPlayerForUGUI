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
        [SerializeField]
        private int index_;

        /// <summary>
        /// セル内の切り取りのインデックス
        /// </summary>
        [SerializeField]
        private int fragment_;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cellIndex"></param>
        /// <param name="mapIndex"></param>
        public static CellUpdater Create( int cellIndex, int mapIndex )
        {
            var self = ScriptableObject.CreateInstance<CellUpdater>();
            self.index_ = cellIndex;
            self.fragment_ = mapIndex;
            return self;
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
