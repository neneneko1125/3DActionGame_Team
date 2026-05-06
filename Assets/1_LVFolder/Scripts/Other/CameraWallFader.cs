using Player;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class CameraWallFader : MonoBehaviour
{
    [SerializeField] private LayerMask _wallLayer;
    [SerializeField] private float _fadeAlpha = 0.3f;

    private Dictionary<Renderer, Material[]> _originalMaterials = new Dictionary<Renderer, Material[]>();
    private List<Renderer> _currentlyHitRenderers = new List<Renderer>();

    private void Update()
    {
        // CameraManagerのインスタンスから、現在のターゲット座標を取得
        Vector3 targetPos = PlayerCore.Instance.transform.position;
        Vector3 direction = targetPos - transform.position;
        float distance = direction.magnitude;

        Debug.DrawRay(transform.position, direction, Color.red);

        // Rayを飛ばして壁を検知
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction.normalized, distance, _wallLayer);

        _currentlyHitRenderers.Clear();
        foreach (var hit in hits)
        {
            if (hit.collider.TryGetComponent<Renderer>(out var renderer))
            {
                _currentlyHitRenderers.Add(renderer);
                FadeOut(renderer);
            }
        }

        // 当たらなくなったものを元に戻す
        List<Renderer> toRemove = new List<Renderer>();
        foreach (var kvp in _originalMaterials)
        {
            Renderer cachedRenderer = kvp.Key;
            if (!_currentlyHitRenderers.Contains(cachedRenderer))
            {
                FadeIn(cachedRenderer, kvp.Value);
                toRemove.Add(cachedRenderer);
            }
        }

        // リストから削除
        foreach (var r in toRemove)
        {
            Debug.Log("元に戻した");
            _originalMaterials.Remove(r);
        }
    }

    private void FadeOut(Renderer renderer)
    {
        if (_originalMaterials.ContainsKey(renderer)) return;

        // ここがポイント：書き換える前の状態を「配列のコピー」として保存
        Material[] copy = new Material[renderer.sharedMaterials.Length];
        for (int i = 0; i < renderer.sharedMaterials.Length; i++)
        {
            // sharedMaterial（共有マテリアル）を元に、新しいインスタンスを作る
            copy[i] = new Material(renderer.sharedMaterials[i]);
        }
        _originalMaterials.Add(renderer, copy);

        // 透明化処理：renderer.materials（インスタンス化されたもの）を直接いじる
        foreach (var mat in renderer.materials)
        {
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.renderQueue = 3000;

            Color color = mat.color;
            color.a = _fadeAlpha;
            mat.color = color;
        }
    }

    private void FadeIn(Renderer renderer, Material[] originalMats)
    {
        if (renderer == null)
        {
            return;
        }

        // 保存しておいた元のマテリアル配列を戻す
        renderer.materials = originalMats;
    }
}