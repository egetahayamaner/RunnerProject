using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWorld : MonoBehaviour
{
    public GameObject[] platforms;
    GameObject dummyTraveller;

    // Start is called before the first frame update
    void Start()
    {
        dummyTraveller = new GameObject("dummy");

        for (int i = 0; i < 10; i++)
        {
            int platformNumber = Random.Range(0, platforms.Length);
            GameObject p = Instantiate(platforms[platformNumber], dummyTraveller.transform.position, dummyTraveller.transform.rotation);

            if (platforms[platformNumber].tag == "stairsUp")
            {
                dummyTraveller.transform.Translate(0,5,0);
            }
            else if (platforms[platformNumber].tag == "stairsDown")
            {
                dummyTraveller.transform.Translate(0,-5,0);
                p.transform.Rotate(new Vector3(0, 180, 0));
                p. transform.position = dummyTraveller.transform.position;
            }
            else if (platforms[platformNumber].tag == "platformTSection")
            {
                if (Random. Range (0,2) == 0)
                {
                    dummyTraveller. transform.Rotate(new Vector3(0, 90, 0));
                }
                else
                {
                    dummyTraveller. transform. Rotate(new Vector3(0, -90, 0));
                }
                dummyTraveller.transform.Rotate(new Vector3(0, 90, 0));
                dummyTraveller.transform.Translate(Vector3. forward * -10);
            }

            dummyTraveller.transform.Translate(Vector3. forward * -10);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
