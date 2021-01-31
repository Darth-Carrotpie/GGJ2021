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
    float progress;
    bool doThrow;

    public void ThrowAt(Vector3 targetPosition) {
        if (targetPosition == Vector3.zero) {
            destinationPosition = Random.insideUnitCircle * maxFlyRange;
            destinationPosition = new Vector3(destinationPosition.x, 0, 0);
            destinationPosition += transform.position;
        } else {
            destinationPosition = new Vector3(targetPosition.x, 0, targetPosition.z);
            height = height / 3f;
        }
        startingPosition = transform.position;
        height = height + Random.Range(0.05f, 0.1f) + Mathf.Abs(destinationPosition.x - transform.position.x) / 3f;
        p1Position = new Vector3(startingPosition.x, height, height / 2f);
        p2Position = new Vector3(destinationPosition.x, height, height / 2f);
        rotationSpeed = Random.Range(-2, 2);
        rotationSpeed = Mathf.Sign(rotationSpeed) * maxRotationSpeed;
        doThrow = true;
    }

    void FixedUpdate() {
        if (!doThrow)
            return;
        if (progress < 1) {
            progress += Time.deltaTime * speed;
            transform.position = Curves.CubeBezier3(startingPosition, p1Position, p2Position, destinationPosition, progress);
            transform.GetComponentInChildren<SpriteRenderer>().transform.RotateAroundLocal(Vector3.up, rotationSpeed);
            DestroyOnCollision(Curves.CubeBezier3(startingPosition, p1Position, p2Position, destinationPosition, progress + Time.deltaTime * speed * 2f));
        }
        if (progress >= 1) {
            GetComponentInChildren<SpriteRenderer>().sortingOrder = 2;
            transform.position = destinationPosition;
            Destroy(this);
        }
    }
    void DestroyOnCollision(Vector3 direction) {
        if (CheckForWalls(direction)) {
            Destroy(gameObject);
        }
    }
    bool CheckForWalls(Vector3 moveDir) {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, moveDir);
        Vector3 flatDir = moveDir;
        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.isTrigger) {
                return true;
            }
        }
        return false;
    }
}