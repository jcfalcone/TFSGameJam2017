using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField] Light lightning;
    [SerializeField] [Range(0, 10)] float speed;

    float currentTime = 0.0f;
    [SerializeField] [Range(0, 10)] float enableLightningTime;
    [SerializeField] float gapLightning;
    float currentGap = 0.0f;
    int setup = 1;

    bool isLightning = false;
    float intensity = 0.2f;

	// Update is called once per frame
	void Update ()
    {
        if (isLightning) SetupLightning();

        if(setup == 4)
        {
            isLightning = false;
            setup = 1;
        }
        currentTime += Time.deltaTime;

        if(currentTime >= enableLightningTime)
        {
            currentTime = 0.0f;
            isLightning = true;
        }
	}

    void SetupLightning()
    {
        switch(setup)
        {
            case 1:
                ShowLight(0.6f, 0.2f);
                break;

            case 2:
                currentGap += Time.deltaTime;

                if (currentGap >= gapLightning)
                {
                    ShowLight(1.0f, 0.2f);
                }
                break;

            case 3:
                currentGap += Time.deltaTime;

                if (currentGap >= gapLightning)
                {
                    ShowLight(1.0f, 0.2f);
                }
                break;
        }
    }

    void ShowLight(float max, float min)
    {
        intensity += speed * Time.deltaTime;

        if (intensity >= max)
        {
            intensity = max;
            speed *= -1;
        }
        else if (intensity <= min)
        {
            intensity = min;
            speed *= -1;
            setup++;
            currentGap = 0.0f;
        }

        lightning.intensity = intensity;
    }
}
