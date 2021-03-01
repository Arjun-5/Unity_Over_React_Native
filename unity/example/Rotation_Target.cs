using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation_Target : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private List<GameObject> shootingPoints = null;

    private int rotationValue;
    private bool done;
    void Start()
    {
        done = false;
        rotationValue = 90;
    }

    // Update is called once per frame
    void Update()
    {
        if (!done && rotationValue > 0)
        {
            done = true;
            for(int i = 0; i < shootingPoints.Count; i++)
            {
                shootingPoints[i].transform.Rotate(Vector3.right * -2);
                if (i == shootingPoints.Count - 1)
                {
                    done = false;
                    rotationValue -= 2;
                }
            }
        }
    }
}
