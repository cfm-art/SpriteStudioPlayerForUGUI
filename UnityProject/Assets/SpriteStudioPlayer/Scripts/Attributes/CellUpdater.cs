using UnityEngine;
using System.Collections;

namespace a.spritestudio.attribute
{
    /// <summary>
    /// 参照するセルマップの更新
    /// </summary>
    public class CellUpdater
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cellIndex"></param>
        /// <param name="mapIndex"></param>
        public static AttributeBase Create( int cellIndex, int mapIndex )
        {
            var self = new AttributeBase( AttributeBase.Target.kCell, 
                new int[] { cellIndex, mapIndex }, null, null );
            return self;
        }

        /// <summary>
        /// 処理
        /// </summary>
        /// <param name="part"></param>
        public static void OnUpdate( SpritePart part, AttributeBase attribute )
        {
            part.SetCellMap( attribute.@int( 0 ), attribute.@int( 1 ) );
        }
    }
}
