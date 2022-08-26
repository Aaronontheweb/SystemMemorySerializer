using System.Buffers;

namespace SystemMemorySerializer;

/// <summary>
/// Wraps the serialized output from another serializer and passes in the appropriate deserialization information
/// </summary>
public static class ProtobufCodec
{
    public static void WriteCodec(IBufferWriter<byte>)
}