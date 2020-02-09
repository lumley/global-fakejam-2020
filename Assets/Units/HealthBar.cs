using UnityEngine;
using UnityEngine.UI;

namespace Units
{
    public class HealthBar : MonoBehaviour
    {
        private Image _healthImage;

        private bool _initialized=false;

        // Start is called before the first frame update
        void Start()
        {
            _healthImage = transform.GetComponent<Image>();
            _initialized = true;
        }

        public void SetHealth(float health)
        {
            if (_initialized == false)
            {
                return;
            }
            if (health <= 0)
            {
                _healthImage.fillAmount = 0;
            }
            else if (health >= 1)
            {
                _healthImage.fillAmount = 1;
            }
            else
            {
                _healthImage.fillAmount = health;
            }
        }

    }
}