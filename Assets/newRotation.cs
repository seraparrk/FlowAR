using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newRotation : MonoBehaviour
{
    //초당 degreePerSecond도씩 bodyObject회전
    public float degreePerSecond;

    private void Update()
    {
        //축을 중심으로 bodyObject 회전
        transform.Rotate(Vector3.up * Time.deltaTime * degreePerSecond);
    }
}

