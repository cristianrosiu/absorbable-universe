using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AbsorbablesManager : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private GameObject abosrbableObject;
    [SerializeField] private BlackHole blackHole;

    private int[] _choices = {-1, 1};
    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 0f, 1/spawnRate);
    }

    public void Spawn()
    {
        var randomAngle = Random.Range(0f, 2f * Mathf.PI);
        var randomRadius = Random.Range(blackHole.GetEventHorizon() * 2.28f, blackHole.GetEventHorizon() * 4f);
        
        var pointOnCircle = Utils.FromAngle(randomAngle, randomRadius);

        var centerToPoint = pointOnCircle - (Vector2)blackHole.transform.position;
        var tangentDir = new Vector2(-centerToPoint.y, centerToPoint.x).normalized;

        var spawnPoint = centerToPoint - tangentDir * 5;

        var newObject = Instantiate(abosrbableObject, spawnPoint, Quaternion.identity);

        var randomIndex = Random.Range(0, _choices.Length);
        var localScale = newObject.transform.localScale;
            
        localScale = new Vector3(
            _choices[randomIndex] * localScale.x,
            _choices[randomIndex] * localScale.y, 
            localScale.z
        );
        
        newObject.transform.localScale = localScale;

        newObject.GetComponent<AbosrbableObject>().Theta = Mathf.Atan2(tangentDir.y, tangentDir.x);
    }
}
