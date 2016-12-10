using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{

    //public float orthographicSize = 18;
   // public float aspect = 1.33333f;
    void Start()
    {
        float aspect = 800f / 1280f;
        float orthographicSize = Camera.main.orthographicSize;

        Camera.main.projectionMatrix = Matrix4x4.Ortho(
                -orthographicSize * aspect, Camera.main.orthographicSize * aspect,
                -orthographicSize, orthographicSize,
                Camera.main.nearClipPlane, Camera.main.farClipPlane);
    }
}
