using System.Reflection;
using System.Runtime.Loader;
using System;

namespace LessonShowTools;

class PluginLoadContext(string pluginPath) : AssemblyLoadContext("LessonShowTools.PluginLoadContext")
{
    private readonly AssemblyDependencyResolver _resolver = new(pluginPath);

    protected override Assembly Load(AssemblyName assemblyName)
    {
        var assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);
        if (assemblyPath != null)
        {
            return LoadFromAssemblyPath(assemblyPath);
        }

        return null;
    }

    protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
    {
        var libraryPath = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
        if (libraryPath != null)
        {
            return LoadUnmanagedDllFromPath(libraryPath);
        }

        return IntPtr.Zero;
    }
}