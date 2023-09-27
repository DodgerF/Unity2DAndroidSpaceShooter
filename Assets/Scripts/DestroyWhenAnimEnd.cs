using UnityEngine;

[RequireComponent (typeof(Animator))]
public class DestroyWhenAnimEnd : MonoBehaviour
{
    [SerializeField] private float m_Time;
    void Start()
    {
        Destroy(gameObject, m_Time);
    }
}
