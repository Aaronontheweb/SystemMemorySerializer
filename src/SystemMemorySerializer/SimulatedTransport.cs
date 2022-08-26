using System.Threading;
using System.Threading.Tasks;
using System.IO.P
using System.IO.Pipelines;

namespace SystemMemorySerializer;

public class SimulatedTransport
{
    private readonly Pipe _pipe = new Pipe();
    private readonly PipeReader _reader;
    private readonly PipeWriter _writer;
    private readonly Serializer _serializer;

    public SimulatedTransport(Serializer serializer)
    {
        _serializer = serializer;
        _reader = _pipe.Reader;
        _writer = _pipe.Writer;
    }

    ValueTask WriteAndFlushAsync(object message, CancellationToken ct = default)
    {
        var manifest = _serializer.ToBinary(_writer, message);
    }
}