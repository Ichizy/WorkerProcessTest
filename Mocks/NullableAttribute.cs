using System;

namespace WorkerProcessTest.Mocks
{
    internal class NullableAttribute : Attribute
    {
        private int v;

        public NullableAttribute(int v)
        {
            this.v = v;
        }
    }
}