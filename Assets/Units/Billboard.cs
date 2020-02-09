using System;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace Units
{
    public class Billboard : MonoBehaviour
    {
        [SerializeField]
        private Camera _camera;
        
        // Start is called before the first frame update
        private void Start()
        {
            if (_camera == null)
            {
                _camera = Camera.main;
            }
            
            if (_camera == null)
            {
                return;
            }
            transform.LookAt(_camera.transform.position, Vector3.up);
        }
    }
}