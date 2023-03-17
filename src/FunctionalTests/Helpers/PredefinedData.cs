using Domain.Entities;

namespace FunctionalTests.Helpers;

internal static class PredefinedData
{
  internal static List<Sample> Samples = new List<Sample>();

  internal static void InitializeSamples()
  {
      Samples.Clear();

      var sample1 = new Sample("sample1");
      Samples.Add(sample1);
      
      var sample2 = new Sample("sample2");
      Samples.Add(sample2);
  }
}