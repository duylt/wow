using System.Web.Optimization;

public class StyleImagePathBundle
    : Bundle
{
    public StyleImagePathBundle(string virtualPath)
        : base(virtualPath)
    {
        base.Transforms.Add(new StyleRelativePathTransform());
        base.Transforms.Add(new CssMinify());
    }

    public StyleImagePathBundle(string virtualPath, string cdnPath)
        : base(virtualPath, cdnPath)
    {
        base.Transforms.Add(new StyleRelativePathTransform());
        base.Transforms.Add(new CssMinify());
    }
}