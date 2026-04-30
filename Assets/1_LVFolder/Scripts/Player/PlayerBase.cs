using UnityEngine;

namespace Player
{
    /// <summary>
    /// プレイヤーが共通で使うコンポーネントの取得
    /// PlayerCoreコンポーネントも取得している
    /// </summary>
    public class PlayerBase : MonoBehaviour
    {
        protected Rigidbody Rb;
        protected Animator Anim;
        protected PlayerCore Core;

        protected virtual void Awake()
        {
            Rb = GetComponent<Rigidbody>();
            Anim = GetComponent<Animator>();
            Core = GetComponent<PlayerCore>();
        }
    }
}
