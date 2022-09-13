using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineAPI
{
    public abstract class GameObject : ILoggable
    {
        public Guid GUID { get; private set; }
        public string Name { get; protected set; }
        public List<string> Tags { get; protected set; }

        public GameObject()
        {
            GUID = Guid.NewGuid();
            Name = string.Empty;
            Tags = new List<string>();

            EngineBase.RegisterObject(this);
        }

        public GameObject(string name)
        {
            GUID = Guid.NewGuid();
            Name = name;
            Tags = new List<string>();

            EngineBase.RegisterObject(this);
        }

        public GameObject(string name, params string[] tags)
        {
            GUID = Guid.NewGuid();
            Name = name;
            Tags = new List<string>(tags);

            EngineBase.RegisterObject(this);
        }

        public abstract void Start();
        public abstract void Update(Timestep ts);
        public abstract void Destroy();

        #region ILoggable

        public string LogView()
        {
            return $"Object[{Name}]";
        }

        #endregion

    }
}
