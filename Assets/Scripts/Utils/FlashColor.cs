using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashColor : MonoBehaviour
{
    public List<SpriteRenderer> spriteRenderers;
    public Color color = Color.red;
    public float duration = .1f;

    private Tween _currentTween;

    private void OnValidate()
    {
        spriteRenderers = new List<SpriteRenderer>();
        foreach (var child in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderers.Add(child);
        }
    }

    public void Flash()
    {
        if (_currentTween != null)
        {
            _currentTween.Kill();
            spriteRenderers.ForEach(i => i.color = Color.white);
        }

        foreach (var s in spriteRenderers)
        {
            spriteRenderers.ForEach(i => i.color = Color.white);
            _currentTween = s.DOColor(color, duration).SetLoops(6, LoopType.Yoyo);
        }
    }

}
