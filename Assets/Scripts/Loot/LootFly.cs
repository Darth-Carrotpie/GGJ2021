using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootFly : MonoBehaviour {
    public float maxFlyRange = 2f;
    public float speed = 2f;
    public float height = 0.5f;
    public float maxRotationSpeed = 0.5f;
    Vector3 destinationPosition;
    Vector3 startingPosition;
    Vector3 p1Position;
    Vector3 p2Position;
    float rotationSpeed;
    // Start is called before the first frame update
    float progress;
    void Start() {
        destinationPosition = Random.insideUnitCircle * maxFlyRange;
        destinationPosition = new Vector3(destinationPosition.x, 0, 0);
        destinationPosition += transform.position;
        startingPosition = transform.position;
        height = height + Random.Range(0.1f, 0.2f) + Mathf.Abs(destinationPosition.x) / 2f;
        p1Position = new Vector3(startingPosition.x, height, 0);
        p2Position = new Vector3(destinationPosition.x, height, 0);
        rotationSpeed = Random.Range(-2, 2);
        rotationSpeed = Mathf.Sign(rotationSpeed) * maxRotationSpeed;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (progress < 1) {
            progress += Time.deltaTime * speed;
            transform.position = Curves.CubeBezier3(startingPosition, p1Position, p2Position, destinationPosition, progress);
            transform.RotateAroundLocal(Vector3.forward, rotationSpeed);
        }
        if (progress >= 1)
            Destroy(this);
    }
}