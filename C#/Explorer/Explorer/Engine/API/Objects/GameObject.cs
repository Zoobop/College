using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace EngineAPI
{
    internal class GameObject : ILoggable
    {
        public Guid GUID { get; private set; }
        public Color Color { get; protected set; }
        public List<string> Tags { get; protected set; }
        public string Name { get; protected set; }
        public Transform Transform { get; private set; }

        public GameObject()
        {
            GUID = Guid.NewGuid();
            Color = Color.White;
            Tags = new List<string>();
            Name = string.Empty;
            Transform = new Transform();

            EngineBase.RegisterObject(this);
        }

        public GameObject(string name)
        {
            GUID = Guid.NewGuid();
            Color = Color.White;
            Tags = new List<string>();
            Name = name;
            Transform = new Transform();

            EngineBase.RegisterObject(this);
        }

        public GameObject(Transform transform)
        {
            GUID = Guid.NewGuid();
            Color = Color.White;
            Tags = new List<string>();
            Name = string.Empty;
            Transform = transform;

            EngineBase.RegisterObject(this);
        }

        public GameObject(Vector2 position, Vector2 scale)
        {
            GUID = Guid.NewGuid();
            Color = Color.White;
            Tags = new List<string>();
            Name = string.Empty;
            Transform = new Transform(position, scale);

            EngineBase.RegisterObject(this);
        }

        public GameObject(string name, Transform transform)
        {
            GUID = Guid.NewGuid();
            Color = Color.White;
            Tags = new List<string>();
            Name = name;
            Transform = transform;

            EngineBase.RegisterObject(this);
        }

        public GameObject(string name, Vector2 position, Vector2 scale)
        {
            GUID = Guid.NewGuid();
            Color = Color.White;
            Tags = new List<string>();
            Name = name;
            Transform = new Transform(position, scale);

            EngineBase.RegisterObject(this);
        }

        ~GameObject()
        {
            EngineBase.UnregisterObject(this);
        }

        public void AddTags(params string[] args) => Tags.AddRange(args);

        public virtual void OnKeyDown(object sender, KeyEventArgs e) { }
        public virtual void OnKeyUp(object sender, KeyEventArgs e) { }


        public virtual void Start() { }

        public virtual void Update() { }

        public virtual void Destroy() { }

        #region ILoggable

        public string LogView()
        {
            return $"Object[{Name}]";
        }

        #endregion
    }
}
