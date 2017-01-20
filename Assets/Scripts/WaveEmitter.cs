using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEmitter : MonoBehaviour {

    public float range = 5f;
    public float wave_speed = 10f;
    public float wave_cooldown = 2f;
    public bool activated = true;

    private float current_wave;

    protected Mesh _mesh;

    private MeshFilter _filter;
    private MeshRenderer _renderer;

    void Start ()
    {
        _mesh = new Mesh();
        _filter = GetComponent<MeshFilter>();
        _filter.mesh = _mesh;
        _renderer = GetComponent<MeshRenderer>();
        current_wave = 0;
	}
	
    List<Vector2> GetRaycastTargets()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range * 2, LayerMask.NameToLayer("Walls"));
        List<Vector2> raycast_targets = new List<Vector2>();
        foreach (Collider2D collider in colliders)
        {
            if (collider is PolygonCollider2D)
            {
                PolygonCollider2D poly = (PolygonCollider2D)collider;
                foreach (Vector2 vec in poly.points)
                {
                    raycast_targets.Add(vec + (Vector2)poly.transform.position);
                    raycast_targets.Add(vec + (Vector2)poly.transform.position + new Vector2(.01f, .01f));
                    raycast_targets.Add(vec + (Vector2)poly.transform.position - new Vector2(.01f, .01f));
                }
            }
            else if (collider is BoxCollider2D)
            {
                BoxCollider2D box = (BoxCollider2D)collider;
                Vector2[] vertices = new Vector2[4];
                vertices[0] = box.offset + new Vector2(-box.size.x / 2, -box.size.y / 2);
                vertices[1] = box.offset + new Vector2(-box.size.x / 2, box.size.y / 2);
                vertices[2] = box.offset + new Vector2(box.size.x / 2, -box.size.y / 2);
                vertices[3] = box.offset + new Vector2(box.size.x / 2, box.size.y / 2);
                foreach (Vector2 vec in vertices)
                {
                    raycast_targets.Add(vec + (Vector2)box.transform.position);
                    raycast_targets.Add(vec + (Vector2)box.transform.position + new Vector2(.01f, .01f));
                    raycast_targets.Add(vec + (Vector2)box.transform.position - new Vector2(.01f, .01f));
                }
            }
        }
        raycast_targets.Add(transform.position + new Vector3(-range, -range));
        raycast_targets.Add(transform.position + new Vector3(range, -range));
        raycast_targets.Add(transform.position + new Vector3(-range, range));
        raycast_targets.Add(transform.position + new Vector3(range, range));
        raycast_targets.Sort(new ClockwiseVector2Comparer(transform.position));
        return (raycast_targets);
    }

    void RecalculateMesh()
    {
        List<Vector2> raycast_targets = GetRaycastTargets();
        Vector2 position = transform.position;
        Vector3[] vertices = new Vector3[raycast_targets.Count + 1];
        Vector2[] uvs = new Vector2[raycast_targets.Count + 1];
        int[] triangles = new int[raycast_targets.Count * 3];
        uvs[0] = new Vector2(0.5f, 0.5f);
        vertices[0] = new Vector2(0f, 0f);
        for (int i = 0; i < raycast_targets.Count; i++)
        {
            Vector2 target = raycast_targets[i];
            RaycastHit2D hit = Physics2D.Raycast(position, target - position, range * 2, LayerMask.NameToLayer("Walls"));
            Vector2 point = hit.point;
            if (point.magnitude == 0)
                point = position + (target - position).normalized * range * 2;
            vertices[i + 1] = point - position;
            uvs[i + 1] = new Vector2(vertices[i + 1].x / range / 2f + 0.5f, vertices[i + 1].y / range / 2f + 0.5f);
            triangles[i * 3] = i + 1;
            triangles[i * 3 + 1] = 0;
            triangles[i * 3 + 2] = (i + 1) % raycast_targets.Count + 1;
        }
        _mesh.Clear();
        _mesh.vertices = vertices;
        _mesh.uv = uvs;
        _mesh.triangles = triangles;
        _mesh.RecalculateNormals();
    }

    void Update ()
    {
        RecalculateMesh();
        if (!activated && current_wave == 0)
            return;
        current_wave += wave_speed * Time.deltaTime;
        if (current_wave > range)
            current_wave = 0;
        _renderer.material.SetFloat("_Range", current_wave / range);
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        List<Vector2> raycast_targets = GetRaycastTargets();
        foreach (Vector2 vec in raycast_targets)
            Gizmos.DrawLine(transform.position, vec);
        Gizmos.color = Color.green;
        if (_mesh != null)
            foreach (Vector2 vec in _mesh.vertices)
                Gizmos.DrawLine(transform.position, vec + (Vector2)transform.position);
    }
}
