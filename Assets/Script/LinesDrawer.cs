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

    bool isDrawing;
    

    Line currentLine;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
        cantDrawOverLayerIndex = LayerMask.NameToLayer("CantDrawOver");
    }

    public void ondrawline(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isDrawing = true;
            BeginDraw();
        }

        if (context.canceled)
        {
            isDrawing = false;
            EndDraw();
        }

        Debug.Log(context.phase);
    }
    private void FixedUpdate()
    {
    }
    void Update()
    {
        if (currentLine != null)
        {
            Draw();
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

        if (hit)
        {
            Debug.Log("Hit: " + hit.collider.name);
            //EndDraw();
        }
        else
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
                currentLine.UsePhysics(true);
            }

            currentLine = null;
        }
    }
}