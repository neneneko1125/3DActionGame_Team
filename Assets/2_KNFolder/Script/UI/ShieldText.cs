using Enemy;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShieldText : MonoBehaviour
{
    [SerializeField] private EnemyBase enemyBase;

    [SerializeField] private Image image;
    [SerializeField] private TextMeshPro text;
    private void Update()
    {
        if (enemyBase.Shield > 0)
        {
            image.enabled = true;
            text.enabled = true;
            text.text = enemyBase.Shield.ToString();
        }
        else
        {
            image.enabled = false;
            text.enabled = false;
        }
    }
}
