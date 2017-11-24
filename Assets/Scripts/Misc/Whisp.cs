using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whisp : MonoBehaviour
{
    [SerializeField] [Range(0, 10)] float speed;

    int totalPoints; 
    List<Vector3> points;
    [SerializeField] int index;

	// Use this for initialization
	void Awake ()
    {
        totalPoints = 4;
        points = new List<Vector3>();

        for (int i = 0; i < totalPoints; i++)
        {
            points.Add(new Vector3(transform.position.x,
                                   transform.position.y + Random.Range(-1.0f, 1.0f),
                                   transform.position.z + Random.Range(-1.0f, 1.0f)));
        }
        index = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector3.MoveTowards(transform.position, points[index], speed * Time.deltaTime);

        if(transform.position == points[index])
        {
            index++;
            if (index == points.Count)
                index = 0;
        }
	}
}
