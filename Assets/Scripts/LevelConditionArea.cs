using UnityEngine;

namespace SpaceShooter
{
    public class LevelConditionArea : MonoBehaviour, ILevelCondition
    {
        [SerializeField] private Area m_Area;
 
        bool ILevelCondition.IsComplited
        {
            get
            {
                if (Player.Instance.Ship == null) return false;

                if ((m_Area.transform.position - Player.Instance.Ship.transform.position).sqrMagnitude < m_Area.Radius * m_Area.Radius)
                {
                    return true;
                }
                else 
                { 
                    return false; 
                }
            }
        }
    }
}

