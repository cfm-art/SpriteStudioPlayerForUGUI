using UnityEngine;
using a.spritestudio;

namespace a.smaple
{
    /// <summary>
    /// モーション変更のサンプル
    /// SpriteRoot#ChangeMotionを利用してモーションを変更する
    /// </summary>
    public class ChangeMotion
        : MonoBehaviour
    {
        /// <summary>
        /// シーンのヒエラルキー内に追加されているSSのデータ
        /// </summary>
        [SerializeField]
        private SpriteRoot skelton_;

        /// <summary>
        /// モーション1
        /// </summary>
        [SerializeField]
        private KeyFrameResource motion1_;

        /// <summary>
        /// モーション1
        /// </summary>
        [SerializeField]
        private KeyFrameResource motion2_;

        /// <summary>
        /// インスペクターでちゃんと設定されているかチェック
        /// </summary>
        void Start()
        {
            if ( skelton_ == null ) {
                Debug.LogError( "SSのデータ(skelton)が設定されていない" );
            }
            if ( motion1_ == null ) {
                Debug.LogError( "モーションデータ(motion1)が設定されていない" );
            }
            if ( motion2_ == null ) {
                Debug.LogError( "モーションデータ(motion2)が設定されていない" );
            }
        }

        /// <summary>
        /// GUI
        /// </summary>
        void OnGUI()
        {
            // ボタン押下でモーション変更
            if ( GUILayout.Button( "モーション1", GUILayout.Width( 100 ), GUILayout.Height( 100 ) ) ) {
                skelton_.ChangeMotion( motion1_ );
            }

            if ( GUILayout.Button( "モーション2", GUILayout.Width( 100 ), GUILayout.Height( 100 ) ) ) {
                skelton_.ChangeMotion( motion2_ );
            }
        }
    }

}