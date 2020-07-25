using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBreak : MonoBehaviour
{
    public GameObject Cube;
    public float cubeSize = 0.2f;
    public int cubesInRow = 5;

    float cubesPivotDistance;
    Vector3 cubesPivot;

    public float explosionForce = 50f;
    public float explosionRadius = 4f;
    public float explosionUpward = 0.4f;

    Vector3 porce;

    public int state;
    bool isCheck;

    public GameObject Player;
    public bool isBreak = false;
    //
    void Start()
    {

        Player = null;

        cubesPivotDistance = cubeSize * cubesInRow / 2;
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);

        state = 0;
        isBreak = false;
    }


    public void BreakOk(GameObject Player)
    {
        if (isBreak) return;

        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<BoxCollider>().enabled = false;


        for (int x = 0; x < cubesInRow; x++)
        {
            for (int y = 0; y < cubesInRow; y++)
            {
                for (int z = 0; z < cubesInRow; z++)
                {
                    CreatePiece(x, y, z);
                }
            }

        }
        Vector3 explisionsPos = this.transform.position;

        Collider[] colliders = Physics.OverlapSphere(explisionsPos, explosionRadius);

        foreach (Collider hit in colliders)
        {

            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position + new Vector3(0, 1f, 0), explosionRadius, explosionUpward);
            }
        }
        StartCoroutine(cDelay());
    }

    IEnumerator cDelay()
    {
        yield return new WaitForSeconds(0.2f);
        this.GetComponent<BoxCollider>().enabled = false;
        Vector3 explisionsPos = this.transform.position;

        Collider[] colliders = Physics.OverlapSphere(explisionsPos, explosionRadius);

        foreach (Collider hit in colliders)
        {

            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
        }
        yield return new WaitForSeconds(0.6f);
        foreach (Collider hit in colliders)
        {

            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
        }
    }
    //
    void CreatePiece(int x, int y, int z)
    {
        if (x == cubesInRow / 2 && y == cubesInRow / 2 && z == cubesInRow / 2)
        {
            porce = new Vector3(x, y, z);
        }
        GameObject piece;

        piece = Instantiate(Cube);

        piece.transform.position = transform.position + new Vector3(x, y, z) * cubeSize - cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = cubeSize;

        piece.transform.parent = this.transform;
    }
}
