using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnvironmentActionController 
{
    // Excecutes environment action(s) handled by the controller
    void ExcecuteAction();

    // Updates environment action(s) handled by the controller 
    void UpdateAction();
    
    // initialized environment action(s) handled by the controller
    void InitAction();

    // Initilizes the environment action controller 
    void InitActionController();
}
