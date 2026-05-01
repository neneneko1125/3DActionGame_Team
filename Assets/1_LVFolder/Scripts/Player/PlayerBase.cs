using UnityEngine;

namespace Player
{
    /// <summary>
    /// プレイヤーが共通で使うコンポーネントの取得
    /// PlayerCoreコンポーネントも取得している
    /// </summary>
    public class PlayerBase : MonoBehaviour
    {
        protected static readonly int AnimIsWalking = Animator.StringToHash("IsWalking");
        protected static readonly int AnimAttackTrigger = Animator.StringToHash("Attack");
        protected static readonly int AnimAttackSpeed = Animator.StringToHash("AttackSpeed");
        protected static readonly int AnimStunTrigger = Animator.StringToHash("Stun");
        protected static readonly int AnimStunRecoverySpeed = Animator.StringToHash("StunRecoverySpeed");
        protected static readonly int AnimDead = Animator.StringToHash("Dead");

        protected const string AttackStateName = "Player_Attack";
        protected const string StunStateName = "Player_Stun";

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
