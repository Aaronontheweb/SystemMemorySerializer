using System.Buffers;

namespace SystemMemorySerializer
{
    public abstract class Serializer
    {
        protected Serializer()
        {
            Identifier = 1;
        }

        public virtual short Identifier { get; }

        public abstract string ToBinary(IBufferWriter<byte> bufferWriter, object obj);

        public abstract object FromBinary(IMemoryOwner<byte> memory, string manifest);
    }
}
