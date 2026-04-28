using UnityEngine;

namespace Enemy
{
    public interface IDamaged
    {
        /// <summary>
        /// 敵にダメージを与える、マイナスで減らし、プラスで回復
        /// </summary>
        /// <param name="value"></param>
        public void Damaged(float value);
    }
}
