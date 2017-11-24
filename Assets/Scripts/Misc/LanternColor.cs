using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternColor : MonoBehaviour 
{
    [SerializeField]
    Renderer objRender;

    [SerializeField]
    [Range(0f, 10f)]
    float colorSpeed;

    [SerializeField]
    [ColorUsageAttribute(true,true,0f,8f,0.125f,3f)]
    List<Color> colors;

    int currColor = 1;

    Material currMaterial;

	// Use this for initialization
	void Awake () 
    {
        this.currMaterial = this.objRender.material;

        this.colors.Insert(0, this.currMaterial.GetColor("_EmissionColor"));
	}
	
	// Update is called once per frame
	void Update () 
    {
        Color colorMat = this.currMaterial.GetColor("_EmissionColor");

        this.currMaterial.SetColor("_EmissionColor", Color.Lerp(colorMat, this.colors[currColor], Time.deltaTime * this.colorSpeed));

        if (colorMat == this.colors[currColor])
        {
            this.currColor++;

            if (this.currColor >= this.colors.Count)
            {
                this.currColor = 0;
            }
        }
	}
}
