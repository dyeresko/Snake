using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public float speed = 0.25f;
    private Vector2 direction = new Vector2(0, 0);
    private List<Transform> segments = new List<Transform>();
    public int segmentsCount = 8;

    [SerializeField] Transform segmentPrefab;

    void Start()
    {
        segments.Add(this.transform);
        for (int i = 1; i < segmentsCount; i++)
        {
            Grow();
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down)
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up)
        {
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right)
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left)
        {
            direction = Vector2.right;
        }
    }

    void FixedUpdate()
    {
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        this.transform.position = new Vector3(this.transform.position.x + Mathf.Round(direction.x), this.transform.position.y + Mathf.Round(direction.y), 0);
    }

    void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = segments[segments.Count - 1].position;

        segments.Add(segment);
    }


    void ResetLevel()
    {
        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }
        segments.Clear();
        segments.Add(this.transform);

        for (int i = 1; i < segmentsCount; i++)
        {
            Grow();
        }

        direction = Vector2.zero;
        this.transform.position = Vector3.zero;
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Apple")
        {
            Grow();
        }
        else if (other.tag == "Obstacle")
        {
            ResetLevel();
        }
    }
}
