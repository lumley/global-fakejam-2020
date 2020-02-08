using UnityEngine;

namespace Fakejam.GameUtilities
{
    public class ContinuousLocalRotation : MonoBehaviour
    {
        [SerializeField] private Vector3 _rotationSpeed = new Vector3(0f, 90f, 0f);

        private void Update()
        {
            var localRotationEulerAngles = transform.localRotation.eulerAngles;
            var rotationEulerAngles = localRotationEulerAngles + _rotationSpeed * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(rotationEulerAngles);
        }
    }
}