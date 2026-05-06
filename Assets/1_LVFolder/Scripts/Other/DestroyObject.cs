using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 5.0f;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }
}
