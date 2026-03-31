using UnityEngine;
using UnityEngine.InputSystem;

public class LinesDrawer : MonoBehaviour
{
    public GameObject linePrefab;
    public LayerMask cantDrawOverLayer;
    int cantDrawOverLayerIndex;

    [Space(30f)]
    public Gradient lineColor;
    public float linePointsMinDistance;
    public float lineWidth;
    public GameObject light; // ← ojo: GameObject (tenías Gameobject)
    bool isDrawing;
    [SerializeField] private bool Active_input;

    Line currentLine;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
        cantDrawOverLayerIndex = LayerMask.NameToLayer("CantDrawOver");

        if (light != null)
            light.SetActive(false); // empieza apagada
    }

    public void ondrawline(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isDrawing = true;
            BeginDraw();

            if (light != null)
                light.SetActive(true); // prender luz
        }

        if (context.canceled)
        {
            isDrawing = false;
            EndDraw();

            if (light != null)
                light.SetActive(false); // apagar luz
        }
    }

    void Update()
    {
        if (Active_input)
        {
            if (currentLine != null)
            {
                Draw();
            }

            // mover la luz con el mouse mientras dibuja
            if (isDrawing && light != null && cam != null)
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                light.transform.position = mousePosition;
            }
        }
    }

    // Begin Draw ----------------------------------------------
    void BeginDraw()
    {
        currentLine = Instantiate(linePrefab, this.transform).GetComponent<Line>();

        currentLine.UsePhysics(false);
        currentLine.SetLineColor(lineColor);
        currentLine.SetPointsMinDistance(linePointsMinDistance);
        currentLine.SetLineWidth(lineWidth);
    }

    // Draw ----------------------------------------------------
    void Draw()
    {
        if (Mouse.current == null || cam == null) return;

        Vector2 mousePosition = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        RaycastHit2D hit = Physics2D.CircleCast(
            mousePosition,
            lineWidth / 3f,
            Vector2.zero,
            0f,
            cantDrawOverLayer
        );

        if (!hit)
        {
            currentLine.AddPoint(mousePosition);
        }
    }

    // End Draw ------------------------------------------------
    void EndDraw()
    {
        if (currentLine != null)
        {
            if (currentLine.pointsCount < 2)
            {
                Destroy(currentLine.gameObject);
            }
            else
            {
                currentLine.gameObject.layer = cantDrawOverLayerIndex;
                currentLine.UsePhysics(false);
            }

            currentLine = null;
        }
    }
}