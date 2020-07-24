using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CubeBreak : MonoBehaviour
{

    public float cubeSize = 0.2f;
    public int cubesInRow = 5;

    float cubesPivotDistance;
    Vector3 cubesPivot;

    public float explosionForce = 50f;
    public float explosionRadius = 4f;
    public float explosionUpward = 0.4f;

    Vector3 porce;

    int state;
    //
    void Start()
    {
        cubesPivotDistance = cubeSize * cubesInRow / 2;
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);

        state = 0;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (state == 1) return;
        if (collision.collider.CompareTag("Player"))
        {
            this.GetComponent<MeshRenderer>().enabled = false;
            this.GetComponent<BoxCollider>().isTrigger = true;
            //
            //
            state = 1;

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
                    rb.AddExplosionForce(explosionForce, transform.position + new Vector3(0, 0.3f, 0), explosionRadius, explosionUpward);
                }
            }
            StartCoroutine(cDelay(true));
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (state != 2) return;
        if (other.CompareTag("Player"))
        {
            Rigidbody[] rb = this.GetComponentsInChildren<Rigidbody>();
            for (int i = 0; i < rb.Length; i++)
            {
                rb[i].isKinematic = false;
            }

            Vector3 explisionsPos = this.transform.position;
            Collider[] colliders = Physics.OverlapSphere(explisionsPos, explosionRadius);

            foreach (Collider hit in colliders)
            {

                Rigidbody rbb = hit.GetComponent<Rigidbody>();
                if (rbb != null)
                {
                    rbb.AddExplosionForce(explosionForce, transform.position + new Vector3(0, 1f, 0), explosionRadius, explosionUpward);
                }
            }
        }
    }

    IEnumerator cDelay(bool isKine)
    {
        yield return new WaitForSeconds(0.13f);
        Rigidbody[] rb = this.GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < rb.Length; i++)
        {
            rb[i].isKinematic = isKine;
        }

        if (isKine)
        {
            state = 2;
        }
        else
        {
            state = 3;
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
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);

        piece.transform.position = transform.position + new Vector3(x, y, z) * cubeSize - cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = cubeSize;

        piece.transform.parent = this.transform;
    }
}
