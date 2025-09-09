using UnityEditor.Callbacks;
using UnityEngine;

public class AirBender : MonoBehaviour
{
    [SerializeField] GameObject AirbenderVFX; // fix så det virker
    public PotentialAIScript potentialAIScript;
    public Vector3 Thrustforce ;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            potentialAIScript.AirbenderForce(Thrustforce);
        }
    }
    

}
