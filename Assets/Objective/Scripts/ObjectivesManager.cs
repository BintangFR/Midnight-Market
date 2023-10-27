using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectivesManager : MonoBehaviour
{
    public TextMeshProUGUI UIDescription;
    public TextMeshProUGUI UIHint;
    public ObjectiveController[] objectiveDatas;
    public float elapsedTime = 0f;

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
    public void ShowHint(string hint)
    {
        UIHint.text = hint;
    }
    private void Update()
    {
        foreach (ObjectiveController objectiveController in objectiveDatas)
        {
            if (objectiveController.isActive)
            {
                ShowDescription(objectiveController);
                elapsedTime += Time.deltaTime;

                if (elapsedTime >= 10f)
                {
                    if (!objectiveController.isComplete)
                    {
                        ShowHint(objectiveController.hint); // pake hint dari ObjectiveController
                    }

                }

                else
                {
                    UIHint.text = "";

                }

                break;
            }

        }


        foreach (ObjectiveController objectiveController in objectiveDatas)
        {
            bool ObjectiveResult = objectiveDatas.All(objectiveController => objectiveController.isComplete);

            if (ObjectiveResult)
            {
                GameManager.instance.FinishGame();
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
