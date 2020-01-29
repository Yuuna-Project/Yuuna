// Author: Orlys
// Github: https://github.com/Orlys

namespace Yuuna.Contracts.Modules
{
    public interface IModuleMetadata
    {
        string Author { get; set; }
        string Description { get; set; }
        string Name { get; }
        string Url { get; set; }
    }
}