using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ProductionClickable : MonoBehaviour
{
    public UnityEvent OnDown;
    public UnityEvent OnUp;

    private void OnMouseDown()
    {
        OnDown?.Invoke();
    }

    private void OnMouseUp()
    {
        OnUp?.Invoke();
    }
}
