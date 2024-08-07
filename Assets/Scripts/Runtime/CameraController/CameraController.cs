using DG.Tweening;
using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Camera _camera;

    public float orthographicSize
    {
        get
        {
            return _camera.orthographicSize;
        }
    }

    public float aspect
    {
        get
        {
            return _camera.aspect;
        }
    }

    public void MoveTween(float target, float delay, Action complete)
    {
        _camera.transform.DOMoveY(target, delay)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                complete();
            });
    }
}
