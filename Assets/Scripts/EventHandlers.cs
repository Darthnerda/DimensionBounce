using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface DeathHandler : IEventSystemHandler
{
    void OnDied();
}

// public interface DimensionSwitchHandler : IEventSystemHandler
// {
//     void SwitchDimensions(int DimensionIdx);
// }

public interface Runs : IEventSystemHandler
{
    void OnRunEnter();
    void OnRunExit();
}

public interface HandlesLevelNext : IEventSystemHandler
{
    void OnNextLevel();
}

public interface HandlesExitPortal : IEventSystemHandler
{
    void OnExitPortal();
}
