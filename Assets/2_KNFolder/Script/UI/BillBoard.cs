using UnityEngine;

namespace Enemy
{
    public class BillBoard : MonoBehaviour
    {
        private void Update()
        {
            transform.LookAt(transform.position + Camera.main.transform.forward);
        }
    }
}
