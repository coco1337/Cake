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
    //
    void Start()
    {
        
        //this.gameObject.AddComponent<Rigidbody>();
        //this.GetComponent<Rigidbody>().useGravity = false;
        //this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        Player = null;

        cubesPivotDistance = cubeSize * cubesInRow / 2;
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);

        state = 0;
    }

    private void Update()
    {
        if( Player != null)
        {
            Vector3 a = new Vector3(Player.transform.localPosition.x, 0, Player.transform.localPosition.z);
            Vector3 b = new Vector3(this.transform.localPosition.x, 0, this.transform.localPosition.z);
            float dis = Vector3.Distance(a, b);
            
            if( dis > 1.2f)
            {

                state = 2;
                Player = null;
            }
        }
    }

    public void BreakOk(GameObject Player)
    {
        if (state == 3) return;
        if (state == 0)
        {
            this.Player = Player;
            Debug.Log("1번쨰");
            this.GetComponent<MeshRenderer>().enabled = false;
            this.GetComponent<BoxCollider>().isTrigger = true;
            //this.GetComponent<BoxCollider>().size = new Vector3(1, 1.2f, 1);
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
                    rb.AddExplosionForce(explosionForce, transform.position + new Vector3(0, 1f, 0), explosionRadius, explosionUpward);
                }
            }
            StartCoroutine(cDelay(true));
        }
        else if (state == 2)
        {
            Debug.Log("2번쨰");
            state = 3;
            this.GetComponent<BoxCollider>().isTrigger = true;
            //this.GetComponent<BoxCollider>().isTrigger = true;

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
                    rbb.AddExplosionForce(explosionForce, transform.position + new Vector3(0, 0f, 0), explosionRadius, explosionUpward);
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
            //state = 2;
            this.GetComponent<BoxCollider>().isTrigger = false;
            this.GetComponent<BoxCollider>().size = new Vector3(1, 0.8f, 1);
        }
        else
        {
            state = 3;
            //this.GetComponent<BoxCollider>().isTrigger = true;
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
        //piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
        piece = Instantiate(Cube);

        piece.transform.position = transform.position + new Vector3(x, y, z) * cubeSize - cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = cubeSize;
        //piece.GetComponent<Rigidbody>().useGravity = false;

        piece.transform.parent = this.transform;
    }
}
