using UnityEngine;
using System.Collections;

namespace Xft
{
    public class RenderObject
    {
        public EffectNode Node;

        public virtual void Initialize(EffectNode node)
        {
            Node = node;
        }

        public virtual void Reset()
        {

        }

        public virtual void Update(float deltaTime)
        {

        }

        public virtual void ApplyShaderParam(float x, float y)
        {

        }

    }
}

