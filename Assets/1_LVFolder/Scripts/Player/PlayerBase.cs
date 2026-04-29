using UnityEngine;

namespace Player
{
    public class PlayerBase : MonoBehaviour
    {
        protected Rigidbody2D Rb;
        protected Animator Anim;
        protected PlayerCore Core;

        protected virtual void Awake()
        {
            Rb = GetComponent<Rigidbody2D>();
            Anim = GetComponent<Animator>();
            Core = GetComponent<PlayerCore>();
        }
    }
}
