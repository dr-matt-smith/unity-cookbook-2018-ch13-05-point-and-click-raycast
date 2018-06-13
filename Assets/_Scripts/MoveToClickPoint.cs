using UnityEngine;
using UnityEngine.AI;

// adapted from Unity exmaple:
// https://docs.unity3d.com/Manual/nav-MoveToClickPoint.html

public class MoveToClickPoint : MonoBehaviour
{
    public GameObject sphereDestination;
    public GameObject sphereDestinationCandidate;

    private NavMeshAgent navMeshAgent;    
    private RaycastHit hit;
   
    void Start() 
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.destination = transform.position;
    }
    
    void Update() 
    {
        Ray rayFromMouseClick = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (FireRayCast(rayFromMouseClick)){
            Vector3 rayPoint = hit.point;
            ProcessRayHit(rayPoint);
        }
    }

    // RayCast hit a surface - do something, depending of whether mouse was clicked ...
    private void ProcessRayHit(Vector3 rayPoint)
    {
        if(Input.GetMouseButtonDown(0)) {
            // (1) set hitPoint as new NavmeshAgent destination
            navMeshAgent.destination = rayPoint;
            
            // (2) move Red sphere to destination point
            sphereDestination.transform.position = rayPoint;
        } else {
            // move yellow sphere to destination point
            sphereDestinationCandidate.transform.position = rayPoint;
        }
    }

    // return NegativeInfinity if Raycast did not hit a surface
    private bool FireRayCast(Ray rayFromMouseClick)
    {
        // ignore layer "UISpheres"
        LayerMask layerMask = ~LayerMask.GetMask("UISpheres");
        return Physics.Raycast(rayFromMouseClick, out hit, 100, layerMask.value);
    }
}