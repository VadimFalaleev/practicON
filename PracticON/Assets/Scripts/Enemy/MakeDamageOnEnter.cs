using UnityEngine;

public class MakeDamageOnEnter : MonoBehaviour
{
    [SerializeField] private int _damageValue;

    private void OnTriggerStay(Collider other)
    {
        Touch(other);
    }

    private void OnCollisionStay(Collision collision)
    {
        Touch(collision.collider);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Touch(collision.collider);
    }

    private void Damaged(PlayerHealth playerHealth)
    {
        playerHealth.TakeDamage(_damageValue);
    }

    private void Touch(Collider collider)
    {
        if (collider.attachedRigidbody)
        {
            PlayerHealth playerHealth = collider.attachedRigidbody.GetComponent<PlayerHealth>();
            //SwordCollider swordCollider = collider.attachedRigidbody.GetComponent<SwordCollider>();
            if (playerHealth /*&& swordCollider == false*/)
            {
                Damaged(playerHealth);
            }
        }
    }
}