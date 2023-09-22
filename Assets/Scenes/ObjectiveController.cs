using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveController : MonoBehaviour
{

    public ObjectivesManager objectiveManager;


    public void MarkObjectiveAsComplete(ObjectiveController objective)
    {
        objective.IsComplete = true;

        // untuk mengecek objective 1 sudah selesai atau belum
        if (objective.ID == 1)
        {
            // mengactivate objective 2 jika obj 1 sudah selesai
            if (CheckConditionsForObjective2())
            {
                // Aktifkan objektif 2
                ObjectiveController objective2 = GetObjectiveByID(2);
                if (objective2 != null)
                {
                    ActivateObjective(objective2);
                }
            }
        }
    }

    private bool CheckConditionsForObjective2()
    {
        //untuk memeriksa apakah obj 1 sudah selesai atau belum
        ObjectiveController objective1 = GetObjectiveByID(1);
        if (objective1 != null && objective1.IsComplete)
        {
            //untuk menambahkan kondisi lain jika ada
            return true;
        }
        return false;
    }

    private ObjectiveController GetObjectiveByID(int objectiveID)
    {
        foreach (var objective in objectiveManager.objectiveDatas)
        {
            if (objective.ID == objectiveID)
            {
                return objective;
            }
        }
        return null;
    }

    private void ActivateObjective(ObjectiveController objective)
    {
        objective.IsActive = true;
    }

    public int ID;
    public string Description;
    public bool IsComplete;
    public bool IsActive;
    public ObjectiveType Type;
    public float Progress;
    public Requirement ObjectiveRequirement;
    public HintData ObjectiveHint;

    public ObjectiveController(int id, string description, ObjectiveType type, Requirement requirement, HintData hint)
    {
        ID = id;
        Description = description;
        IsComplete = false;
        IsActive = false;
        Type = type;
        Progress = 0f;
        ObjectiveRequirement = requirement;
        ObjectiveHint = hint;
    }


    public class Requirement
    {
        public string Description;
        public bool IsFulfilled;

        public Requirement(string description)
        {
            Description = description;
            IsFulfilled = false;
        }
    }

    public class HintData
    {
        public string Description;
        public HintType Type;
        public bool IsActive;

        public HintData(string description, HintType type)
        {
            Description = description;
            Type = type;
            IsActive = false;
        }
    }

    public enum ObjectiveType
    {
        Item,
        Trigger,
        Activate
    }

    public enum HintType
    {
        FuseHint,
        VentHint,
        ToolkitHint
    }

    
}

