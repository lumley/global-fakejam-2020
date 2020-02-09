using UnityEngine;

namespace Fakejam.Production
{
    public class SpriteWidthByTime : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private CountdownTimer _countdownTimer;
        private float _maxLeftTime;
        private float _currentLeftTime;

        private void OnEnable()
        {
            _countdownTimer.OnTimeSet.AddListener(SetScaleDown);
        }

        private void OnDisable()
        {
            if (_countdownTimer != null)
            {
                _countdownTimer.OnTimeSet.RemoveListener(SetScaleDown);
            }
        }

        public void SetScaleDown(float timeLeft)
        {
            _currentLeftTime = timeLeft;
            _maxLeftTime = _currentLeftTime;
            _spriteRenderer.size = new Vector2(1f, _spriteRenderer.size.y);
        }

        private void Update()
        {
            if (_currentLeftTime <= 0)
            {
                return;
            }

            var deltaTime = Time.deltaTime;
            _currentLeftTime -= deltaTime;

            var sizePercentage = SizePercentage;
            _spriteRenderer.size = new Vector2(sizePercentage, _spriteRenderer.size.y);
        }

        private float MaxLeftTime => _maxLeftTime < 0.1f ? 0.1f : _maxLeftTime;

        private float SizePercentage => Mathf.Max(0.0001f, _currentLeftTime / MaxLeftTime);
    }
}