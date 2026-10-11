using System.Collections;
using UnityEngine;

public class INT_Door : Interactables
{
    public Outline Outline;
    public Animator Anim;
    PlayerMovement player;

    public bool canUse;
    protected override void Start()
    {
        base.Start();
        player = FindAnyObjectByType<PlayerMovement>();
    }
    public override void Look()
    {
        base.Look();
        Outline.enabled = true;
    }
    public override void StopLooking()
    {
        base.StopLooking();
        Outline.enabled = false;
    }

    public override void Use()
    {
        base.Use();
        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("DoorClose"))
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            float dot = Vector3.Dot(transform.forward, direction);
            if(dot > 0f)
            {
                Anim.SetTrigger("OpenFront");
            }
            else
            {
                Anim.SetTrigger("OpenBack");
            }
        }
        else
        {
            Anim.SetTrigger("Close");
        }
    }

    IEnumerator UseCooldown()
    {
        canUse = false;
        yield return new WaitForSeconds(0.5f);
    }
}
