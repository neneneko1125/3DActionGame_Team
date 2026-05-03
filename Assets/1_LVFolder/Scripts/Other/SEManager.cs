using UnityEngine;

public class SEManager : MonoBehaviour
{
    [SerializeField] private AudioSource _attack1_2;
    [SerializeField] private AudioSource _attack3;
    [SerializeField] private AudioSource _attackHit;
    [SerializeField] private AudioSource _attackHit2;
    [SerializeField] private AudioSource _damage;

    public static SEManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySE_Attack1_2() => _attack1_2.Play();
    public void PlaySE_Attack3() => _attack3.Play();
    public void PlaySE_AttackHit() => _attackHit.Play();
    public void PlaySE_AttackHit2() => _attackHit2.Play();
    public void PlaySE_Damage() => _damage.Play();
}
