using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance { get => instance; }

    [SerializeField] protected Vector3 mouseWorldPos;
    public Vector3 MouseWorldPos { get => mouseWorldPos; }

    [SerializeField] protected float onFiring;
    public float OnFiring { get => onFiring; }

    protected Vector4 direction;
    public Vector4 Direction => direction;
    [SerializeField] protected float leftBoundary;
    [SerializeField] protected float rightBoundary;
    [SerializeField] protected float topBoundary;
    [SerializeField] protected float bottomBoundary;
    public float TopBoundary => topBoundary;
    public float BottomBoundary => bottomBoundary;
    public float LeftBoundary => leftBoundary;
    public float RightBoundary => rightBoundary;

    Camera mainCamera;

    void Awake()
    {
        if (InputManager.instance != null) Debug.LogError("Only 1 InputManager allow to exist");
        InputManager.instance = this;
        GetPositionBoundary();
    }
    protected virtual void GetPositionBoundary()
    {
        mainCamera = Camera.main;

        Vector3 screenLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, Camera.main.nearClipPlane));
        Vector3 screenRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 0.5f, Camera.main.nearClipPlane));
        Vector3 screenTop = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 1, Camera.main.nearClipPlane));
        Vector3 screenBottom = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0, Camera.main.nearClipPlane));

        leftBoundary = screenLeft.x;
        rightBoundary = screenRight.x;
        topBoundary = screenTop.y;
        bottomBoundary = screenBottom.y;
    }

    void Update()
    {
        this.GetMouseDown();
        this.GetDirectionByKeyDown();
    }

    void FixedUpdate()
    {
        this.GetMousePos();
    }

    protected virtual void GetMouseDown()
    {
        this.onFiring = Input.GetAxis("Fire1");
    }

    protected virtual void GetMousePos()
    {
        this.mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    protected virtual void GetDirectionByKeyDown()
    {
        this.direction.x = Input.GetKeyDown(KeyCode.A) ? 1 : 0;
        if(this.direction.x == 0) this.direction.x = Input.GetKeyDown(KeyCode.LeftArrow) ? 1 : 0;

        this.direction.y = Input.GetKeyDown(KeyCode.D) ? 1 : 0;
        if (this.direction.y == 0) this.direction.y = Input.GetKeyDown(KeyCode.RightArrow) ? 1 : 0;
        
        this.direction.z = Input.GetKeyDown(KeyCode.W) ? 1 : 0;
        if (this.direction.z == 0) this.direction.z = Input.GetKeyDown(KeyCode.UpArrow) ? 1 : 0;
        
        this.direction.w = Input.GetKeyDown(KeyCode.S) ? 1 : 0;
        if (this.direction.w == 0) this.direction.w = Input.GetKeyDown(KeyCode.DownArrow) ? 1 : 0;

        //if (this.direction.x == 1) Debug.Log("Left");
        //if (this.direction.y == 1) Debug.Log("Right");
        //if (this.direction.z == 1) Debug.Log("Up");
        //if (this.direction.w == 1) Debug.Log("Down");
    }
}
