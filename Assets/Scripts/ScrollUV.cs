using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollUV : MonoBehaviour
{
    public float parallax = 2f;
    private MeshRenderer mr;
    private Material mat;
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        mat = mr.material;
        mat.mainTexture.wrapMode = TextureWrapMode.Repeat;
    }
    void Update()
    {
        Vector2 offset = mat.mainTextureOffset;

        offset.x = transform.position.x / transform.localScale.x / parallax;
        offset.y = transform.position.x / transform.localScale.y / parallax;

        mat.mainTextureOffset = offset;
    }
}
