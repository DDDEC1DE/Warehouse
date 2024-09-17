using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Package : MonoBehaviour
{
    //public House m_house;
    public MissionManager m_missionManager;

    public int packageID; 
    public Vector3 deliveryPoint; 
    public bool isDelivered = false;
    bool m_isFired;
    bool m_isDelivered;
    bool m_isStolen;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
