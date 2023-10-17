using UnityEngine;

namespace SpaceShooter
{
    public class LevelConditionAstronuats : MonoBehaviour, ILevelCondition
    {
        bool ILevelCondition.IsComplited
        {
            get
            {
                if (AstronautsCounter.Instance.Amount == AstronautsCounter.Instance.Saved)
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
