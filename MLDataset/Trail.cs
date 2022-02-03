using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.UnityConverters;
using Newtonsoft.Json.UnityConverters.Math;

using UnityEngine;

namespace MLDataset
{
    public class Trail : MonoBehaviour
    {
        public string path = "output/json";
        public string fileName = "coordinate_list";
        
        // 1. Empty list of coordinates
        // [SerializeField]
        public List<Vector3> position = 
            new List<Vector3>();
        
        public List<UnityEngine.Quaternion> rotation = 
            new List<UnityEngine.Quaternion>();
        
        private Camera _camera;
    
        // Start is called before the first frame update
        void Start()
        {
            _camera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            // 2. get game object coordinates
            position.Add(transform.localPosition + _camera.transform.localPosition);
            rotation.Add(transform.localRotation);
        }

        void OnApplicationQuit()
        {
            var pathWithExtension = Path.Combine(path, fileName+ ".json");

            if(!Directory.Exists(path))
                Directory.CreateDirectory(path);

            // 3. write to json file
            // string jsonPosition = JsonConvert.SerializeObject(position);
            // string jsonPosition = JsonConvert.SerializeObject(rotation);

            string jsonPosition = JsonUtility.ToJson(this);
            
            File.WriteAllText(
                pathWithExtension, 
                jsonPosition);
        }
    }
}
