using DG.Tweening;
using Fakejam.GameUtilities;
using Fakejam.Input;
using Units;
using UnityEngine;

namespace Fakejam.Units
{
    public class Shot : MonoBehaviour
    {
        [SerializeField] private float _timeToReachDistance = 0.2f; 
        
        private Shot _prefab;
        private int _damage;
        private UnitController _target;
        private Tween _tween;

        public void SetTarget(Transform origin, UnitController target, int damage, Shot prefab)
        {
            _prefab = prefab;
            _damage = damage;
            _target = target;

            transform.position = origin.position;
            transform.LookAt(target.transform);

            _tween = transform.DOMove(target.transform.position, _timeToReachDistance).OnComplete(OnShotHit);
        }

        private void OnShotHit()
        {
            _tween.Kill();
            if (_target != null)
            {
                _target.TakeDamage(_damage);
            }

            var poolingManager = Toolbox.Get<PoolingManager>();
            poolingManager.Recycle(_prefab, this);
        }

        private void OnDestroy()
        {
            _tween.Kill();
        }
    }
}