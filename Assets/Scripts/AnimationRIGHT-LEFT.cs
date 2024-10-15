using UnityEngine;

public class AnimationRIGHTLEFT : MonoBehaviour
{
    public float speed = 5f; // Скорость движения объекта
    public float leftBoundary = -10f; // Координата X, за которую объект будет удален

    void Update()
    {
        // Перемещаем объект влево по оси X
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // Удаляем объект, если он пересек левую границу экрана
        if (transform.position.x < leftBoundary)
        {
            Destroy(gameObject);
        }
    }
}
