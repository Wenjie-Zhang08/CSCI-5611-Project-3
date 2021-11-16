using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joints : MonoBehaviour
{
    // Start is called before the first frame update
    /*
    [SerializeField] 
    private joints parentL;
    [SerializeField]
    private joints childL;
    */
    public joints parent;
    public joints child;
    /*
    [SerializeField]
    private Vector3 minEulerL = Vector3.zero;
    [SerializeField]
    private Vector3 maxEulerL = Vector3.zero;
    [SerializeField]
    private Vector3 minEulerR = Vector3.zero;
    [SerializeField]
    private Vector3 maxEulerR = Vector3.zero;
    [SerializeField]
    */
    //public Transform overallParent;

    //public Vector3 minEuler;
    //public Vector3 maxEuler;

    //Vector3 startEuler;
    private Vector3 localEuler;

    public float lenToChild;

    private float angularV = 20f;
    
    public Vector3 EulerSpeed = Vector3.zero;

    private Quaternion localRot;
    void Start()
    {
        //localEuler = startEuler;
        //this.transform.localRotation = Quaternion.Euler(startEuler);
        localEuler = Vector3.zero;
    }
    /*
    public void init()
    {
        localRot = Quaternion.Euler(startEuler.x, startEuler.y, startEuler.z);//++------

        //this.transform.rotation = localRot;
        if (parent)
        {

            Vector3 pos = this.parent.transform.position;
            Vector3 dir = this.parent.transform.rotation * Vector3.forward;
            this.transform.position = pos + dir * parent.lenToChild;
            this.transform.rotation = parent.transform.rotation * localRot;
        }
        else
        {
            this.transform.rotation = localRot;
        }
        if (child)
        {
            child.init();
        }
    }

    public void calculateEuler()
    {
        if (parent)
        {

        }
        else
        {
            this.localRot = this.transform.rotation;
            this.localEuler = this.localRot.eulerAngles;
        }
        if (child)
        {
            child.calculateEuler();
        }
    }



    public void calculatePos()
    {
        if (parent)
        {
            Vector3 pos = this.parent.transform.position;
            Vector3 dir = this.parent.transform.rotation * Vector3.forward;
            this.transform.position = pos + dir * parent.lenToChild;
        }
        if (child)
        {
            child.calculatePos();
        }

    }

    */
    /*
    public void resetParents()
    {
        this.transform.SetParent(overallParent,true);
        if (this.parent)
        {
            this.parent.resetParents();
        }
    }


    public void resetToCurrentChild()
    {
        if (this.child)
        {
            this.child.transform.SetParent(this.transform,true);
            this.child.resetToCurrentChild();
            Debug.Log(this.child.transform.localRotation.eulerAngles);
        }

    }
    */
    public void resetToCurrentEuler()
    {
        //this.localEuler = Vector3.zero;
        //this.localRot = Quaternion.Euler(localEuler);
        this.localRot = this.transform.localRotation;
        this.localEuler = -localRot.eulerAngles;
        if (this.localEuler.x > 180.0f) this.localEuler.x -= 360f;
        if (this.localEuler.y > 180.0f) this.localEuler.y -= 360f;
        if (this.localEuler.z > 180.0f) this.localEuler.z -= 360f;
        if (this.parent)
        {
            parent.resetToCurrentEuler();
        }
    }

    /*
    public void setVersion(bool left)
    {
        if (left)
        {
            parent = parentL;
            child = childL;
            minEuler = minEulerL;
            maxEuler = maxEulerL;
            if (childL)
            {
                childL.setVersion(left);
            }
        }
        else
        {
            parent = childL;
            child = parentL;
            minEuler = minEulerR;
            maxEuler = maxEulerR;
            if (childL)
            {
                childL.setVersion(left);
            }
        }
    }
    */

    public void calculateLocalAngle()
    {

    }


    public void IK_V1(Transform goal, Transform endEffector)
    {
        if (!child)
        {
            parent.IK_V1(goal, endEffector);
            return;
        }

        Vector3 stg = goal.position - this.transform.position;
        stg.Normalize();
        Vector3 ste = endEffector.position - this.transform.position;
        ste.Normalize();
        /*
        float angleBetween = Mathf.Acos(Vector3.Dot(stg, ste)) * 180.0f / Mathf.PI ;
        
        if(angleBetween > 0)
        {
            //float angleTime = angularV * Time.deltaTime;
            //if (angleBetween > angleTime)
            //    angleBetween = angleTime;
            //Debug.Log(angleBetween);
            Vector3 axis = Vector3.Cross(ste, stg);
            axis.Normalize();
           
            Quaternion angleChange = Quaternion.AngleAxis(angleBetween, axis);
            if (!parent)
            {
                Debug.Log("Eulerian angle is " +  angleChange.eulerAngles);
            }
            Quaternion finalRotation = angleChange * this.transform.rotation;

            //angleChange.Normalize();
            Vector3 eulerChange = angleChange.eulerAngles;
            if(eulerChange.x > 180) eulerChange.x -= 360;
            if(eulerChange.y > 180) eulerChange.y -= 360;
            if (eulerChange.z > 180) eulerChange.z -= 360;
            /*
            if (!parent)
                Debug.Log("Old EulerChange is " + eulerChange);
            
            Vector3 eulerDist = EulerSpeed * Time.deltaTime;
            eulerChange.x = Mathf.Clamp(eulerChange.x, -eulerDist.x, eulerDist.x);
            eulerChange.y = Mathf.Clamp(eulerChange.y, -eulerDist.y, eulerDist.y);
            eulerChange.z = Mathf.Clamp(eulerChange.z, -eulerDist.z, eulerDist.z);
            angleChange = Quaternion.Euler(eulerChange.x, eulerChange.y, eulerChange.z);
            /*
            if (!parent)
            {
                Debug.Log("EulerDist is " + eulerDist);
                Debug.Log("EulerChange is " + eulerChange);
            }
            

        
            

            //this.transform.rotation = Quaternion.Lerp(this.transform.rotation, finalRotation, Time.deltaTime * angularV);
            //this.transform.rotation = finalRotation;

        }
        */


        Quaternion angleChange = Quaternion.FromToRotation(ste, stg);
        Vector3 EulerChange = angleChange.eulerAngles;
        if (EulerChange.x > 180.0f) EulerChange.x -= 360.0f;
        if (EulerChange.y > 180.0f) EulerChange.y -= 360.0f;
        if (EulerChange.z > 180.0f) EulerChange.z -= 360.0f;


        Vector3 angleDist = EulerSpeed * Time.deltaTime;

        EulerChange.x = Mathf.Clamp(EulerChange.x, -angleDist.x, angleDist.x);
        EulerChange.y = Mathf.Clamp(EulerChange.y, -angleDist.y, angleDist.y);
        EulerChange.z = Mathf.Clamp(EulerChange.z, -angleDist.z, angleDist.z);
        /*
        Quaternion localR = this.transform.rotation;
        if (parent)
        {
            localR = localR * Quaternion.Inverse(parent.transform.rotation);
        }
        
        Vector3 currentEuler = localR.eulerAngles;
        if (currentEuler.x > 180.0f) currentEuler.x -= 360.0f;
        if (currentEuler.y > 180.0f) currentEuler.y -= 360.0f;
        if (currentEuler.z > 180.0f) currentEuler.z -= 360.0f;
        */
        Vector3 currentEuler = localEuler;
        currentEuler += EulerChange;
        //if (minEuler.x == maxEuler.x) currentEuler.x = minEuler.x;
        ///////////////
        /// This should be the clamp
        /*
        currentEuler.x = Mathf.Clamp(currentEuler.x, minEuler.x, maxEuler.x);
        //if (minEuler.y == maxEuler.y) currentEuler.y = minEuler.y;
        currentEuler.y = Mathf.Clamp(currentEuler.y, minEuler.y, maxEuler.y);
        //if (minEuler.z == maxEuler.z) currentEuler.z = minEuler.z;
        currentEuler.z = Mathf.Clamp(currentEuler.z, minEuler.z, maxEuler.z);
        */
        ////////////////
        localEuler = currentEuler;
        localRot = Quaternion.Euler(currentEuler.x, currentEuler.y, currentEuler.z);

        //Quaternion globalRot = localRot;
        /*
        if (parent)
        {
            globalRot =    parent.transform.rotation  * globalRot;
        }
        */
        this.transform.localRotation = localRot;
        
        // after rotation calculate child position
        /*
        if (child)
        {
            child.calculatePos();
        }
        */
        

        if (parent)
            parent.IK_V1(goal, endEffector);
    }


    void updateLocation()
    {



    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
