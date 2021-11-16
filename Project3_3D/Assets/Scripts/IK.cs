using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IK : MonoBehaviour
{
    /*
    [SerializeField]
    private joints rootJointL;
    [SerializeField]
    private Transform goal;
    //[SerializeField]
    */
    public Transform currentGoal;
    /*
    [SerializeField]
    private joints endEffectorL;
    */
    public joints rootJoint;
    public joints endEffector;

    //public bool left = true;
    // Start is called before the first frame update
    void Start()
    {
        /*
        rootJoint = rootJointL;
        endEffector = endEffectorL;
        left = true;
        rootJoint.setVersion(left);
        //rootJoint.GetComponent<joints>().init();
        goal.position = endEffector.transform.position;
        currentGoal = goal.GetComponent<Goals>().GetNextChildren();
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        endEffector.GetComponent<joints>().IK_V1(currentGoal, endEffector.transform);

        /*
        if (currentGoal)
        {
            float dist = (currentGoal.position - endEffector.transform.position).magnitude;
            if (dist < 1)
            {
                currentGoal = goal.GetComponent<Goals>().GetNextChildren();
            }
            if (currentGoal)
                endEffector.GetComponent<joints>().IK_V1(currentGoal, endEffector.transform);
        }
        else
        {

            // reset goal index, regenrate goal position, reset left

            if(left)
            {
                left = false;
                rootJoint = endEffectorL;
                endEffector = rootJointL;
            }
            else
            {
                left = true; 
                rootJoint = rootJointL;
                endEffector = endEffectorL;

            }

            rootJointL.setVersion(left);

            // reset parents
            endEffector.resetParents();
            rootJoint.resetToCurrentChild();
            endEffector.resetToCurrentEuler();
            // reset goal
            goal.position = endEffector.transform.position;
            goal.GetComponent<Goals>().resetIndex();
            currentGoal = goal.GetComponent<Goals>().GetNextChildren();
        }
        */

    }
}
