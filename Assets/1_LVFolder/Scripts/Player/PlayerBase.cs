using UnityEngine;

namespace Player
{
    /// <summary>
    /// プレイヤーが共通で使うコンポーネントの取得
    /// PlayerCoreコンポーネントも取得している
    /// </summary>
    public class PlayerBase : MonoBehaviour
    {
        protected static readonly int IsWalkingHash = Animator.StringToHash("IsWalking");

        protected static readonly int AttackTriggerHash = Animator.StringToHash("Attack");
        protected static readonly int AttackSpeedHash = Animator.StringToHash("AttackSpeed");
        protected static readonly int Attack2TriggerHash = Animator.StringToHash("Attack2");
        protected static readonly int Attack2SpeedHash = Animator.StringToHash("Attack2Speed");
        protected static readonly int Attack3TriggerHash = Animator.StringToHash("Attack3");
        protected static readonly int Attack3SpeedHash = Animator.StringToHash("Attack3Speed");
        protected static readonly int Attack4TriggerHash = Animator.StringToHash("Attack4");
        protected static readonly int Attack4SpeedHash = Animator.StringToHash("Attack4Speed");

        protected static readonly int StunTriggerHash = Animator.StringToHash("Stun");
        protected static readonly int StunRecoverySpeedHash = Animator.StringToHash("StunRecoverySpeed");
        protected static readonly int DeadHash = Animator.StringToHash("Dead");

        protected const string AttackStateName = "Player_Attack";
        protected const string Attack2StateName = "Player_Attack2";
        protected const string Attack3StateName = "Player_Attack3";
        protected const string Attack4StateName = "Player_Attack4";
        protected const string StunStateName = "Player_Stun";

        protected Rigidbody Rb;
        protected Animator Anim;
        protected PlayerInput PlayerInput;
        protected PlayerAttackHandler PlayerAttackHandler;
        protected PlayerCore Core;

        protected virtual void Awake()
        {
            Rb = GetComponent<Rigidbody>();
            Anim = GetComponent<Animator>();
            PlayerInput = GetComponent<PlayerInput>();
            PlayerAttackHandler = GetComponent<PlayerAttackHandler>();
            Core = GetComponent<PlayerCore>();
        }
    }
}
