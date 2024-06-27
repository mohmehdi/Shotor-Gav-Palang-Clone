using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    [SerializeField] float area = 1f;
    [SerializeField] Transform lightStart;
    [SerializeField] Transform lightEnd;
    [SerializeField] Transform lightMid;
    Vector3 _lineDirection = Vector3.zero;
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
        _lineDirection =  end - start;
    }
    private void Update()
    {
        UpdateLine();
        SetupLight();
        CreateCollider();
    }

    private void UpdateLine()
    {
        _line.SetPosition(0, _ownerTransform.position);
        _line.SetPosition(1, _ownerTransform.position + _lineDirection);
    }

    private void SetupLight()
    {
        lightStart.position = _ownerTransform.position;
        lightEnd.position = _ownerTransform.position + _lineDirection;

        lightMid.position = _ownerTransform.position + (_lineDirection / 2);
        lightMid.rotation = Quaternion.Euler(0, 0, -Mathf.Rad2Deg * Mathf.Atan2(_lineDirection.x, _lineDirection.y));

        var scale = lightMid.localScale;
        scale.y = _lineDirection.magnitude;
        lightMid.localScale = scale;
    }

    private void CreateCollider()
    {
        Vector2 startV2 = new Vector2(_ownerTransform.position.x-_transform.position.x, _ownerTransform.position.y-_transform.position.y);
        Vector2 endV2 = new Vector2(_ownerTransform.position.x + _lineDirection.x-_transform.position.x, _ownerTransform.position.y + _lineDirection.y-_transform.position.y);

        var perpendicular = Vector2.Perpendicular(startV2 - endV2).normalized / 2 * area;

        Vector2[] points = new[] { startV2 + perpendicular, startV2 - perpendicular, endV2 - perpendicular, endV2 + perpendicular };
        _poly.points = points;
        _poly.SetPath(0, points);
    }
}
