using UnityEngine;

public class EnemyHand : MonoBehaviour
{
    [SerializeField] private AIController aIController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            aIController.GetPlayer().GetComponent<PlayerController>().TakeDamage();
        }
    }
}
