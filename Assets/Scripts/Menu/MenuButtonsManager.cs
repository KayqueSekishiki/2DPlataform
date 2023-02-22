using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuButtonsManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _buttons;

    [Header("Animation")]
    [SerializeField] private float _duration = .5f;
    [SerializeField] private float _delay = .1f;
    [SerializeField] private Ease ease = Ease.OutBack;

    private void OnEnable()
    {
        HideAllButtons();
        ShowButtons();
    }

    private void HideAllButtons()
    {
        foreach (var b in _buttons)
        {
            b.transform.localScale = Vector3.zero;
            b.SetActive(false);
        }
    }


    private void ShowButtons()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            var b = _buttons[i];
            b.SetActive(true);
            b.transform.DOScale(1, _duration).SetDelay(i * _delay).SetEase(ease);
        }
    }
}
