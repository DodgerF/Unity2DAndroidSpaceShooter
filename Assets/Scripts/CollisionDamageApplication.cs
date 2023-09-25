using UnityEngine;

namespace SpaceShooter
{
    public class CollisionDamageApplication : MonoBehaviour
    {
        public static string IgnoreTag = "WorldBoundary";

        [SerializeField] private float _velocityDamageModifier;
        [SerializeField] private float _damageConstant;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag == IgnoreTag) return;

            if (TryGetComponent<Destructible>(out var destructible))
            {
                destructible.ApplyDamage((int)_damageConstant + (int)(_velocityDamageModifier * collision.relativeVelocity.magnitude));
            }
        }
    }
}

