using Player;
using UnityEngine;

public class EXP : MonoBehaviour
{
    public int Point;

    private Rigidbody rb;

    private float time;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 15f);
    }
    private void FixedUpdate()
    {
        if (!PlayerCore.Instance)
        {
            return;
        }
        Vector3 vec = PlayerCore.Instance.gameObject.transform.position - transform.position;
        if (time >= 1)
        {
            if (vec.magnitude <= 3)
            {
                rb.linearVelocity = vec.normalized * time * time;
                time += Time.fixedDeltaTime;
            }
        }
        else
        {
            time += Time.fixedDeltaTime;
        }
    }
}
