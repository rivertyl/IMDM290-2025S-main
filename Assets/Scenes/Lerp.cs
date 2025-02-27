// UMD IMDM290 
// Instructor: Myungin Lee
    // [a <-----------> b]
    // Lerp : Linearly interpolates between two points. 
    // https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Vector3.Lerp.htmlusing System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class Lerp : MonoBehaviour
{
    GameObject[] spheres;
    static int numSphere = 500;
    float time = 0f;
    Vector3[] startPosition, endPosition;
    float lerpFraction; //lerp point between 0~1
    float t;
    public Transform centerPoint; 
    private float[] colorChangeTimers; // For color change

    void Start()
    {
        spheres = new GameObject[numSphere];
        startPosition = new Vector3[numSphere];
        endPosition = new Vector3[numSphere];
        colorChangeTimers = new float[numSphere]; 

        // Define target positions. Start = random, End = heart
        for (int i = 0; i < numSphere; i++)
        {   
            // Random start positions
            float r = 15f;
            startPosition[i] = new Vector3(r * Random.Range(-1f, 1f), r * Random.Range(-1f, 1f), r * Random.Range(-1f, 1f));
            //Heart shape end position
            t = i * 2 * Mathf.PI / numSphere;
            endPosition[i] = new Vector3(
                5f * Mathf.Sqrt(2f) * Mathf.Sin(t) * Mathf.Sin(t) * Mathf.Sin(t),
                5f * (-Mathf.Cos(t) * Mathf.Cos(t) * Mathf.Cos(t) - Mathf.Cos(t) * Mathf.Cos(t) + 2 * Mathf.Cos(t)) + 3f + Mathf.Sin(time * 2) * 2f,
                10f + Mathf.Sin(time));

            // Position
            spheres[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            spheres[i].transform.position = startPosition[i];

            // scale changes
            float scale = Random.Range(0.3f, 1f);
            spheres[i].transform.localScale = Vector3.one * scale;

        }
    }

    void Update()
    {
        // Measure Time
        time += Time.deltaTime;

        for (int i = 0; i < numSphere; i++)
        {
            colorChangeTimers[i] += Time.deltaTime; // Time.deltaTime = The interval in seconds from the last frame to the current one
            lerpFraction = Mathf.Sin(time) * 0.8f + 0.5f;
            spheres[i].transform.position = Vector3.Lerp(startPosition[i], endPosition[i], lerpFraction);

            // scaling spheres
            float scale = 0.2f + 0.5f * Mathf.Sin(time + i * 0.7f);
            spheres[i].transform.localScale = Vector3.one * scale;
            Renderer sphereRenderer = spheres[i].GetComponent<Renderer>();

            // color speed on sine wave
            float colorSin = Mathf.Sin(time + i * 0.8f);

            // sine wave changes colors
            Color color = Color.Lerp(Color.blue, new Color(0.9f, 0.7f, 0.8f), colorSin);
            sphereRenderer.material.color = color;
        }
    }
}