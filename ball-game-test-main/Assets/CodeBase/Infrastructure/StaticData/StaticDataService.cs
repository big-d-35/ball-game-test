using UnityEngine;

namespace CodeBase.Infrastructure.Data
{
    public class StaticDataService
    {
        private StaticData _staticData;

        public void LoadStaticData(string path)
        {
            _staticData = Resources.Load<StaticData>(path);

            if (_staticData == null)
                throw new System.Exception($"Static data not found at path: {path}");
        }

        public StaticData GetStaticData() =>
            _staticData;
    }
}
