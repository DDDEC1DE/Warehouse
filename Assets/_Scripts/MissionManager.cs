using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Burst.Intrinsics;
using System.Linq;



//  If you use network synchronization,
//  make sure that all packages and delivery targets on the server
//  are synchronized between clients.

//  Make sure that all events (Mission Generation, Package Delivery)
//  are correctly processed on both the server and client sides.
public class MissionManager : NetworkBehaviour
{
    //public GameManager m_gameManager; 
    //public AIManager m_aiManager;  
    public float m_estimatedDeliveryTime;
    public Package[] m_DeliveryRoute;
    public int totalPackages = 5;
    public List<Package> packages = new List<Package>();
    void Start()
    {

    }
    void Update()
    {

    }

    [ServerRpc]
    public void GenerateMissionServerRpc()
    {
        for (int i = 0; i < totalPackages; i++)
        {
            Package newPackage = new Package();
            newPackage.packageID = i;
            GetDeliveryPoint();
            packages.Add(newPackage);
        }
    }

    public void GetDeliveryPoint()
    {

    }

    [ClientRpc]
    public void AssignPackagesToPlayersClientRpc()
    {
        foreach (var package in packages)
        {
            Debug.Log($"Package {package.packageID} assigned. Deliver to {package.deliveryPoint}");
        }
    }
    public float CalculateEstimatedTime()
    {
        float totalDistance = 0f;
        foreach (var package in m_DeliveryRoute)
        {
            totalDistance += Vector3.Distance(Vector3.zero, package.deliveryPoint); 
        }
        
        return totalDistance / 10f;
    }

    
    public void Delivered(Package package)
    {
        package.isDelivered = true;
        Debug.Log($"Package {package.packageID} successfully delivered!");
        CheckMissionCompletion();
    }

    
    public void Stolen(Package package)
    {
        packages.Remove(package);
        Debug.Log($"Package {package.packageID} was stolen!");
        CheckMissionCompletion();
    }

    public void CheckMissionCompletion()
    {
        int deliveredCount = packages.Count(p => p.isDelivered);
        if (deliveredCount >= totalPackages)
        {
            Debug.Log("Mission Completed");
        }
        else if (packages.Count == 0)
        {
            Debug.Log("Mission Failed");
        }
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("Collided with " + collision.gameObject.name);
    //}

}

