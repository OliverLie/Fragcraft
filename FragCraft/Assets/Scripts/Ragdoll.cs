using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    public Transform pelvisBone;

    public Rigidbody[] rigidBodies;
    Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBodies = GetComponentsInChildren<Rigidbody>();
        anim = GetComponent<Animator>();
        DeactiveRagdoll();

    }

    // Update is called once per frame
    public void DeactiveRagdoll()
    {
        foreach (var rigidBody in rigidBodies)
        {
            rigidBody.isKinematic = true;
        }
        anim.enabled = true;
    }

    public void ActiveRagdoll()
    {
        foreach (var rigidBody in rigidBodies)
        {
            rigidBody.isKinematic = false;
        }
        anim.enabled = false;
    }



    public void ApplyForce(Vector3 Force)
    {
        Rigidbody targetRb = pelvisBone.GetComponent<Rigidbody>();

        if (targetRb != null)
        {
        targetRb.AddForce(Force, ForceMode.VelocityChange);
        }
    }

}
