using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKManager : MonoBehaviour
{
    //Root of the armature
    public Joint m_root;
    
    //End effector
    public Joint m_end;

    public GameObject m_target;

    public float m_threshold = 0.05f;

    public float m_rate = 5.0f;

    public int m_steps = 20; 

    float CalculateSlope(Joint _joint)
    {
        float deltaTheta = 0.01f;
        float distance1 = GetDistance(m_end.transform.position, m_target.transform.position);
        
        _joint.Rotate(deltaTheta);
        
        float distance2 = GetDistance(m_end.transform.position, m_target.transform.position);

        _joint.Rotate(-deltaTheta);

        return (distance2 - distance1) / deltaTheta;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < m_steps; ++i)
        {
            if (GetDistance(m_end.transform.position, m_target.transform.position) > m_threshold)
            {
                Joint current = m_root;
                while (current != null)
                {
                    float slope = CalculateSlope(current);
                    current.Rotate(-slope * m_rate);
                    current = current.GetChild();
                }
            }
        }
        
    }

    float GetDistance(Vector3 _point1, Vector3 _point2)
    {
        return Vector3.Distance(_point1, _point2);
    }
}
