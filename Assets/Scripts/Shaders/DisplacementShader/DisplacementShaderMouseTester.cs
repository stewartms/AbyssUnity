using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplacementShaderMouseTester : MonoBehaviour
{
    public Material dispShader;
    public float strength = .05f;

    int width = Screen.width;
    int height = Screen.height;

    void Update() {

        Vector2 mousePosition = Input.mousePosition;



        dispShader.SetFloat("_DispX", strength * (mousePosition.x - width/2)/width);
        dispShader.SetFloat("_DispY", strength * (mousePosition.y - height/2)/height);

    }


}
