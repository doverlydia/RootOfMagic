using UnityEngine;
namespace Runes
{

    [CreateAssetMenu(fileName = "SyllablesScriptableObject", menuName = "ScriptableObjects/SyllablesScriptableObject")]
    public class SyllablesScriptableObject : ScriptableObject
    {
        public string[] availableSyllables;
    }
}