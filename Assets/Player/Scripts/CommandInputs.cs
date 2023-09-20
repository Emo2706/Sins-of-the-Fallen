using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommandInputs : MonoBehaviour
{
    public abstract void Execute(Player player);
}
