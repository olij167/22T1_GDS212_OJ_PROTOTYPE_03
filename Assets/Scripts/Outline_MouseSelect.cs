using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 // not written by me (if you couldn't tell)
 //Author Youtube: PluMaZero
public class Outline_MouseSelect : MonoBehaviour
{
    [SerializeField] private Material outlineMaterial;
    [SerializeField] private float outlineScale = -1.03f;
    public Color outlineColor = Color.yellow;
    //[Range(0.0f, 2.0f)] [SerializeField] private float RotateSpeed = 0.2f;
    //[Range(-1.0f, 1.0f)] [SerializeField] private float RotateDirection = 1.0f;
    [HideInInspector] public Renderer outlineRenderer;

    void Start()
    {
        outlineRenderer = CreateOutline(outlineMaterial, outlineScale, outlineColor);
    }

    Renderer CreateOutline(Material m, float s, Color c)
    {
        // 자기 자신 객체를 복제해서 Child로 하고 Outline을 그린다.
        GameObject outlineObject = Instantiate(this.gameObject, transform.position, transform.rotation, transform);

        outlineObject.name = this.gameObject.name + "_Outline";

        Renderer rend = outlineObject.GetComponent<Renderer>();
        rend.material = m;
        rend.material.SetColor("_OutlineColor", c);
        rend.material.SetFloat("_OutlineScale", s);
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        // 복제된 Outlien Child는 또다시 반복하지 않도록 객체의 Component를 비활성화 한다.
        outlineObject.GetComponent<Outline_MouseSelect>().enabled = false;
        outlineObject.GetComponent<Collider>().enabled = false;
        rend.enabled = false;
        return rend;
    }

//    private void OnMouseEnter()
//    {
//        outlineRenderer.enabled = true;
//    }
//    private void OnMouseOver()
//    {
//        if (RotateDirection > 0)
//            transform.Rotate(Vector3.up, RotateSpeed, Space.World);
//        else
//            transform.Rotate(Vector3.down, RotateSpeed, Space.World);
//    }

//    private void OnMouseExit()
//    {
//        outlineRenderer.enabled = false;
//    }
}