using UnityEngine;

namespace SpaceShooter
{
    public class LevelConditionEnemies : MonoBehaviour, ILevelCondition
    {
        private bool isFind;
        bool ILevelCondition.IsComplited
        {
            get
            {
                isFind = false;
                foreach (var destructable in Destructible.AllDestructibles)
                {
                    if (destructable.TryGetComponent<AIController>(out var controller))
                    {
                        isFind = true;
                    }
                }

                if (isFind)
                {
                    return false;
                }
                
                return true;
            }
        }
    }
}
