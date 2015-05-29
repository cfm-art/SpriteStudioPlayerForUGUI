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
        /// 処理の指定
        /// 順番遵守。
        /// 順番が変わった場合はそれまでのprefabが正しく再生されなくなる。
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
            kVisibility,
            kUserNotify,
            kVertex,
            kUVS,
            kUVT,
            kUVU,
            kUVV,
            kSizeX,
            kSizeY,
        }

        /// <summary>
        /// 関数テーブル
        /// enum Targetと同じ順番で用意する。
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
            UserDataNotifier.OnUpdate,
            VertexUpdater.OnUpdate,
            TextureCoordUpdater.OnUpdateS,
            TextureCoordUpdater.OnUpdateT,
            TextureCoordUpdater.OnUpdateU,
            TextureCoordUpdater.OnUpdateV,
            SizeUpdater.OnUpdateX,
            SizeUpdater.OnUpdateY,
        };

        /// <summary>
        /// 処理の対象
        /// </summary>
        [SerializeField]
        private Target target_;
        
        /// <summary>
        /// 整数のパラメータ
        /// </summary>
        [SerializeField]
        private int[] intValues_;

        /// <summary>
        /// 小数のパラメータ
        /// </summary>
        [SerializeField]
        private float[] floatValues_;

        /// <summary>
        /// 真偽値のパラメータ
        /// </summary>
        [SerializeField]
        private bool[] boolValues_;

        /// <summary>
        /// 文字列のパラメータ
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
        /// 整数パラメータの取得
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public int @int( int no )
        {
            return intValues_[no];
        }

        /// <summary>
        /// 小数パラメータの取得
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public float @float( int no )
        {
            return floatValues_[no];
        }

        /// <summary>
        /// 真偽値パラメータの取得
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public bool @bool( int no )
        {
            return boolValues_[no];
        }

        /// <summary>
        /// 文字列パラメータの取得
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public string @string( int no )
        {
            return stringValues_[no];
        }
    }
}
