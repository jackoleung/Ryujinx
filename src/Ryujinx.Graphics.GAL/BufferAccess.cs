using System;

namespace Ryujinx.Graphics.GAL
{
    [Flags]
    public enum BufferAccess
    {
        Default = 0,
        FlushPersistent = 1 << 0,
        SparseCompatible = 1 << 1,
    }
}
