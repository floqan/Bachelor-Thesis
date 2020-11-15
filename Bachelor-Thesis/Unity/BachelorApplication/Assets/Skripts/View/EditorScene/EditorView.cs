using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EditorView : MonoBehaviour, IEditorView
{
    public GameObject Selection { get; set; }
    public GameObject Arrow { get; set; }
    public GameObject CameraPreview;
    public GameObject DefaultSun;

    public float objectDistance;
    public event EventHandler OnPlaceItem = (sender, e) => { };
    public event EventHandler<SceneObjectSelectedEventArgs> OnSceneObjectSelected = (sender, e) => { };
    public event EventHandler<SceneObjectSelectedEventArgs> OnHideParameter = (sender, e) => { };
    public event Action OnSceneObjectDeselected;
    public event Action OnDefaultSunDeleted;
    public enum State { ItemPlacement, ItemSelection, Idle, SelectionMovement, TexturePlacement}

    private Vector3 lastMousePosition;
    private Vector3 Direction;
    public float movementSpeed;
    public State state { get; set; }

    private void Start()
    {
        Arrow = GameObject.FindGameObjectWithTag("Arrow");
        Arrow.SetActive(false);
        foreach (Transform child in Arrow.transform)
        {
            child.gameObject.GetComponent<ArrowView>().OnMoveSelection += StartMoveSelection;
        }
    }

    private void Update()
    {
        //Item selection
        if (state == State.Idle || state == State.ItemSelection)
        {
            if (Input.GetMouseButtonUp(0) && !IsMouseOverUI())
            {
            
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("SceneObject", "Visualization")))
                {
                    SceneObjectSelectedEventArgs args = new SceneObjectSelectedEventArgs();
                    args.Selection = hit.transform.gameObject;
                    
                    OnSceneObjectSelected(this, args);
                }
                else
                {
                    if (Selection != null)
                    {
                        OnSceneObjectDeselected();
                    }
                }
            }
        }

        //Delete selected sceneobject with DeleteKey
        if(state == State.ItemSelection)
        {
            if (Input.GetKeyUp(KeyCode.Delete))
            {
                ISceneObjectView view = Selection.GetComponent<ISceneObjectView>();
                if (view != null)
                {
                    SceneObjectSelectedEventArgs args = new SceneObjectSelectedEventArgs();
                    view.DeleteSceneObject();
                    args.Selection = Selection;
                    OnHideParameter(this, args);
                    Selection.GetComponent<SceneObjectView>().DestoyController();
                }
                else
                {
                    OnDefaultSunDeleted();
                }
                Destroy(Selection);
                Arrow.SetActive(false);
                state = State.Idle;
                Selection = null;
            }
        }

        //During texture placement
        if(state == State.TexturePlacement)
        {
            if (!IsMouseOverUI())
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("SceneObject")))
                {
                    Selection.transform.position = hit.point;
                    if (Input.GetMouseButtonUp(0)) {
                        ISceneObjectView view = hit.transform.GetComponent<ISceneObjectView>();
                        if (view != null) {
                            StoreParameterEventArgs args = new StoreParameterEventArgs(Constants.SceneObjectParameter.TEXTURE);
                            args.Value = Selection.GetComponent<Renderer>().material;
                            view.ParameterValueChanged(this, args);
                        } 
                    }
                }
                else
                {
                    Vector3 pos = Input.mousePosition;
                    pos.z = objectDistance;

                    Selection.transform.position = Camera.main.ScreenToWorldPoint(pos);
                }
            }

            if (Input.GetMouseButtonUp(1))
            {
                Destroy(Selection);
                Selection = null;
                state = State.Idle;
            }



        }

        //During moving with Arrow
        if (state == State.SelectionMovement)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Direction = Vector3.zero;
                state = State.ItemSelection;
            }
            else
            {
                Vector2 diff = Input.mousePosition - lastMousePosition;
                float movementSpeed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? 0.1f : 0.5f;
                Selection.transform.position += Direction * movementSpeed * (diff.x + diff.y);
                lastMousePosition = Input.mousePosition;
                Selection.GetComponent<ISceneObjectView>().SceneObjectValueChanged(Constants.SceneObjectParameter.POSITION, Selection.transform.position);
                MoveArrow();
            }
        }

        //During placement of an item
        if (Selection != null && state == State.ItemPlacement)
        {
            //Dont show item above UI
            if (!IsMouseOverUI())
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("SceneObject", "Visualization")))
                {
                    if (hit.transform.tag != "Arrow")
                    {
                        Selection.transform.position = hit.point;
                    }
                }
                else
                {
                    Vector3 pos = Input.mousePosition;
                    pos.z = objectDistance;

                    Selection.transform.position = Camera.main.ScreenToWorldPoint(pos);
                }
                MoveArrow();

                Selection.GetComponent<ISceneObjectView>().SceneObjectValueChanged(Constants.SceneObjectParameter.POSITION, Selection.transform.position);

                if (Input.GetMouseButtonUp(0))
                {
                    OnPlaceItem(this, EventArgs.Empty);
                }
                if (Input.GetMouseButtonUp(1))
                {
                    Selection.GetComponent<ISceneObjectView>().DeleteSceneObject();
                    Selection.GetComponent<ISceneObjectView>().DestoyController();
                    Destroy(Selection);
                    Arrow.SetActive(false);
                    Selection = null;
                    state = State.Idle;
                }
            }
        }        
    }

    public void testRandomizeObject()
    {
        if (Selection != null)
        {
            Selection.GetComponent<ISceneObjectView>().TestRandomizeObject();
        }
    }
    public void DisplayCameraPreview()
    {
        //CameraPreview.GetComponent<Image>().;
        CameraPreview.SetActive(true);
    }

    public void HideCameraPreview()
    {
        CameraPreview.SetActive(false);
    }

    public void MoveArrow()
    {
        if(Selection == null)
        {
            return;
        }
        BoxCollider collider = Selection.GetComponent<BoxCollider>();
        Vector3 arrowPosition = new Vector3(collider.bounds.max.x + 3, collider.bounds.min.y, collider.bounds.min.z);
        Arrow.transform.position = arrowPosition;
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public void Destroy(GameObject sceneObject)
    {
        UnityEngine.Object.Destroy(sceneObject);
    }

    public void StartMoveSelection(object sender, MoveSelectionEventArgs e)
    {
        if (state != State.ItemPlacement)
        {
            Direction = e.Direction;
            state = State.SelectionMovement;
            lastMousePosition = Input.mousePosition;
        }
    }

    public void DeleteDefaultSun(bool defaultSunDeleted)
    {
        if (defaultSunDeleted)
        {
            DestroyImmediate(DefaultSun);
        }
    }
}
