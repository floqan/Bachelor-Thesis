using System.IO;
using UnityEngine;
using UnityEditor.Callbacks;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

public class PostBuildProcess : IPostprocessBuildWithReport
{
    public int callbackOrder { get { return 0; } }

    public void OnPostprocessBuild(BuildReport report)
    {
        string importPath = Path.Combine(Path.GetDirectoryName(report.summary.outputPath), Application.productName + "_Data", "Import");       
        Directory.CreateDirectory(importPath);
        Directory.CreateDirectory(Path.Combine(importPath, "Models"));
        Directory.CreateDirectory(Path.Combine(importPath, "Textures"));
    }
}
