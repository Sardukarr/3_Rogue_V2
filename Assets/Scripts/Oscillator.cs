using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movmentVector = new Vector3(0, 10f, 0);
    [SerializeField] float period = 2f;

    float movmentFactor;
    
     Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (period <= Mathf.Epsilon) return;
        float cycles = Time.time / period; // grows from 0 at start of game continually

        const float tau = Mathf.PI * 2f;
        float rawSinWave = Mathf.Sin(cycles * tau);

        movmentFactor = rawSinWave / 2f + 0.5f;
        Vector3 offset = movmentVector * movmentFactor;
        gameObject.transform.position = startPosition + offset;
    }
}
