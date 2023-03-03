
using UnityEngine;

public class Apple : MonoBehaviour
{

    [SerializeField] BoxCollider2D gridArea;
    private Bounds bounds;
    void Start()
    {
        bounds = gridArea.bounds;
        RandomizeApple();
    }



    void RandomizeApple()
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            RandomizeApple();
        }
    }
}
