using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageText : MonoBehaviour
{
    private TextMeshPro _text;

    private Vector3 _firstPos;
    private float _alpha = 1f;
    private void Start()
    {
        _text = GetComponent<TextMeshPro>();
        _firstPos = transform.position;
    }
    void FixedUpdate()
    {
        _alpha -= Time.fixedDeltaTime * 0.5f;
        _text.color = new(1, 1, 1, _alpha);
        if (_alpha <= 0)
        {
            Destroy(gameObject);
        }

        Vector3 nextPos = _firstPos;
        nextPos.y += (1 - _alpha) * 0.5f;
        transform.position = nextPos;
    }
}
