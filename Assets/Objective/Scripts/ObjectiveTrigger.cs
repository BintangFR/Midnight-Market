using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectiveTrigger : MonoBehaviour
{
    public enum ObjectiveType{
        Start,
        End
    }
    public ObjectiveType objectiveType;

    public UnityEvent startObjective;
    public UnityEvent endObjective;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (objectiveType == ObjectiveType.Start)
        {
            startObjective.Invoke();
        }
        else if (objectiveType == ObjectiveType.End)
        {
            endObjective.Invoke();
        }
    
    }
}
