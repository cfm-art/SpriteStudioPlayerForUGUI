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
        /// 処理
        /// </summary>
        /// <param name="part"></param>
        protected override void OnUpdate( SpritePart part )
        {
            part.CellMap = part.Root.CellMap( index_ );
        }
    }
}
