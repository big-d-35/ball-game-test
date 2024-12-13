using UnityEngine;

namespace CodeBase.Infrastructure.Data
{
    [CreateAssetMenu(fileName = "StaticData", menuName = "Static Data")]
    public class StaticData : ScriptableObject
    {
        public string StartScene;
    }
}
