using DG.Tweening;
using UnityEngine;

namespace Fakejam.GameUtilities
{
    public class FadeAfterSeconds : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _targetAlpha;
        [SerializeField] private float _animationTime = 0.7f;
        [SerializeField] private float _timeToWait = 5f;

        private Tween _tween;

        private void OnEnable()
        {
            _tween = _canvasGroup.DOFade(_targetAlpha, _animationTime).SetDelay(_timeToWait);
        }

        private void OnDisable()
        {
            _tween.Kill(false);
        }
    }
}