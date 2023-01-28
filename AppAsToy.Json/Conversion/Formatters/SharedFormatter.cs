using System;
using System.Collections.Generic;
using System.Text;

namespace AppAsToy.Json.Conversion.Formatters;
public abstract class SharedFormatter<T, TFormatter>
    where TFormatter : class, IFormatter<T>, new()
{
    public static readonly IFormatter<T> Shared = new TFormatter();
}
