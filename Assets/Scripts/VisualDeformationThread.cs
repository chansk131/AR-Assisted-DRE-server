using UnityEngine;
using System.Collections;

public class VisualDeformationThread : MonoBehaviour
{

    public float springForce = 20f;
    public float damping = 5f;
    public float softness = 10.0f;
    public bool touchedFromOutside = true;

    Mesh deformingMesh;
    Vector3[] originalVertices, displacedVertices;
    Vector3[] vertexVelocities;

    float uniformScale = 1f;

    void Start()
    {
        deformingMesh = GetComponent<MeshFilter>().mesh;
        originalVertices = deformingMesh.vertices;
        displacedVertices = new Vector3[originalVertices.Length];
        for (int i = 0; i < originalVertices.Length; i++)
        {
            displacedVertices[i] = originalVertices[i];
        }
        vertexVelocities = new Vector3[originalVertices.Length];
    }

    void Update()
    {
        //uniformScale = transform.localScale.x;
        uniformScale = transform.lossyScale.x;
        for (int i = 0; i < displacedVertices.Length; i++)
        {
            UpdateVertex(i);
        }
        deformingMesh.vertices = displacedVertices;
        deformingMesh.RecalculateNormals();
    }

    void UpdateVertex(int i)
    {
        Vector3 velocity = vertexVelocities[i];
        Vector3 displacement = displacedVertices[i] - originalVertices[i];
        displacement *= uniformScale;
        velocity -= displacement * springForce * Time.deltaTime;
        velocity *= 1f - damping * Time.deltaTime;
        vertexVelocities[i] = velocity;
        displacedVertices[i] += velocity * (Time.deltaTime / uniformScale);
    }

    public void AddDeformingForce(Vector3 point, float force)
    {
        point = transform.InverseTransformPoint(point);
        for (int i = 0; i < displacedVertices.Length; i++)
        {
            AddForceToVertex(i, point, force);
        }
    }

    void AddForceToVertex(int i, Vector3 point, float force)
    {
        Vector3 pointToVertex = displacedVertices[i] - point;
        /*Vector3 pointToVertex = Vector3.zero;
		if (touchedFromOutside) {
			pointToVertex = displacedVertices [i] - point;
		} else {
			pointToVertex = displacedVertices[i] + point;
		}*/
        pointToVertex *= uniformScale;
        //float attenuatedForce = force / (1f + pointToVertex.sqrMagnitude);
        float attenuatedForce = softness / (1f + pointToVertex.sqrMagnitude);
        float velocity = attenuatedForce * Time.deltaTime;

        if (touchedFromOutside)
        {
            vertexVelocities[i] += pointToVertex.normalized * velocity;
        }
        else
        {
            vertexVelocities[i] -= pointToVertex.normalized * velocity;
        }
    }

}
