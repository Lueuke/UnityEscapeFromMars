using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Occilator : MonoBehaviour
{

    Vector3 startPosition;
  [SerializeField]  Vector3 movementVector;
    float movementFactor;

  [SerializeField] float period = 2f;
    void Start()
    {
        startPosition = transform.position;
      
    }

    
    void Update()
    {
    if( period <= Mathf.Epsilon)
    {
        return;
    }
        float cycles = Time.time / period;


        const float tau = Mathf.PI * 2;
        float rawSineWave = Mathf.Sin(cycles * tau);

        movementFactor = (rawSineWave + 1f)/2f;


        Vector3 offset = movementVector * movementFactor;
        transform.position = startPosition + offset;
    }
}
