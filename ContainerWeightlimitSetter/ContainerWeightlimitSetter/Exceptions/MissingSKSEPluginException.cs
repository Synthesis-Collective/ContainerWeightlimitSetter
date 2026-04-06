using Noggog;

namespace ContainerWeightlimitSetter.Exceptions;

public class MissingSKSEPluginException : Exception
{
    private readonly string? _filePath;
    private string? _message;
    private bool _noPath;
    
    public MissingSKSEPluginException(string content, bool isFilePath = false)
    {
        if (isFilePath) _filePath = content;
        else _message = content;
    }

    public MissingSKSEPluginException(string message, string filePath)
    {
        _filePath = filePath;
        _message = message;
    }

    public override string ToString()
    {
        if (_filePath.IsNullOrEmpty() || _filePath.IsNullOrWhitespace())
        {
            if (_message.IsNullOrEmpty() || _message.IsNullOrWhitespace()) return "MissingSKSEPluginDependency";
            return _message;
        }

        if (_message.IsNullOrEmpty() || _message.IsNullOrWhitespace())
            return $"MissingSKSE Dependency at {_filePath}";
        return $"{_message}\n\n Missing SKSE Dependency at {_filePath}";
    }
}