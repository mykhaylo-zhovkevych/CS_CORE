using System;

namespace FactoryMethodPattern.MVCFramework
{
    public interface IViewEngine
    {
        string Render(string fileName, Dictionary<string, object> data);
    }
}