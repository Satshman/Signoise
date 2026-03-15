using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MouseUtil 
{
    public static Vector3 GetMousePositionInWorldSpace(float zValue = 0f)
    {
        if (Camera.main == null)
            return Vector3.zero;   // Evita MissingReferenceException

        Camera cam = Camera.main;

        Plane dragPlane = new Plane(cam.transform.forward, new Vector3(0, 0, zValue));
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (dragPlane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }

        return Vector3.zero;
    }
}
