// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using System.Text.Json;

namespace Bengbenz.Embassy.eServices.Core;

public sealed class ErrorDetails(int statusCode, string message)
{
  public int StatusCode { get; set; } = statusCode;
  public string Message { get; set; } = message;

  public override string ToString()
  {
    return JsonSerializer.Serialize(this);
  }
}
