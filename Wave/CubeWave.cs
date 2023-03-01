using UnityEngine;

public class CubeWave : MonoBehaviour
{
    public int numberOfCubes = 100; 
    public float amplitude = 2f; 
    public float frequency = 1f; 
    public float speed = 2f;
    public float spacing = 1f; 
    public int cubesPerSide = 10; 
    public Gradient colorGradient; 
    public bool colorByHeight = true; 

    private GameObject[] cubes; 
    private float[] phases; 
    private float startTime; 

    private void Start()
    {
        cubes = new GameObject[numberOfCubes];
        phases = new float[numberOfCubes];


        int count = 0;
        for (int x = 0; x < cubesPerSide; x++)
        {
            for (int y = 0; y < cubesPerSide; y++)
            {
                cubes[count] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cubes[count].transform.position = new Vector3(x * spacing, 0f, y * spacing);
                phases[count] = 5;
                count++;
            }
        }

        startTime = Time.time;
    }

    private void Update()
    {
        MoveCubes();
        ColorCubes();
    }

    private void MoveCubes()
    {
        for (int i = 0; i < numberOfCubes; i++)
        {
            float x = cubes[i].transform.position.x;
            float y = amplitude * Mathf.Sin((Time.time - startTime) * speed + phases[i] + x * frequency);
            cubes[i].transform.position = new Vector3(x, y, cubes[i].transform.position.z);
        }
    }

    private void ColorCubes()
    {
        if (colorByHeight)
        {
            for (int i = 0; i < numberOfCubes; i++)
            {
                float heightRatio = Mathf.InverseLerp(-amplitude, amplitude, cubes[i].transform.position.y);
                cubes[i].GetComponent<Renderer>().material.color = colorGradient.Evaluate(heightRatio);
            }
        }
        else
        {
            float timeRatio = Mathf.InverseLerp(startTime, startTime + 10f, Time.time);
            Color color = colorGradient.Evaluate(timeRatio);
            foreach (GameObject cube in cubes)
            {
                cube.GetComponent<Renderer>().material.color = color;
            }
        }
    }
}
