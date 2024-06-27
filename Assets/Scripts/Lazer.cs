using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    [SerializeField] float area = 1f;
    [SerializeField] Transform lightStart;
    [SerializeField] Transform lightEnd;
    [SerializeField] Transform lightMid;
    Transform _ownerTransform;
    Transform _transform;
    LineRenderer _line;
    PolygonCollider2D _poly;
    DeathArea _deathArea;

    private void Awake()
    {
        _transform = transform;
        _line = GetComponent<LineRenderer>();
        _poly = GetComponent<PolygonCollider2D>();
        _deathArea = GetComponent<DeathArea>();
    }
    public void SetLazerOwner(GameObject owner)
    {
        _deathArea.owner = owner;
        _ownerTransform = owner.transform;
    }
    public void SetLinePoints(Vector3 start, Vector3 end)
    {
        _line.SetPosition(0, start);
        _line.SetPosition(1, end);
    }
    private void Update()
    {
        SetupLight();
        CreateCollider();
    }

    private void SetupLight()
    {
        var start = _line.GetPosition(0);
        var end = _line.GetPosition(1);
        var lightDirection = end - start;
        lightStart.position = _ownerTransform.position;
        lightEnd.position = _ownerTransform.position + lightDirection;

        lightMid.position = _ownerTransform.position + (lightDirection / 2);
        lightMid.rotation = Quaternion.Euler(0, 0, -Mathf.Rad2Deg * Mathf.Atan2(lightDirection.x, lightDirection.y));

        var scale = lightMid.localScale;
        scale.y = lightDirection.magnitude;
        lightMid.localScale = scale;
    }

    private void CreateCollider()
    {
        var start = _line.GetPosition(0);
        var end = _line.GetPosition(1);
        var lightDirection = end - start;
        Vector2 startV2 = new Vector2(_ownerTransform.position.x-_transform.position.x, _ownerTransform.position.y-_transform.position.y);
        Vector2 endV2 = new Vector2(_ownerTransform.position.x + lightDirection.x-_transform.position.x, _ownerTransform.position.y + lightDirection.y-_transform.position.y);

        var perpendicular = Vector2.Perpendicular(startV2 - endV2).normalized / 2 * area;

        Vector2[] points = new[] { startV2 + perpendicular, startV2 - perpendicular, endV2 - perpendicular, endV2 + perpendicular };
        _poly.points = points;
        _poly.SetPath(0, points);
    }
}
