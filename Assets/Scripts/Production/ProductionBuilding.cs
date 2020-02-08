using DG.Tweening;
using Fakejam.Units;
using UnityEngine;
using UnityEngine.AI;

namespace Fakejam.Production
{
    public class ProductionBuilding : MonoBehaviour
    {
        [SerializeField] private UnitDefinition _producingUnit;
        [SerializeField] private int _currentUnitCount;
        
        [Header("References")]
        [SerializeField] private ProductionClickable _clickable;

        [SerializeField] private SpriteRenderer _imageOfUnit;

        [SerializeField] private ParticleSystem _productionActive;
        [SerializeField] private CountdownTimer _countdownTimer;

        [Header("Animation")]
        [SerializeField] private Vector3 _squashScale = new Vector3(1f, 0.5f, 1f);
        [SerializeField] private float _animationTimeDown = 0.1f;
        [SerializeField] private float _animationTimeUp = 0.4f;
        
        private Tween _currentTween;
        private bool _isAlreadyProducing;
        private Vector3 _positionWhereUnitsWillWalk;

        public void SetProduction(UnitDefinition producingUnit, int initialCount, Transform transformWhereUnitsWillWalk)
        {
            _producingUnit = producingUnit;
            _currentUnitCount = initialCount;
            _positionWhereUnitsWillWalk = transformWhereUnitsWillWalk.position;

            for (int i = 0; i < initialCount; i++)
            {
                var instance = Instantiate(_producingUnit.PrefabOfProductionUnit);
                instance.transform.position = _positionWhereUnitsWillWalk;
            }

            if (_producingUnit.Icon != null)
            {
                _imageOfUnit.sprite = _producingUnit.Icon;
                _imageOfUnit.enabled = true;
            }
            else
            {
                _imageOfUnit.enabled = false;
            }
        }

        public (UnitDefinition, int) GetTotalProduction()
        {
            return (_producingUnit, _currentUnitCount);
        }

        private void OnEnable()
        {
            var emission = _productionActive.emission;
            emission.enabled = false;
            
            _clickable.OnDown.AddListener(OnBuildingDown);
            _clickable.OnUp.AddListener(OnProductionClicked);
            _countdownTimer.OnTrigger.AddListener(OnUnitProduced);
        }

        private void OnDisable()
        {
            if (_clickable != null)
            {
                _clickable.OnDown.RemoveListener(OnBuildingDown);
                _clickable.OnUp.RemoveListener(OnProductionClicked);
            }
            
            if (_countdownTimer != null)
            {
                _countdownTimer.OnTrigger.RemoveListener(OnUnitProduced);
            }
        }

        private void OnBuildingDown()
        {
            if (_isAlreadyProducing)
            {
                return;
            }
            _currentTween.Kill(false);
            _currentTween = transform.DOScale(_squashScale, _animationTimeDown).SetEase(Ease.Linear);
        }

        private void OnProductionClicked()
        {
            if (_isAlreadyProducing)
            {
                return;
            }
            _currentTween.Kill(false);
            _currentTween = transform.DOScale(Vector3.one, _animationTimeUp).SetEase(Ease.OutElastic);

            _isAlreadyProducing = true;
            var emission = _productionActive.emission;
            emission.enabled = true;
            _countdownTimer.SetTime(_producingUnit.ProductionTime);
        }

        private void OnUnitProduced()
        {
            var emission = _productionActive.emission;
            emission.enabled = false;
            _isAlreadyProducing = false;
            _currentUnitCount++;

            var instance = Instantiate(_producingUnit.PrefabOfProductionUnit);
            instance.transform.position = transform.position;
            var navMeshAgent = instance.GetComponent<NavMeshAgent>();
            if (navMeshAgent != null)
            {
                navMeshAgent.destination = _positionWhereUnitsWillWalk;
            }
        }
    }
}