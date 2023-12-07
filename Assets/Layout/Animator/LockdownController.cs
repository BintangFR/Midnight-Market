using UnityEngine;

public class LockdownController : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OpenLockdown()
    {
        anim.SetTrigger("open");
        AudioManager.Instance.PlaySFX("Lockdown", transform.position);
    }

    public void CloseLockdown()
    {
        anim.SetTrigger("close");
        AudioManager.Instance.PlaySFX("Lockdown", transform.position);
    }
}
