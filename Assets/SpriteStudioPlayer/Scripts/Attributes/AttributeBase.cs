using UnityEngine;
using System.Collections;

namespace a.spritestudio.attribute
{
    /// <summary>
    /// キーフレームの基本
    /// </summary>
    [System.Serializable]
    public class AttributeBase
    {
        /// <summary>
        /// 
        /// </summary>
        public enum Target
        {
            kCell,
            kFliper,
            kPosition,
            kPriority,
            kRotation,
            kScaling,
            kTransparency,
            kVisibility
        }

        /// <summary>
        /// 関数テーブル
        /// </summary>
        private static System.Action<SpritePart, AttributeBase>[] Functions = new System.Action<SpritePart, AttributeBase>[] {
            CellUpdater.OnUpdate,
            Flipper.OnUpdate,
            PositionUpdater.OnUpdate,
            PriorityUpdater.OnUpdate,
            RotationUpdater.OnUpdate,
            ScalingUpdater.OnUpdate,
            TransparencyUpdater.OnUpdate,
            VisibilityUpdater.OnUpdate,
        };


        /// <summary>
        /// 
        /// </summary>
        [SerializeField]
        private int target_;
        
        /// <summary>
        /// 
        /// </summary>
        [SerializeField]
        private int[] intValues_;

        /// <summary>
        /// 
        /// </summary>
        [SerializeField]
        private float[] floatValues_;

        /// <summary>
        /// 
        /// </summary>
        [SerializeField]
        private bool[] boolValues_;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="intValue"></param>
        /// <param name="floatValue"></param>
        /// <param name="boolValue"></param>
        public AttributeBase( Target target, int[] intValue, float[] floatValue, bool[] boolValue )
        {
            target_ = (int) target;
            intValues_ = intValue;
            floatValues_ = floatValue;
            boolValues_ = boolValue;
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="part"></param>
        public void Do( SpritePart part )
        {
            Functions[target_]( part, this );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public int @int( int no )
        {
            return intValues_[no];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public float @float( int no )
        {
            return floatValues_[no];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public bool @bool( int no )
        {
            return boolValues_[no];
        }
    }
}
