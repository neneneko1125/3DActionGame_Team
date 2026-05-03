using Enemy;
using System.Collections;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] private float damage;
    private Collider _col;

    private void Start()
    {
        _col = GetComponent<Collider>();
        _col.enabled = false;
    }
    public void Attack(float delay)
    {
        StartCoroutine(AttackCoroutine(delay));
    }
    private IEnumerator AttackCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        _col.enabled = true;
        yield return null;
        yield return null;
        yield return null;
        _col.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<IDamaged>(out var target))
            {
                target.ChangeHP(-damage);
            }
        }
    }
}
