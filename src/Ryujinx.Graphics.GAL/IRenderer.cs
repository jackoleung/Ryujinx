using Ryujinx.Common.Configuration;
using System;
using System.Threading;

namespace Ryujinx.Graphics.GAL
{
    public interface IRenderer : IDisposable
    {
        event EventHandler<ScreenCaptureImageInfo> ScreenCaptured;

        bool PreferThreading { get; }

        IPipeline Pipeline { get; }

        IWindow Window { get; }

        void BackgroundContextAction(Action action, bool alwaysBackground = false);

        BufferHandle CreateBuffer(int size, BufferAccess access = BufferAccess.Default);
        BufferHandle CreateBuffer(int size, BufferAccess access, BufferHandle storageHint);
        BufferHandle CreateBuffer(nint pointer, int size);
        BufferHandle CreateBufferSparse(ReadOnlySpan<BufferRange> storageBuffers);

        IProgram CreateProgram(ShaderSource[] shaders, ShaderInfo info);

        ISampler CreateSampler(SamplerCreateInfo info);
        ITexture CreateTexture(TextureCreateInfo info, float scale);
        bool PrepareHostMapping(nint address, ulong size);

        void CreateSync(ulong id, bool strict);

        void DeleteBuffer(BufferHandle buffer);

        PinnedSpan<byte> GetBufferData(BufferHandle buffer, int offset, int size);

        Capabilities GetCapabilities();
        ulong GetCurrentSync();
        HardwareInfo GetHardwareInfo();

        IProgram LoadProgramBinary(byte[] programBinary, bool hasFragmentShader, ShaderInfo info);

        void SetBufferData(BufferHandle buffer, int offset, ReadOnlySpan<byte> data);

        void UpdateCounters();

        void PreFrame();

        ICounterEvent ReportCounter(CounterType type, EventHandler<ulong> resultHandler, bool hostReserved);

        void ResetCounter(CounterType type);

        void RunLoop(ThreadStart gpuLoop)
        {
            gpuLoop();
        }

        void WaitSync(ulong id);

        void Initialize(GraphicsDebugLevel logLevel);

        void SetInterruptAction(Action<Action> interruptAction);

        void Screenshot();
    }
}
