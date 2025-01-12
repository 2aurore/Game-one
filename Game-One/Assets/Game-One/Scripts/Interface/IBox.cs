using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONE
{
    public interface IBox
    {
        public string Name { get; }

        public void Open();
    }
}
