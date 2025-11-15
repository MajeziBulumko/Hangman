using System;

namespace Work_Here
{
    internal class FileStream
    {
        private string v;
        private object open;
        private object read;

        public FileStream(string v, object open, object read)
        {
            this.v = v;
            this.open = open;
            this.read = read;
        }

        internal void Write(byte[] info, int v, int length)
        {
            throw new NotImplementedException();
        }
    }
}