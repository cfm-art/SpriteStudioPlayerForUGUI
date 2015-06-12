using UnityEngine;
using a.spritestudio;

namespace a.smaple
{
    /// <summary>
    /// 動的にモーションを作る
    /// </summary>
    public class Dynamic
        : MonoBehaviour
    {
        /// <summary>
        /// 召喚する親ゲームオブジェクト
        /// </summary>
        [SerializeField]
        private GameObject parent_;

        /// <summary>
        /// モーション
        /// </summary>
        [SerializeField]
        private GameObject prefab_;

        /// <summary>
        /// インスペクターでちゃんと設定されているかチェック
        /// </summary>
        void Start()
        {
            if ( parent_ == null ) {
                Debug.LogError( "プレハブをクローンする先になるゲームオブジェクト(parent)が設定されていない" );
            }
            if ( prefab_ == null ) {
                Debug.LogError( "モーションデータのプレハブ(prefab)が設定されていない" );
            }
        }


        /// <summary>
        /// ボタン表示
        /// </summary>
        void OnGUI()
        {
            // ボタン押下でモーション再生
            if ( GUILayout.Button( "召喚", GUILayout.Width( 100 ), GUILayout.Height( 100 ) ) ) {
                // prefabをクローン
                GameObject o = (GameObject) Object.Instantiate( prefab_ );
                var root = o.GetComponent<SpriteRoot>();

                // 親を指定
                root.transform.SetParent( parent_.transform );

                // 位置と大きさを適当に指定
                root.SetPosition( Random.Range( -320, 320 ), Random.Range( -240, 240 - 100 ) );
                root.SetScale( 0.3f, 0.3f );

                // 完了時にゲームオブジェクトを削除
                root.OnComplete += ( sprite ) => sprite.DestroySelf();
            }
        }
    }
}
