using UnityEngine;

namespace ONE
{
    public interface IBox
    {
        public string Name { get; }
        public Transform InterationPoint { get; }
        public void Open();
    }
}
