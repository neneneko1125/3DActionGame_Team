using Enemy;
using UnityEngine;
using TMPro;

public class ShieldText : MonoBehaviour
{
    [SerializeField] private EnemyBase enemyBase;

    [SerializeField] private TextMeshPro text;
    private void Update()
    {
        if (enemyBase.Shield > 0)
        {
            gameObject.SetActive(true);
            text.text = enemyBase.Shield.ToString();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
