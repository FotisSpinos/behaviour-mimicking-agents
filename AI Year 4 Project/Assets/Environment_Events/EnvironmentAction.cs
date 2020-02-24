using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnvironmentAction
{
    // Excecutes the environment action
    void ExcecuteAction();

    // Initializes the environment action
    void InitAction();

    // Updates the environment action
    void UpdateAction();
}
