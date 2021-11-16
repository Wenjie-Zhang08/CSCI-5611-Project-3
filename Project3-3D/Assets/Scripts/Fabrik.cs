using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fabrik : MonoBehaviour
{
    public joints[] jointls;
    public Transform goal;
    private float[] lens;
    private int numJoints;
    private float totalLens;
    private joints endEffector;
    private joints root;
    // Start is called before the first frame update
    void Start()
    {
        numJoints = jointls.Length;
        lens = new float[numJoints - 1];
        totalLens = 0;
        for(int i = 0; i < numJoints - 1; i++)
        {
            lens[i] =(jointls[i].transform.position 
                - jointls[i + 1].transform.position).magnitude;
            totalLens += lens[i];
        }
        endEffector = jointls[numJoints - 1];
        root = jointls[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = (goal.position - root.transform.position);
        float dist = dir.magnitude;
        if(dist > totalLens)
        {
            // this sitation, we will point to the goal
            dir = dir.normalized;
            for(int i = 1; i < numJoints; i++)
            {
                jointls[i].transform.position = jointls[i - 1].transform.position + dir * lens[i - 1];
            }
        }
        else
        {
            // in this situation, the goal can be achieved
            int maxIter = 10;
            endEffector.transform.position = goal.position;
            for (int i = 0; i < maxIter; i++)
            {
                
                for(int j = numJoints - 2; j > 0; j--)
                {
                    Vector3 currDir = jointls[j].transform.position - jointls[j + 1].transform.position;
                    currDir = currDir.normalized;
                    currDir *= lens[j];
                    Vector3 currPos = jointls[j + 1].transform.position + currDir;
                    jointls[j].transform.position = currPos;
                }


                // Then we do it from root
                for (int j = 1; j < numJoints - 1; j++)
                {
                    Vector3 currDir = jointls[j].transform.position - jointls[j - 1].transform.position;
                    currDir = currDir.normalized;
                    currDir *= lens[j - 1];
                    Vector3 currPos = jointls[j - 1].transform.position + currDir;
                    jointls[j].transform.position = currPos;
                }
            }


        }
        for(int i = 0; i < numJoints - 1; i++)
        {
            jointls[i].transform.LookAt(jointls[i + 1].transform);
        }
    }
}
