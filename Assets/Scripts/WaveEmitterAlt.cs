using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class WaveEmitterAlt : MonoBehaviour {

    public float range = 5f;
    public float wave_speed = 10f;
    public float wave_cooldown = 2f;
    public float width = 1f;
    public bool auto_emit = true;
    public int mesh_res = 360;
    [HideInInspector]
    public int destroy_after = -1;
    public float reset_delay = .5f;
    [HideInInspector]
    public MovingEmitter moving_emitter;

    private DeathSound _death_sound;

    private Vector2 pos2 { get { return (transform.position); } }
    private float delta_speed;

    private float current_wave;
    private float current_wave_cooldown;

    private float[] distances;
    private bool[] is_fixed;

    private bool emitting = false;
    private bool resetting = false;
    private bool should_emit { get { return emitting || auto_emit; } }

    private Mesh _mesh;

    private MeshFilter _filter;
    private MeshRenderer _renderer;
    private CameraShader _camera;
    private Player _player;

    void Start()
    {
        distances = new float[mesh_res];
        is_fixed = new bool[mesh_res];
        for (int i = 0; i < mesh_res; i++)
        {
            distances[i] = range;
            is_fixed[i] = false;            
        }
        _mesh = new Mesh();
        _filter = GetComponent<MeshFilter>();
        _filter.mesh = _mesh;
        _renderer = GetComponent<MeshRenderer>();
        current_wave = 0;
        current_wave_cooldown = 0;
        _camera = FindObjectOfType<CameraShader>();
        _player = FindObjectOfType<Player>();
        _death_sound = FindObjectOfType<DeathSound>();
        OnEmit();
    }

    IEnumerator Reset()
    {
        float t = 0;
        while (t < reset_delay)
        {
            t += Time.deltaTime;
            _camera.aberration = 0.005f * Mathf.Tan(t * 100);
            _camera.fadeout = t / reset_delay;
            yield return new WaitForEndOfFrame();
        }
        _player.gameObject.SetActive(true);
        foreach (StateResetter resetter in FindObjectsOfType<StateResetter>())
            resetter.Reset();
        foreach (Buttons button in FindObjectsOfType<Buttons>())
            button.reset();
        _camera.aberration = 0.0f;
        _camera.fadeout = 0.0f;
        resetting = false;
        if (moving_emitter != null)
        {
            moving_emitter.Reset();
            moving_emitter.paused = false;
        }
    }

    void OnPlayerHit()
    {
        if (_player == null || _player.gameObject == null)
            _player = FindObjectOfType<Player>();
        _death_sound.Play();
        _player.gameObject.SetActive(false);
        resetting = true;
        if (moving_emitter != null)
            moving_emitter.paused = true;
        StartCoroutine(Reset());
    }

    void OnEmit()
    {
        _renderer.material.SetColor("_Color", ColorProvider.instance.GetColor(2));
    }

    List<Vector2> GetRaycastHitPoints()
    {
        List<Vector2> points = new List<Vector2>();
        for (int i = 0; i < mesh_res; i ++)
        {
            float angle = 360f / mesh_res * (mesh_res - i) * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
            if (!is_fixed[i])
            {
                RaycastHit2D[] hits = Physics2D.RaycastAll(pos2, dir, range);
                GameObject closest = null;
                float d = 0;
                foreach (RaycastHit2D hit in hits)
                {
                    if ((d == 0 || hit.distance < d) && hit.distance >= current_wave - width)
                    {
                        d = hit.distance;
                        closest = hit.collider.gameObject;
                    }
                }
                if (d == 0)
                    d = range;
                distances[i] = d;
                if (distances[i] < current_wave)
                {
                    if (closest.CompareTag("Player"))
                    {
                        OnPlayerHit();
                        continue;
                    }
                    is_fixed[i] = true;
                    continue;
                }
            }
            float dist = distances[i];
            dir *= dist;
            points.Add(dir + pos2);
        }
        return (points);
    }

    void RecalculateMesh()
    {
        List<Vector2> raycast_points = GetRaycastHitPoints();
        int points_count = raycast_points.Count;
        Vector3[] vertices = new Vector3[points_count + 1];
        Vector2[] uvs = new Vector2[points_count + 1];
        int[] triangles = new int[points_count * 3];
        uvs[0] = new Vector2(0.5f, 0.5f);
        vertices[0] = new Vector2(0f, 0f);
        for (int i = 0; i < raycast_points.Count; i++)
        {
            Vector2 point = raycast_points[i];
            vertices[i + 1] = point - pos2;
            uvs[i + 1] = new Vector2(vertices[i + 1].x / range / 2f + 0.5f, vertices[i + 1].y / range / 2f + 0.5f);
            triangles[i * 3] = i + 1;
            triangles[i * 3 + 1] = 0;
            triangles[i * 3 + 2] = (i + 1) % points_count + 1;
        }
        _mesh.Clear();
        _mesh.vertices = vertices;
        _mesh.uv = uvs;
        _mesh.triangles = triangles;
        _mesh.RecalculateNormals();
    }

    public void Emit()
    {
        current_wave = 0;
        emitting = true;
        OnEmit();
    }

    void Update()
    {
        if (!should_emit)
            return;
        if (destroy_after == 0)
        {
            if (resetting)
                return;
            Destroy(gameObject);
        }
        delta_speed = wave_speed * Time.deltaTime;
        RecalculateMesh();
        if (current_wave_cooldown > 0 && auto_emit)
        {
            current_wave_cooldown -= Time.deltaTime;
            if (current_wave_cooldown <= 0)
                OnEmit();
            return;
        }
        current_wave += delta_speed;
        if (current_wave > range)
        {
            current_wave = 0;
            if (!auto_emit)
                emitting = false;
            else
                current_wave_cooldown = wave_cooldown;
            for (int i = 0; i < mesh_res; i++)
            {
                distances[i] = range;
                is_fixed[i] = false;
            }
            if (destroy_after > 0)
                destroy_after--;
        }
        _renderer.material.SetFloat("_Range", current_wave / range);
        _renderer.material.SetFloat("_Width", width / range);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        if (_mesh != null)
            foreach (Vector2 vec in _mesh.vertices)
                Gizmos.DrawLine(transform.position, vec + (Vector2)transform.position);
    }
}
