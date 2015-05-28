using UnityEngine;
using System.Collections;

namespace a.spritestudio.attribute
{
    /// <summary>
    /// 不透明度の変更
    /// </summary>
    public class TransparencyUpdater
        : AttributeBase
    {
        /// <summary>
        /// 値
        /// </summary>
        [SerializeField]
        private float value_;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static TransparencyUpdater Create( float value )
        {
            var self = ScriptableObject.CreateInstance<TransparencyUpdater>();
            self.value_ = value;
            return self;
        }

        /// <summary>
        /// 処理
        /// </summary>
        /// <param name="part"></param>
        protected override void OnUpdate( SpritePart part )
        {
            part.Renderer.canvasRenderer.SetAlpha( value_ );
        }
    }
}
