
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Parallax References")]
    public Camera cam;
    public Transform subject;

    Vector2 startPosition;
    float startZ;

    Vector2 travel => (Vector2)cam.transform.position-startPosition;

    float distanceFromSubject => transform.position.z - subject.position.z;
    float clippingPlane => (cam.transform.position.z + (distanceFromSubject > 0 ? cam.farClipPlane : cam.nearClipPlane));

    float parallaxFactor => Mathf.Abs(distanceFromSubject)/clippingPlane;

    // Initializes Parallax Class
    public void Start() {
        startPosition = transform.position;
        startZ = transform.position.z;
    }

    // Updates Parallax Based On Camera
    void Update()
    {
//        Debug.Log(parallaxFactor + " " + this.gameObject.name);
        Vector2 newPos = startPosition+(travel*parallaxFactor*10);
        // Debug.Log(transform.position.z + " " + subject.position.z);
        transform.position = PixelPerfectClamp(new Vector3(newPos.x, newPos.y, startZ), 32);
    }

    // Corrects Jittering
    private Vector3 PixelPerfectClamp(Vector3 locationVector, float pixelsPerUnit)
    {
        Vector3 vectorInPixels = new Vector3(Mathf.CeilToInt(locationVector.x * pixelsPerUnit), Mathf.CeilToInt(locationVector.y * pixelsPerUnit), Mathf.CeilToInt(locationVector.z * pixelsPerUnit));
        return vectorInPixels / pixelsPerUnit;
    }
}
