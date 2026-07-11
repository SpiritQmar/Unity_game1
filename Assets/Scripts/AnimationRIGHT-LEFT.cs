using UnityEngine;

public class AnimationRIGHTLEFT : MonoBehaviour
{
    public float speed = 5f;  
    public float leftBoundary = -10f;  

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

         
        if (transform.position.x < leftBoundary)
        {
            Destroy(gameObject);
        }
    }
}
