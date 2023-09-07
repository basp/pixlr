using System.Numerics;

namespace Pixlr.Generic;

public record Vector2<T>(T X, T Y)
    where T : IFloatingPointIeee754<T>
{
    
}