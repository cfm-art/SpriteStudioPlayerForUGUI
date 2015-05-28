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
            kFliperH,
            kFliperV,
            kPositionX,
            kPositionY,
            kPositionZ,
            kPriority,
            kRotationX,
            kRotationY,
            kRotationZ,
            kScalingX,
            kScalingY,
            kTransparency,
            kVisibility
        }

        /// <summary>
        /// 関数テーブル
        /// </summary>
        private static System.Action<SpritePart, AttributeBase>[] Functions = new System.Action<SpritePart, AttributeBase>[] {
            CellUpdater.OnUpdate,
            Flipper.OnUpdateH,
            Flipper.OnUpdateV,
            PositionUpdater.OnUpdateX,
            PositionUpdater.OnUpdateY,
            PositionUpdater.OnUpdateZ,
            PriorityUpdater.OnUpdate,
            RotationUpdater.OnUpdateX,
            RotationUpdater.OnUpdateY,
            RotationUpdater.OnUpdateZ,
            ScalingUpdater.OnUpdateX,
            ScalingUpdater.OnUpdateY,
            TransparencyUpdater.OnUpdate,
            VisibilityUpdater.OnUpdate,
        };

        /// <summary>
        /// 
        /// </summary>
        [SerializeField]
        private Target target_;
        
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
        [SerializeField]
        private string[] stringValues_;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="intValue"></param>
        /// <param name="floatValue"></param>
        /// <param name="boolValue"></param>
        /// <param name="stringValue"></param>
        public AttributeBase( Target target, int[] intValue, float[] floatValue, bool[] boolValue, string[] stringValue = null )
        {
            target_ = target;
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
            Functions[(int) target_]( part, this );
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
