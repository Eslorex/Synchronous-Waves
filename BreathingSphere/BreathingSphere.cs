using UnityEngine;

public class BreathingSphere : MonoBehaviour
{
    public int rows = 8; 
    public int cols = 16; 
    public float cubeSize = 0.5f; 
    public float waveSpeed = 1.0f; 
    public float waveAmplitude = 0.1f;
    public Color inhaleColor = Color.blue; 
    public Color exhaleColor = Color.red; 

    private Vector3[,] cubePositions;
    private Vector3 center;
    private float amplitude;
    private GameObject[,] cubes;

    void Start()
    {
        cubePositions = new Vector3[rows, cols];
        cubes = new GameObject[rows, cols];
        center = transform.position;
        amplitude = cubeSize * (rows - 1) / 2;
        for (int i = 0; i < rows; i++)
        {
            float y = (float)i * cubeSize - amplitude;
            float radius = Mathf.Sqrt(amplitude * amplitude - y * y);
            for (int j = 0; j < cols; j++)
            {
                float angle = (float)j / cols * 360.0f;
                float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
                float z = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
                cubePositions[i, j] = center + new Vector3(x, y, z);
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.name = "Cube " + i + " " + j;
                cube.transform.position = cubePositions[i, j];
                cube.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
                cube.transform.parent = transform;
                cubes[i, j] = cube;
            }
        }
    }

    void Update()
    {
        float waveT = Time.time * waveSpeed;
        for (int i = 0; i < rows; i++)
        {
            float y = (float)i * cubeSize - amplitude;
            float radius = Mathf.Sqrt(amplitude * amplitude - y * y);
            for (int j = 0; j < cols; j++)
            {
                GameObject cube = cubes[i, j];
                float angle = (float)j / cols * 360.0f;
                float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
                float z = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
                Vector3 pos = center + new Vector3(x, y, z);
                float waveOffset = Mathf.Cos(waveT + (i + j) * 0.1f) * waveAmplitude;
                pos += new Vector3(0.0f, waveOffset, 0.0f);
                cube.transform.position = pos;
            }
        }

        float t = Mathf.PingPong(Time.time, 1.0f);
        float scale = Mathf.Lerp(1.0f, 1.5f, t
        );
        float colorT = Mathf.Lerp(0.0f, 1.0f, scale);
        Color color = Color.Lerp(inhaleColor, exhaleColor, colorT);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                GameObject cube = cubes[i, j];
                cube.GetComponent<Renderer>().material.color = color;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        float radius = amplitude;
        for (int i = 0; i < rows; i++)
        {
            float y = (float)i * cubeSize - amplitude;
            float r = Mathf.Sqrt(radius * radius - y * y);
            Gizmos.DrawWireSphere(center + new Vector3(0, y, 0), r);
        }
    }
}
