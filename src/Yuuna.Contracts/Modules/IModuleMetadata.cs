// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

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