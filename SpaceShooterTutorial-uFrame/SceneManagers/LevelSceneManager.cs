using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;


// <summary>
// The responsibility of this class is to manage the scenes Initialization, Loading, Transitioning, and Unloading.
// </summary>
public class LevelSceneManager : LevelSceneManagerBase {
    
    // <summary>
    // This method is invoked exactly right before the Load method is invoked.
    // </summary>
    public override void OnLoading() {
        base.OnLoading();
    }
    
    // <summary>
    // This method loads the scene instantiating any views or regular prefabs required by the scene.  This method is invoked after 
    // Setup() and Initialize().  Note: be sure to report progress to the delgate supplied in the first parameter.  It will 
    // update the loading screen to display a nice status message.
    // </summary>
    public override System.Collections.IEnumerator Load(UpdateProgressDelegate progress) {
        // Use the controllers to create the game.
        yield break;
    }
    
    // <summary>
    // This method is invoked after uFrame has completed its scene boot and the game is ready to begin.  Here would 
    // be a good place to use the generated Controller properties on this class to invoke some gameplay logic initialization
    // </summary>
    public override void OnLoaded() {
        base.OnLoaded();
    }
    
    // <summary>
    // This method is invoked when transitioning to another scene.  This could be used to destory a scene or effectively log 
    // a user off.
    // </summary>
    public override void Unload() {
        base.Unload();
    }
    
    // <summary>
    // This method is the first method to be invoked when the scene first loads. Anything registered here with 'Container' will effectively 
    // be injected on controllers, and instances defined on a subsystem.And example of this would be Container.RegisterInstance<IDataRepository>(new CodeRepository()). Then any property with 
    // the 'Inject' attribute on any controller or view-model will automatically be set by uFrame. 
    // </summary>
    public override void Setup() {
        base.Setup();
    }
}
