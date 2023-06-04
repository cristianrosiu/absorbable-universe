using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class BlackHole : MonoBehaviour
{
    [SerializeField] private float m = 1000;             // Mass of black-hole

    public float M
    {
        get => m;
        set => m = value;
    }

    [SerializeField] private float maxMass = 5f;
    [SerializeField] private float radiationRate = 1f;

    private const float LightSpeedSq = Utils.C * Utils.C;

    [SerializeField] private GameObject eventHorizonGFX;
    [SerializeField] private GameObject firstDiskGFX;
    [SerializeField] private GameObject secondDiskGFX;
    

    private void Update()
    {
        UpdateGFX();
        Radiate();
        PullAllObjects();
    }

    private void Radiate()
    {
        m = Mathf.Clamp(m - radiationRate * Time.deltaTime, 0, maxMass);
        
    }

    private void AddMass(float value)
    {
        m = Mathf.Clamp(m + value, 0, maxMass);

        if (Singleton.Instance.ScoreManager.score < m || m >= maxMass - 1)
            Singleton.Instance.ScoreManager.ChangeScore((int)value);

    }
    private void PullAllObjects()
    {
        var absorbedObjects = GameObject.FindGameObjectsWithTag("Absorbable");

        foreach (var obj in absorbedObjects)
            Pull(obj.GetComponent<AbosrbableObject>());
    }
    private void Pull(AbosrbableObject other)
    {
        if (other.Abosrbed)
            return;
        
        var direction = transform.position - other.transform.position;
        var r = direction.magnitude;
        
        if (r <= GetEventHorizon())
        {
            other.DestroyObject();
            AddMass(other.Mass);
            return;
        }
        
        var fg = (Utils.G*M)/(r*r);
        var theta = Mathf.Atan2(direction.y, direction.x);
        var deltaTheta = -fg * (other.Speed * Time.deltaTime / Utils.C) * Mathf.Sin(other.Theta - theta);
        deltaTheta /= Mathf.Abs(1f - 2f * Utils.G * M / (r * Utils.C * Utils.C));
        
        other.Theta += deltaTheta;
        other.Velocity = Utils.FromAngle(other.Theta);
    }

    public float GetEventHorizon()
    {
        return 2 * Utils.G * M / LightSpeedSq;
    }

    private void UpdateGFX()
    {
        eventHorizonGFX.transform.localScale = new Vector3(GetEventHorizon(), GetEventHorizon(), 0f);
        firstDiskGFX.transform.localScale = new Vector3(GetEventHorizon()*1.5f, GetEventHorizon()*1.5f, 0f);
        secondDiskGFX.transform.localScale = new Vector3(GetEventHorizon()*3f, GetEventHorizon()*3f, 0f);
    }

    private void OnDrawGizmos()
    {
        DrawDisk();
        DrawDisk2();
        DrawDisk3();
        DrawEventHorizon();
    }

    private void DrawDisk()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 3*GetEventHorizon());
    }
    
    private void DrawDisk2()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 1.5f*GetEventHorizon());
    }
    private void DrawDisk3()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2.6f*GetEventHorizon());
    }
    private void DrawEventHorizon()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(transform.position, GetEventHorizon());
    }


}
