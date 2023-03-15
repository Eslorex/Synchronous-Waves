using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDropSimulation : MonoBehaviour
{
    public GameObject cubePrefab;
    public int cubeCount = 10;
    public float cubeGap = 1f;
    public float amplitude = 1f;
    public float frequency = 1f;
    public Gradient colorGradient;

    private List<GameObject> cubes;
    private float timer;

    void Start()
    {
        cubes = new List<GameObject>();

        for (int x = 0; x < cubeCount; x++)
        {
            for (int z = 0; z < cubeCount; z++)
            {
                GameObject cube = Instantiate(cubePrefab, new Vector3(x * cubeGap, 0, z * cubeGap), Quaternion.identity);
                cube.transform.SetParent(transform);
                cubes.Add(cube);
            }
        }
    }

    void Update()
    {
        timer += Time.deltaTime * frequency;

        for (int i = 0; i < cubes.Count; i++)
        {
            Vector3 cubePos = cubes[i].transform.position;
            float distance = Vector3.Distance(new Vector3(cubeCount / 2f * cubeGap, 0, cubeCount / 2f * cubeGap), new Vector3(cubePos.x, 0, cubePos.z));
            float yOffset = Mathf.Sin(timer - distance) * amplitude;
            cubes[i].transform.position = new Vector3(cubePos.x, yOffset, cubePos.z);

            float colorFactor = (yOffset + amplitude) / (amplitude * 2);
            Color cubeColor = colorGradient.Evaluate(colorFactor);
            cubes[i].GetComponent<Renderer>().material.color = cubeColor;
        }
    }
}
