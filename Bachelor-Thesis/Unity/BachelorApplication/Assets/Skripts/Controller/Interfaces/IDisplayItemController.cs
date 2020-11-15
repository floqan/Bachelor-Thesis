using System;

public interface IDisplayItemController 
{

    void InitView();
    event EventHandler<DisplayItemClickedArgs> CreateSceneObject;

}
