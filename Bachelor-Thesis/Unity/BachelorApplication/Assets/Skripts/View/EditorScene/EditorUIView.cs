using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EditorUIView : MonoBehaviour, IEditorUIView
{
    private bool GeneratingImages;
    private float TimeTicker;
    public int ImageCounter { get; set; }

    public GameObject[] tabs;
    public Transform ParameterView;
    public GameObject LoadingUI;
    public GameObject CreationUI;
    public GameObject DuringGeneration;
    public GameObject AfterGeneration;
    public TMPro.TextMeshProUGUI DisplayImageCounter;
    public TMPro.TextMeshProUGUI DisplayMessage;
    private List<SceneObjectParameterController> sceneObjectParameterControllers;

    private void Start()
    {
        if (!GeneratingImages) 
        { 
            SwitchTab(0);
            LoadingUI.SetActive(false);
            sceneObjectParameterControllers = new List<SceneObjectParameterController>();
            AdjustCameraPreview();
        }
    }

    private void Update()
    {
        if (GeneratingImages)
        {  
            //Animate loadingscreen
            TimeTicker += Time.deltaTime;
            if(TimeTicker > 1)
            {
                TimeTicker = 0;
                string displayMessage = DisplayMessage.text;
                if (displayMessage.EndsWith("..."))
                {
                    DisplayMessage.text = "Generate images .";
                }
                else if (displayMessage.EndsWith(".."))
                {
                    DisplayMessage.text = "Generate images ...";
                }
                else
                {
                    DisplayMessage.text = "Generate images ..";
                }
            }

            //Update Counter
            int tmpCounter = ImageCounter / 10;
            int currentCounter = int.Parse(DisplayImageCounter.text) / 10;
            if(tmpCounter > currentCounter)
            {
                DisplayImageCounter.text = (tmpCounter * 10).ToString();
            }
        }
    }

    public void LoadMenuScene()
    {
        ApplicationManager.instance.LoadMenuScene();
    }

    public void SwitchTab(int index)
    {
        for(int i = 0; i < tabs.Length; i++)
        {
            if (i == index)
            {
                tabs[i].gameObject.SetActive(true);
            }
            else
            {
                tabs[i].gameObject.SetActive(false);
            }
        }
    }

    public void SwitchUI()
    {
        CreationUI.SetActive(false);
        LoadingUI.SetActive(true);
        AfterGeneration.SetActive(false);
        DuringGeneration.SetActive(true);
        GeneratingImages = true;
    }

    public void DisplaySceneObjectParameter(SceneObjectSelectedEventArgs e)
    {
        foreach(var parameter in e.Parameter)
        {
            GameObject view = parameter.GetGameObjectView();
            SceneObjectParameterController controller = new SceneObjectParameterController(parameter, view.GetComponent<ISceneObjectParameterView>());
            view.GetComponent<ISceneObjectParameterView>().OnParameterValueChanged += e.Selection.GetComponent<ISceneObjectView>().ParameterValueChanged;
            e.Selection.GetComponent<SceneObjectView>().OnSceneObjectValueChanged += controller.UpdateParameterView;
            sceneObjectParameterControllers.Add(controller);
            view.transform.SetParent(ParameterView);
            view.transform.localPosition = new Vector3(view.transform.position.x, view.transform.position.y, 0);
            view.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void ClearParameterView(SceneObjectSelectedEventArgs e)
    {
        foreach (SceneObjectParameterController controller in sceneObjectParameterControllers)
        {
            e.Selection.GetComponent<SceneObjectView>().OnSceneObjectValueChanged -= controller.UpdateParameterView;
        }
        sceneObjectParameterControllers.Clear();

        foreach (Transform child in ParameterView)
        {
            child.GetComponent<ISceneObjectParameterView>().OnParameterValueChanged -= e.Selection.GetComponent<SceneObjectView>().ParameterValueChanged;
            Destroy(child.gameObject);
        }
    }

    public void GenerationFinished()
    {
        DuringGeneration.SetActive(false);
        AfterGeneration.SetActive(true);
    }

    public void OpenExplorer()
    {
        string path = ApplicationManager.instance.GlobalSettingsModel.GeneralParameterModel.TargetFolder.Replace(@"/", @"\");   // explorer doesn't like front slashes
        System.Diagnostics.Process.Start("explorer.exe", "/select," + path);
    }

    public void BackToMenu()
    {
        ApplicationManager.instance.generatedImages = 0;
        ApplicationManager.instance.IsGeneratingImages = false;
        LoadMenuScene();
    }

    private void AdjustCameraPreview()
    {
        //TODO
    }
}
