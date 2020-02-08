using System;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace Units
{
    public class Billboard : MonoBehaviour
    {
        [SerializeField]
        private Camera camera;
        
        // Start is called before the first frame update
        void Start()
        {

            if (camera == null)
            {
                camera = Camera.main;
            }

            transform.LookAt(camera.transform.position, Vector3.up);
            //transform.forward = Camera.main.transform.forward;
        }

        private void Awake()
        {
            if (camera == null)
            {
                camera = Camera.main;
            }

            transform.LookAt(camera.transform.position, Vector3.up);
            //if (Camera.main != null) transform.LookAt(Camera.main.transform.position, Vector3.up);
        }


        // Update is called once per frame
        void Update()
        {
        }
    }
}