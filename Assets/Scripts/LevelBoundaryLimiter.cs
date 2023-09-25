using UnityEngine;


namespace SpaceShooter
{
    /// <summary>
    /// Ограничивает рамки уровня. Работает вместо с LevelBoundary, если такой есть на сцене.
    /// Вешается на объект, котороый необходимо ограничить.
    /// </summary>
    public class LevelBoundaryLimiter : MonoBehaviour
    {
        private void Update()
        {
            if (LevelBoundary.Instance == null) return;

            var levelBoundary = LevelBoundary.Instance;
            var radius = LevelBoundary.Instance.Radius;

            if (transform.position.magnitude > radius)
            {
                //Выходя за границы уровня, останавливает объект
                if (levelBoundary.BoundaryMode == LevelBoundary.Mode.Limit)
                {
                    transform.position = transform.position.normalized * radius;
                }

                //Выходя за границы уровня, телепортирует в противоположную сторону
                if (levelBoundary.BoundaryMode == LevelBoundary.Mode.Teleport)
                {
                    transform.position = -transform.position.normalized * radius;
                }
            }
        }
    }
}

