using UnityEngine;

namespace Enemy
{
    public class Slime : EnemyBase, IDamaged
    {
        //[Header("ƒIƒŠƒWƒiƒ‹İ’è")]
        //[SerializeField] private bool b;

        public void Damaged(float value)
        {
            Hp += value;

            if (Hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
