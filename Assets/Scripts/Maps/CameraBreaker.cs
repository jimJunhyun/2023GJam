using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBreaker : MonoBehaviour
{
    private void Update()
    {
        transform.position = CameraManager.Instance.pCam.transform.position;
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<MeshRenderer>(out MeshRenderer _mesh))
        {
            _mesh.enabled = false;
        }
        
        Debug.LogWarning(other);
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<MeshRenderer>(out MeshRenderer _mesh))
        {
            _mesh.enabled = true;
        }
    }
}
