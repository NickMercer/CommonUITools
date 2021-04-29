using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoTweenController : MonoBehaviour
{
    [SerializeField]
    private Vector3 _targetLocation = Vector3.zero;

    [SerializeField, Range(1.0f, 10.0f)]
    private float _moveDuration = 1.0f;

    [SerializeField]
    private Ease _moveEase = Ease.Linear;

    [SerializeField]
    private Color _targetColor;

    [SerializeField, Range(1.0f, 10.0f)]
    private float _colorChangeDuration = 1.0f;

    [SerializeField]
    private float _targetScale = 3.0f;

    [SerializeField, Range(1.0f, 10.0f)]
    private float _scaleDuration = 1.0f;

    [SerializeField]
    private DoTweenType _doTweenType = DoTweenType.MovementOneWay;

    private enum DoTweenType
    {
        MovementOneWay,
        MovementTwoWay,
        MovementTwoWayWithSequence,
        MovementOneWayColorChange,
        MovementOneWayColorChangeAndScale
    }

    private void Start()
    {
        if (_targetLocation == Vector3.zero)
        {
            _targetLocation = transform.position;
        }

        switch (_doTweenType)
        {
            case DoTweenType.MovementOneWay:
                transform.DOMove(_targetLocation, _moveDuration).SetEase(_moveEase);
                break;
            
            case DoTweenType.MovementTwoWay:
                StartCoroutine(MoveWithBothWays());
                break;

            case DoTweenType.MovementTwoWayWithSequence:
                transform.DOMove(_targetLocation, _moveDuration).SetEase(_moveEase).SetLoops(-1, LoopType.Yoyo);
                break;

            case DoTweenType.MovementOneWayColorChange:
                DOTween.Sequence()
                    .Append(transform.DOMove(_targetLocation, _moveDuration).SetEase(_moveEase))
                    .Append(transform.GetComponent<Renderer>().material.DOColor(_targetColor, _colorChangeDuration));
                break;

            case DoTweenType.MovementOneWayColorChangeAndScale:
                DOTween.Sequence()
                    .Append(transform.DOMove(_targetLocation, _moveDuration).SetEase(_moveEase))
                    .Append(transform.GetComponent<Renderer>().material.DOColor(_targetColor, _colorChangeDuration))
                    .Append(transform.DOScale(_targetScale, _scaleDuration)).SetEase(_moveEase);
                break;

            default:
                break;
        }

    }

    private IEnumerator MoveWithBothWays()
    {
        var originalLocation = transform.position;

        transform.DOMove(_targetLocation, _moveDuration).SetEase(_moveEase);
        yield return new WaitForSeconds(_moveDuration);
        transform.DOMove(originalLocation, _moveDuration).SetEase(_moveEase);
    }
}


