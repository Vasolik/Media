using Vipl.Media.Core;

namespace Vipl.Media.MP4.Boxes;

/// <summary> Extension of <see cref="ContainerBox"/>. </summary>
public static class ContainerBoxExtension
{
    /// <summary> Loads the children of the current instance from a specified file using
    /// the internal data position and size. </summary>
    /// <param name="containerBox">Box to load </param>
    /// <param name="file"> The <see cref="MediaFile" /> from which the current instance
    /// was read and from which to read the children. </param>
    /// <returns> A <see cref="T:System.Collections.Generic.IEnumerable`1" /> object
    /// enumerating the boxes read from the file. </returns>
    public static async Task LoadChildrenAsync(this IContainerBox containerBox, MP4 file)
    {
        // ReSharper disable once SuspiciousTypeConversion.Global
        if(containerBox is not Box basicBox)
            throw new ArgumentException("ContainerBox must be a Box");
        containerBox.Children.Clear();
        var position = (long)containerBox.ChildrenPosition;
        var end = position + (long)containerBox.ChildrenSize;
        
        while (position < end)
        {
            var child = await BoxFactory.CreateBoxAsync(file, position, basicBox.Header, basicBox.Handler);
            if (child.Size == 0)
                break;

            containerBox.Children.Add(child);
            if (child is IsoHandlerBox isoHandlerBox)
                basicBox.Handler = isoHandlerBox;
            position += (long)child.Size;
        }
    }
}