using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectivesManager : MonoBehaviour
{
    public TextMeshProUGUI UIDescription;
    public ObjectiveController[] objectiveDatas;

   public void AddObjective(ObjectiveController objective)
   {
    
   }

   public void RemoveObjective(ObjectiveController objective)
   {
    
   }
    public void ShowDescription(ObjectiveController objective)
    {
        UIDescription.text = objective.description;
    }

    private void Update()
    {
        foreach (ObjectiveController objectiveController in objectiveDatas)
        {
            bool ObjectiveResult = objectiveDatas.All(objectiveController => objectiveController.isComplete);

            if (ObjectiveResult)
            {
                GameManager.Instance.FinishGame();
            }
            
            if (objectiveController.isActive)
            {
                ShowDescription(objectiveController);
            }

        }

        
       
    }

    private void Start()
    {
        ObjectiveController objective1 = objectiveDatas[0];
        objective1.ActivateObjective(objective1);
    }
}
